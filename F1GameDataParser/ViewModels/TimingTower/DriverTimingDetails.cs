using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.TimingTower
{
    public class DriverTimingDetails
    {
        public int VehicleIdx { get; set; }
        public byte Position { get; set; }
        public Team TeamId { get; set; }
        public string Name { get; set; }
        public byte TyreAge { get; set; }
        public string VisualTyreCompound { get; set; }
        public string GapOrResultStatus { get; set; }
        public byte Penalties { get; set; }
        public byte Warnings { get; set; }
        public bool HasFastestLap { get; set; }
    }
}
