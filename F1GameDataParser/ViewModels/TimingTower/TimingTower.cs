using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.TimingTower
{
    public class TimingTower
    {
        public byte CurrentLap { get; set; }
        public byte TotalLaps { get; set; }

        public SafetyCarStatus SafetyCarStatus { get; set; }
        public IEnumerable<bool> SectorYellowFlags { get; set; }

        public IEnumerable<DriverTimingDetails> DriverTimingDetails { get; set; }
        public byte SpectatorCarIdx { get; set; }
    }
}
