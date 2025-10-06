namespace F1GameDataParser.ViewModels.LastLapSectorComparison
{
    public class PreviousLapSectorComparison
    {
        public int LapNumber { get; set; }
        public DriverPreviousLapDetails DriverPreviousLapDetails { get; set; } 
        public DriverPreviousLapDetails ComparingDriverPreviousLapDetails { get; set; } 
    }
}
