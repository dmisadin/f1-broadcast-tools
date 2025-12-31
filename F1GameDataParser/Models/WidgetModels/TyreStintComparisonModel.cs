namespace F1GameDataParser.Models.WidgetModels
{
    public class TyreStintComparisonModel : MergeableBase<TyreStintComparisonModel>
    {
        public List<int> SelectedVehicles { get; set; } = new List<int>();
    }
}
