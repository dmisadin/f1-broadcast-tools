namespace F1GameDataParser.Models.ComputedModels
{
    public class PersonalBestLap : MergeableBase<PersonalBestLap>
    {
        public byte VehicleIdx { get; set; }
        public uint LapTimeInMS { get; set; }
        public ushort Sector1TimeInMS { get; set; }
        public ushort Sector2TimeInMS { get; set; }
        public ushort Sector3TimeInMS { get; set; }
        public PersonalBestLap? PreviousBestLap { get; set; }
    }
}
