namespace F1GameDataParser.Models.WidgetModels
{
    public class SpeedTrapLeaderboardModel : MergeableBase<SpeedTrapLeaderboardModel>
    {
        public List<int> SelectedVehicles { get; set; } = new List<int>();
    }
}
