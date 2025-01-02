using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.Session
{
    public class MarshalZone
    {
        public float ZoneStart { get; set; } // Value [0,1] of the way through the lap
        public ZoneFlag ZoneFlag { get; set; }
    }

}
