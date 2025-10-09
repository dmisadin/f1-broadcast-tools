namespace F1GameDataParser.Models.WidgetModels
{
    public class SectorTimingComparisonModel : MergeableBase<SectorTimingComparisonModel>
    {
        public byte? VehicleIdx { get; set; }
        public byte? ComparingVehicleIdx { get; set; }

        public int? LapNumber { get; set; }
    }
}
