namespace F1GameDataParser.ViewModels.SectorTimingComparison
{
    public class DriverPreviousLapDetails
    {
        public byte Position { get; set; }
        public int VehicleIdx { get; set; }
        public string VisualTyreCompound { get; set; }
        public DriverBasicDetails Driver { get; set; }
        public LapTimingStatus LapTiming { get; set; }
    }
}
