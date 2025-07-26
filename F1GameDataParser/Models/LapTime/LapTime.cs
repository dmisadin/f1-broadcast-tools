namespace F1GameDataParser.Models.LapTime
{
    public class LapTime : MergeableBase<LapTime>
    {
        public byte VehicleIdx { get; set; }
        public uint LapTimeInMS { get; set; }
        public ushort Sector1TimeInMS { get; set; }
        public ushort Sector2TimeInMS { get; set; }
        public ushort Sector3TimeInMS { get; set; }

        public bool? Sector1Changed { get; set; }
        public bool? Sector2Changed { get; set; }
        public bool? Sector3Changed { get; set; }
        public bool? LapTimeChanged { get; set; }
    }
}
