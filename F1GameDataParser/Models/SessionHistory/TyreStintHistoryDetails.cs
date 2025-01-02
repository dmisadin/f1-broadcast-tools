using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.SessionHistory
{
    public class TyreStintHistoryDetails
    {
        public byte EndLap { get; set; } // 255 for current tyre
        public TyreCompoundActual TyreActualCompound { get; set; }
        public TyreCompoundVisual TyreVisualCompound { get; set; }
    }
}
