namespace F1GameDataParser.ViewModels.SessionEvent
{
    public class SessionEvent
    {
        public uint Id { get; set; }
        public DriverBasicDetails? Driver { get; set; }
        public DriverBasicDetails? InvolvedDriver { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
