namespace F1GameDataParser.ViewModels.Stopwatch
{
    public class Stopwatch
    {
        public FastestQualifyingLap? FastestLap { get; set; }
        public FastestQualifyingLap? SecondFastestLap { get; set; }
        public IEnumerable<StopwatchCar> Cars { get; set; }
    }
}
