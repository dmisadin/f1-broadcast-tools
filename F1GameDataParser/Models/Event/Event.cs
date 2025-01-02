namespace F1GameDataParser.Models.Event
{
    public class Event
    {
        public Header Header { get; set; }
        public string EventStringCode { get; set; }
        public EventDetails<IEvent> EventDetails { get; set; }
    }
}
