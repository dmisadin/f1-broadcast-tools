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

    public WebSocketController(TimingTowerFactory timingTowerFactory, 
                                MinimapFactory minimapFactory,
                                StopwatchFactory stopwatchFactory)
    {
        this.timingTowerFactory = timingTowerFactory;
        this.minimapFactory = minimapFactory;
        this.stopwatchFactory = stopwatchFactory;
    }

    [HttpGet("/ws/timing-tower")]
    public async Task GetTimingTower()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => timingTowerFactory.Generate());
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
            await StreamData(webSocket, () => minimapFactory.Generate());
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("/ws/stopwatch")]
    public async Task GetStopwatch()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket, () => stopwatchFactory.Generate());
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task StreamData(WebSocket webSocket, Func<object> dataGenerator)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            while (!webSocket.CloseStatus.HasValue)
            {
                var data = dataGenerator();

                if (data == null) continue;

                var jsonData = JsonSerializer.Serialize(data, options);
                var buffer = Encoding.UTF8.GetBytes(jsonData);

                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);

                await Task.Delay(100);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket error: {ex.Message}");
        }
        finally
        {
            await webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Connection closed",
                CancellationToken.None);
        }
    }
}
