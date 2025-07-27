namespace F1GameDataParser.Models.ComputedModels
{
    public class DriverOnFlyingLap : MergeableBase<DriverOnFlyingLap>
    {
        public byte VehicleIdx { get; set; }
        public float LapDistance { get; set; }
        public bool MarkedForDeletion { get; set; } = false;
    }
}
