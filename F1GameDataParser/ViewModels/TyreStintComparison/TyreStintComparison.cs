namespace F1GameDataParser.ViewModels.TyreStintComparison
{
    public class TyreStintComparison
    {
        public byte TotalLaps { get; set; }
        public byte TotalSizePercentage { get; set; }
        public IEnumerable<CarTyreStints> Cars { get; set; }
        public IEnumerable<byte> PitStopLapMarkers { get; set; }= new List<byte> { 1 };
    }
}
