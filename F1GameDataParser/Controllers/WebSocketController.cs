using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using F1GameDataParser.Mapping.ViewModelFactories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers;

[ApiController]
public class WebSocketController : ControllerBase
{
    private readonly TimingTowerFactory timingTowerFactory;
    private readonly MinimapFactory minimapFactory;
    private readonly StopwatchFactory stopwatchFactory;
    private readonly HaloTelemetryDashboardFactory haloTelemetryDashboardFactory;

    public WebSocketController(TimingTowerFactory timingTowerFactory,
                                MinimapFactory minimapFactory,
                                StopwatchFactory stopwatchFactory,
                                HaloTelemetryDashboardFactory haloTelemetryDashboardFactory)
    {
        this.timingTowerFactory = timingTowerFactory;
        this.minimapFactory = minimapFactory;
        this.stopwatchFactory = stopwatchFactory;
        this.haloTelemetryDashboardFactory = haloTelemetryDashboardFactory;
    }

    [HttpGet("/ws/timing-tower")]
    public async Task GetTimingTower()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => timingTowerFactory.Generate(), HttpContext.RequestAborted);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("/ws/minimap")]
    public async Task GetMinimap()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => minimapFactory.Generate(), HttpContext.RequestAborted);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("/ws/stopwatch-spectated")]
    public async Task GetStopwatch()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => stopwatchFactory.GenerateSpectated(), HttpContext.RequestAborted);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("/ws/stopwatch-list")]
    public async Task GetStopwatchList()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => stopwatchFactory.Generate(), HttpContext.RequestAborted);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("/ws/halo-telemetry")]
    public async Task GetHaloTelemetry()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => haloTelemetryDashboardFactory.Generate(), HttpContext.RequestAborted);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task StreamData(WebSocket webSocket, Func<object?> dataGenerator, CancellationToken ct = default)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var recvBuffer = new byte[1]; // we ignore data, only detect Close frames
            Task<WebSocketReceiveResult> receiveTask = webSocket.ReceiveAsync(recvBuffer, ct);

            while (webSocket.State == WebSocketState.Open && !ct.IsCancellationRequested)
            {
                var data = dataGenerator();
                if (data is not null)
                {
                    var jsonData = JsonSerializer.Serialize(data, options);
                    var buffer = Encoding.UTF8.GetBytes(jsonData);

                    var sendTask = webSocket.SendAsync(
                        new ArraySegment<byte>(buffer),
                        WebSocketMessageType.Text,
                        endOfMessage: true,
                        ct);

                    // Race: either send finishes, receive sees a close, or our 100ms tick elapses
                    var completed = await Task.WhenAny(sendTask, receiveTask, Task.Delay(100, ct));

                    if (completed == receiveTask)
                    {
                        var result = await receiveTask;
                        if (result.MessageType == WebSocketMessageType.Close)
                            break;

                        // Not a close: arm next receive
                        receiveTask = webSocket.ReceiveAsync(recvBuffer, ct);
                    }
                    else if (completed == sendTask)
                    {
                        // propagate any send exception
                        await sendTask;
                    }
                }
                else
                {
                    // No data this tick—still check for a close frame with the same 100ms cadence
                    var completed = await Task.WhenAny(receiveTask, Task.Delay(100, ct));
                    if (completed == receiveTask)
                    {
                        var result = await receiveTask;
                        if (result.MessageType == WebSocketMessageType.Close)
                            break;

                        receiveTask = webSocket.ReceiveAsync(recvBuffer, ct);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            // request aborted or server shutting down
        }
        catch (WebSocketException wse)
        {
            Console.WriteLine($"WebSocket error: {wse.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        finally
        {
            try
            {
                if (webSocket.State is WebSocketState.Open or WebSocketState.CloseReceived)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                    Console.WriteLine($"[Websocket] Websocket closed.");
                }
            }
            catch { /* ignore close races */ }
        }
    }
}
