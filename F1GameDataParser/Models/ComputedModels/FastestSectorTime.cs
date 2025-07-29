using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.ComputedModels
{
    public class FastestSectorTime : MergeableBase<FastestSectorTime>
    {
        public Sector Sector { get; set; }
        public byte VehicleIdx { get; set; }
        public ushort TimeInMS { get; set; } = ushort.MaxValue;
        public ushort PreviousTimeInMS { get; set; } = ushort.MaxValue;
    }
}
