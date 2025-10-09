using F1GameDataParser.ViewModels.Enums;

namespace F1GameDataParser.ViewModels.SectorTimingComparison
{
    public class LapTimingStatus : LapTiming
    {
        public SectorTimeStatus? Sector1TimeStatus { get; set; }
        public SectorTimeStatus? Sector2TimeStatus { get; set; }
        public SectorTimeStatus? Sector3TimeStatus { get; set; }
        public SectorTimeStatus? LapTimeStatus { get; set; }
    }
}
