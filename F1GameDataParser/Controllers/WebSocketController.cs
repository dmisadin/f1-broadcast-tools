using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using F1GameDataParser.Mapping.ViewModelFactories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers;
public class WebSocketController : ControllerBase
{
    private readonly TimingTowerFactory timingTowerBuilder;
    public WebSocketController(TimingTowerFactory timingTowerBuilder)
    {
        this.timingTowerBuilder = timingTowerBuilder;
    }

    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await StreamData(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task StreamData(WebSocket webSocket)
    {
        try
        {
            while (!webSocket.CloseStatus.HasValue)
            {
                // Create the object to send
                var data = this.timingTowerBuilder.Generate();

                // Serialize the object to JSON
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonData = JsonSerializer.Serialize(data, options);
                var buffer = Encoding.UTF8.GetBytes(jsonData);

                // Send the JSON data to the client
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);

                // Wait for 0.1 seconds
                await Task.Delay(100);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket error: {ex.Message}");
        }
        finally
        {
            // Ensure the connection is closed
            await webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Connection closed",
                CancellationToken.None);
        }
    }
}
