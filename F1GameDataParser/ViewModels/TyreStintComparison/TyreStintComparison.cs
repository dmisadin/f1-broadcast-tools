namespace F1GameDataParser.ViewModels.TyreStintComparison
{
    public class TyreStintComparison
    {
        public DriverBasicDetails? Driver { get; set; }
        public IEnumerable<TyreStint> TyreStints { get; set; }
    }
}
