using F1GameDataParser.Enums;

namespace F1GameDataParser.GameProfiles.F1Common.Constants
{
    public static class Tyres
    {
        public static readonly Dictionary<TyreCompoundVisual, string> Colors = new Dictionary<TyreCompoundVisual, string>
        {
            { TyreCompoundVisual.Soft, "#B33C33" },
            { TyreCompoundVisual.Medium, "#E6BD11" },
            { TyreCompoundVisual.Hard, "#FFFFFF" },
            { TyreCompoundVisual.Inter, "#286837" },
            { TyreCompoundVisual.Wet, "#296284" },
        };
    }
}
