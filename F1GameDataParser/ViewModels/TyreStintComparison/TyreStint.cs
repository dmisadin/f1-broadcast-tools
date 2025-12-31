namespace F1GameDataParser.ViewModels.TyreStintComparison
{
    public class TyreStint
    {
        public string TyreCompound { get; set; } = string.Empty;
        public byte? EndLap { get; set; }
        public byte Duration { get; set; }
        public byte SizePercentage { get; set; }
    }
}
