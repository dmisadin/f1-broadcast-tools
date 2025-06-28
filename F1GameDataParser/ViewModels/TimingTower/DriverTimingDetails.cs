using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123;

namespace F1GameDataParser.ViewModels.TimingTower
{
    public class DriverTimingDetails
    {
        public int VehicleIdx { get; set; }
        public byte Position { get; set; }
        public Team TeamId { get; set; }
        public TeamDetails? TeamDetails { get; set; }
        public string Name { get; set; }
        public byte TyreAge { get; set; }
        public string VisualTyreCompound { get; set; }
        public string Gap { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public byte Penalties { get; set; }
        public byte Warnings { get; set; }
        public bool HasFastestLap { get; set; }
        public bool IsInPits { get; set; }

        public byte NumPitStops { get; set; }
        public int PositionsGained { get; set; }
    }
}
