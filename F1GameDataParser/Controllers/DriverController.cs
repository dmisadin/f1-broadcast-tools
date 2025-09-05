using F1GameDataParser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace F1GameDataParser.Controllers;

[Route("ws/driver")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly DriverDetailsBroadcastService driverDetailsBroadcastService;
    private readonly DriverDetailService driverDetailService;

    public DriverController(DriverDetailsBroadcastService driverDetailsBroadcastService,
                            DriverDetailService driverDetailService )
    {
        this.driverDetailsBroadcastService = driverDetailsBroadcastService;
        this.driverDetailService = driverDetailService;
    }

    [Route("driver-details")]
    public async Task DriverDetails()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            // Add this connection
            driverDetailsBroadcastService.AddClient(socket);

            // Send initial snapshot
            await SendJson(socket, driverDetailService.GetDriverBasicDetails());

            // Keep alive until closed
            var buffer = new byte[1024];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    driverDetailsBroadcastService.RemoveClient(socket);
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
                }
            }
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    private static Task SendJson(WebSocket socket, object data)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(data, options);
        var buffer = Encoding.UTF8.GetBytes(json);
        var segment = new ArraySegment<byte>(buffer);

        return socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
    }
}
