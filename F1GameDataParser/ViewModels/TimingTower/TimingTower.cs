using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.TimingTower
{
    public class TimingTower
    {
        public GameYear GameYear { get; set; }
        public bool IsRaceSession { get; set; }
        public bool IsSessionFinished { get; set; }
        public byte CurrentLap { get; set; }
        public byte TotalLaps { get; set; }

        public SafetyCarType SafetyCarStatus { get; set; }
        public IEnumerable<bool> SectorYellowFlags { get; set; }
        public AdditionalInfoType ShowAdditionalInfo { get; set; }
        public string? SessionTimeLeft { get; set; }

        public IEnumerable<DriverTimingDetails> DriverTimingDetails { get; set; }
        public byte SpectatorCarIdx { get; set; }
    }
}
