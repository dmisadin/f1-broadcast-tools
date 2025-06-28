namespace F1GameDataParser.Models.CarDamage
{
    public class CarDamage : MergeableBase<CarDamage>
    {
        public Header Header { get; set; }
        public CarDamageDetails[] CarDamageDetails { get; set; } = new CarDamageDetails[Sizes.MaxPlayers];
    }
}
