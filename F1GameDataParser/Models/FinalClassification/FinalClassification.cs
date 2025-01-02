namespace F1GameDataParser.Models.FinalClassification
{
    public class FinalClassification
    {
        public Header Header { get; set; }
        public byte NumCars { get; set; }
        public FinalClassificationDetails[] Details { get; set; } = new FinalClassificationDetails[Sizes.MaxPlayers];
    }
}
