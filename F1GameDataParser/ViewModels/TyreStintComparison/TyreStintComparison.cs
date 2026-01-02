namespace F1GameDataParser.ViewModels.TyreStintComparison
{
    public class TyreStintComparison
    {
        public byte TotalLaps { get; set; }
        public byte TotalSizePercentage { get; set; }
        public DriverBasicDetails? Driver { get; set; }
        public IEnumerable<TyreStint> TyreStints { get; set; }
    }
}
