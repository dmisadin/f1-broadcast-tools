using F1GameDataParser.Database.Entities.Widgets;
using System.Collections.Concurrent;
using System.Net.WebSockets;

public class WebSocketConnectionManager
{
    private readonly ConcurrentDictionary<WidgetType, ConcurrentBag<WebSocket>> widgetSockets = new();

    public void AddSocket(WidgetType widget, WebSocket socket)
    {
        var sockets = widgetSockets.GetOrAdd(widget, _ => new ConcurrentBag<WebSocket>());
        sockets.Add(socket);
    }

    public void RemoveSocket(WidgetType widget, WebSocket socket)
    {
        if (widgetSockets.TryGetValue(widget, out var sockets))
        {
            var remaining = sockets.Where(s => s != socket).ToList();
            widgetSockets[widget] = new ConcurrentBag<WebSocket>(remaining);
        }
    }

    public IEnumerable<WebSocket> GetSockets(WidgetType widget)
    {
        return widgetSockets.TryGetValue(widget, out var sockets)
            ? sockets
            : Enumerable.Empty<WebSocket>();
    }

    public bool HasConnections(WidgetType widget)
    {
        return widgetSockets.TryGetValue(widget, out var sockets) && sockets.Any();
    }

    public IEnumerable<WidgetType> ActiveWidgets()
    {
        return widgetSockets
            .Where(kv => kv.Value.Any(s => s.State == WebSocketState.Open))
            .Select(kv => kv.Key);
    }
}
