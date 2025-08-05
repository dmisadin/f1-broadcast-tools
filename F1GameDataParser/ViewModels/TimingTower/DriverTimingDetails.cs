using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.TimingTower
{
    public class DriverTimingDetails : DriverDetails
    {
        public byte TyreAge { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public int Penalties { get; set; }
        public byte Warnings { get; set; }
        public bool HasFastestLap { get; set; }
        public bool IsInPits { get; set; }

        public byte NumPitStops { get; set; }
        public int PositionsGained { get; set; }
    }
}
