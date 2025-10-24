using F1GameDataParser.Database.Entities.Widgets;
using F1GameDataParser.Mapping.ViewModelFactories;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public class WebSocketBroadcastService : BackgroundService
{
    private readonly WebSocketConnectionManager _connections;
    private readonly TimingTowerFactory _timingTowerFactory;
    private readonly MinimapFactory _minimapFactory;
    private readonly StopwatchFactory _stopwatchFactory;
    private readonly HaloTelemetryDashboardFactory _haloTelemetryDashboardFactory;
    private readonly SpeedDifferenceFactory _speedDifferenceFactory;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly SemaphoreSlim broadcastLock = new(1, 1);

    public WebSocketBroadcastService(WebSocketConnectionManager connections,
                                       TimingTowerFactory timingTowerFactory,
                                       MinimapFactory minimapFactory,
                                       StopwatchFactory stopwatchFactory,
                                       HaloTelemetryDashboardFactory haloTelemetryDashboardFactory,
                                       SpeedDifferenceFactory speedDifferenceFactory)
    {
        _connections = connections;
        _timingTowerFactory = timingTowerFactory;
        _minimapFactory = minimapFactory;
        _stopwatchFactory = stopwatchFactory;
        _haloTelemetryDashboardFactory = haloTelemetryDashboardFactory;
        _speedDifferenceFactory = speedDifferenceFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var activeWidgets = _connections.ActiveWidgets();

            foreach (var widget in activeWidgets)
            {
                object? data = widget switch
                {
                    WidgetType.TimingTower => _timingTowerFactory.Generate(),
                    WidgetType.Minimap => _minimapFactory.Generate(),
                    WidgetType.StopwatchSpectated => _stopwatchFactory.GenerateSpectated(),
                    WidgetType.StopwatchList => _stopwatchFactory.Generate(),
                    WidgetType.HaloTelemetry => _haloTelemetryDashboardFactory.Generate(),
                    WidgetType.SpeedDifference => _speedDifferenceFactory.Generate(),
                    _ => null
                };

                if (data is not null)
                    await BroadcastAsync(widget, data, stoppingToken);
            }

            await Task.Delay(100, stoppingToken);
        }
    }

    public async Task BroadcastAsync(WidgetType widget, object data, CancellationToken? ct = null)
    {
        await broadcastLock.WaitAsync();
        try
        {
            var sockets = _connections.GetSockets(widget).ToList();
            if (sockets.Count == 0)
                return;

            var json = JsonSerializer.Serialize(data, _jsonOptions);
            var buffer = Encoding.UTF8.GetBytes(json);
            var segment = new ArraySegment<byte>(buffer);

            foreach (var socket in sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    try
                    {
                        await socket.SendAsync(segment, WebSocketMessageType.Text, true, ct ?? CancellationToken.None);
                    }
                    catch
                    {
                        _connections.RemoveSocket(widget, socket);
                    }
                }
                else
                {
                    _connections.RemoveSocket(widget, socket);
                }
            }
        }
        finally
        {
            broadcastLock.Release();
        }
    }
}
