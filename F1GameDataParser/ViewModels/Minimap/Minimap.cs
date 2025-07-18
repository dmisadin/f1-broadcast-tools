using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.Minimap
{
    public class Minimap
    {
        public Track TrackId { get; set; }
        public IEnumerable<MinimapCar> Cars { get; set; }
        public byte SpectatorCarIdx { get; set; }
        public int? Rotation { get; set; }
    }
}
