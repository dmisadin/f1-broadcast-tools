namespace F1GameDataParser.ViewModels.SectorTimingComparison
{
    public class SectorTimingComparison
    {
        public int LapNumber { get; set; }
        public DriverPreviousLapDetails DriverPreviousLapDetails { get; set; } 
        public DriverPreviousLapDetails ComparingDriverPreviousLapDetails { get; set; } 
    }
}
