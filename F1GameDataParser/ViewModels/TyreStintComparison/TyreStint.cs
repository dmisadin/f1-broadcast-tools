using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Constants;

namespace F1GameDataParser.ViewModels.TyreStintComparison
{
    public class TyreStint
    {
        public string TyreCompound { get; set; } = string.Empty;
        public string TyreColor { get; set; } = Tyres.Colors.GetValueOrDefault(TyreCompoundVisual.Hard) ?? "#fff";
        public byte StartLap { get; set; } = 1;
        public byte? EndLap { get; set; }
        public byte Duration { get; set; }
    }
}
