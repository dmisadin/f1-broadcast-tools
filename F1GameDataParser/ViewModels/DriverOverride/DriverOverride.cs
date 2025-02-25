using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.DriverOverride
{
    public class DriverOverride
    {
        public int Id { get; set; }
        public byte RacingNumber { get; set; }
        public string Name { get; set; }
        public string NameOverride { get; set; }
        public byte Position { get; set; }
        public Team Team { get; set; }
    }
}
