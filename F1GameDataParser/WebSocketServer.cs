using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;

public class WebSocketServer
{
    private readonly string _url;
    private readonly HttpListener _httpListener;
    private readonly ConcurrentBag<WebSocket> _clients;

    public WebSocketServer(string url)
    {
        _url = url;
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add(_url);
        _clients = new ConcurrentBag<WebSocket>();
    }

    public async Task StartAsync()
    {
        _httpListener.Start();
        Console.WriteLine($"WebSocket server started at {_url}");

        while (true)
        {
            var context = await _httpListener.GetContextAsync();

            if (context.Request.IsWebSocketRequest)
            {
                var webSocketContext = await context.AcceptWebSocketAsync(null);
                var webSocket = webSocketContext.WebSocket;
                _clients.Add(webSocket);

                Console.WriteLine("WebSocket connection established.");
                _ = HandleWebSocketAsync(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    private async Task HandleWebSocketAsync(WebSocket webSocket)
    {
        byte[] buffer = new byte[1024];
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("WebSocket connection closed.");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }
    }

    public async Task BroadcastMessageAsync(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        foreach (var client in _clients)
        {
            if (client.State == WebSocketState.Open)
            {
                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
