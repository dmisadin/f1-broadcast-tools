using F1GameDataParser.Enums;

namespace F1GameDataParser.Database.Dtos
{
    public class DriverOverrideDto
    {
        public int Id { get; set; }
        public byte RacingNumber { get; set; }
        public string Name { get; set; }
        public byte Position { get; set; }
        public Team Team { get; set; }

        public int? PlayerId { get; set; }
        public DriverPlayerDto? Player { get; set; }
    }
}
