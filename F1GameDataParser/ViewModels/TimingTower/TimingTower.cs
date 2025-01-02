namespace F1GameDataParser.ViewModels.TimingTower
{
    public class TimingTower
    {
        public byte CurrentLap { get; set; }
        public byte TotalLaps { get; set; }
        // FIA Flag color and sectors under flag
        // Safety car...

        public IEnumerable<DriverTimingDetails> DriverTimingDetails { get; set; }
    }
}
