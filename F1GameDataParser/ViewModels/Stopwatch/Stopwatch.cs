namespace F1GameDataParser.ViewModels.Stopwatch
{
    public class Stopwatch
    {
        public FastestQualifyingLap LeaderLap { get; set; }
        public IEnumerable<StopwatchCar> Cars { get; set; }
    }
}
