namespace F1GameDataParser.Models.WidgetModels
{
    public class PreviousLapSectorComparisonModel : MergeableBase<PreviousLapSectorComparisonModel>
    {
        public byte? VehicleIdx { get; set; }
        public byte? ComparingVehicleIdx { get; set; }

        public int? LapNumber { get; set; }
    }
}
