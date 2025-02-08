using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.PlayerOverride
{
    public class PlayerOverride
    {
        public int Id { get; set; }
        public byte RacingNumber { get; set; }
        public string Name { get; set; }
        public string NameOverride { get; set; }
        public byte Position { get; set; }
        public Team Team { get; set; }
    }
}
