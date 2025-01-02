namespace F1GameDataParser.Models.CarStatus
{
    public class CarStatus
    {
        public Header Header { get; set; }
        public CarStatusDetails[] Details { get; set; } = new CarStatusDetails[Sizes.MaxPlayers];
    }
}