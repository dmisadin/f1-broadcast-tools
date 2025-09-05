using F1GameDataParser.ViewModels;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace F1GameDataParser.Services;

public class DriverDetailsBroadcastService
{
    private readonly List<WebSocket> _clients = new();

    private readonly DriverDetailService driverDetailService;
    public DriverDetailsBroadcastService(DriverDetailService driverDetailService)
    {
        this.driverDetailService = driverDetailService;
    }

    public void AddClient(WebSocket socket)
    {
        lock (_clients)
        {
            _clients.Add(socket);
        }
    }

    public void RemoveClient(WebSocket socket)
    {
        lock (_clients)
        {
            _clients.Remove(socket);
        }
    }

    public async Task UpdateDrivers()
    {
        var drivers = this.driverDetailService.GetDriverBasicDetails();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var json = JsonSerializer.Serialize(drivers, options);
        var buffer = Encoding.UTF8.GetBytes(json);
        var segment = new ArraySegment<byte>(buffer);

        List<WebSocket> snapshot;
        lock (_clients) snapshot = _clients.ToList();

        foreach (var socket in snapshot)
        {
            if (socket.State == WebSocketState.Open)
            {
                try
                {
                    await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch
                {
                    RemoveClient(socket);
                }
            }
            else
            {
                RemoveClient(socket);
            }
        }
    }
}
