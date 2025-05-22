using F1GameDataParser.Database.Entities;

namespace F1GameDataParser.Models.DriverOverride
{
    public class DriverOverride
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
