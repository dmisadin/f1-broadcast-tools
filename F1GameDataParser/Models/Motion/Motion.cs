
namespace F1GameDataParser.Models.Motion
{
    public class Motion
    {
        public Header Header { get; set; }
        public CarMotionDetails[] CarMotionDetails { get; set; } = new CarMotionDetails[Sizes.MaxPlayers];
    }
}
