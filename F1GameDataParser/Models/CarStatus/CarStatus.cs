namespace F1GameDataParser.Models.CarStatus
{
    public class CarStatus : MergeableBase<CarStatus>
    {
        public Header Header { get; set; }
        public CarStatusDetails[] Details { get; set; } = new CarStatusDetails[Sizes.MaxPlayers];
    }
}