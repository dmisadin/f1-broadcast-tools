namespace F1GameDataParser.ViewModels
{
    public class DriverDetails : DriverBasicDetails
    {
        public byte Position { get; set; }
        public string VisualTyreCompound { get; set; }
        public string GapInterval { get; set; }
        public string GapToLeader { get; set; }
    }
}
