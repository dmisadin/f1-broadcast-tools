using F1GameDataParser.Database.Entities.Widgets;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace F1GameDataParser.Controllers;

[ApiController]
[Route("ws")]
public class WebSocketController : ControllerBase
{
    private readonly WebSocketConnectionManager _connections;

    public WebSocketController(WebSocketConnectionManager connections)
    {
        _connections = connections;
    }

    [HttpGet("timing-tower")]
    public async Task ConnectTimingTower() => await HandleConnection(WidgetType.TimingTower);

    [HttpGet("minimap")]
    public async Task ConnectMinimap() => await HandleConnection(WidgetType.Minimap);

    [HttpGet("stopwatch-spectated")]
    public async Task ConnectStopwatchSpectated() => await HandleConnection(WidgetType.StopwatchSpectated);

    [HttpGet("stopwatch-list")]
    public async Task ConnectStopwatchList() => await HandleConnection(WidgetType.StopwatchList);

    [HttpGet("halo-telemetry")]
    public async Task ConnectHaloTelemetry() => await HandleConnection(WidgetType.HaloTelemetry);

    [HttpGet("speed-difference")]
    public async Task ConnectSpeedDifference() => await HandleConnection(WidgetType.SpeedDifference);

    private async Task HandleConnection(WidgetType widgetType)
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            HttpContext.Response.StatusCode = 400;
            return;
        }

        var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        _connections.AddSocket(widgetType, socket);
        Console.WriteLine($"[WebSocket] {widgetType} connected");

        var buffer = new byte[512];
        try
        {
            while (socket.State == WebSocketState.Open && !HttpContext.RequestAborted.IsCancellationRequested)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), HttpContext.RequestAborted);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;
            }
        }
        finally
        {
            _connections.RemoveSocket(widgetType, socket);
            try
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
            catch { }
            Console.WriteLine($"[WebSocket] {widgetType} disconnected");
        }
    }
}
