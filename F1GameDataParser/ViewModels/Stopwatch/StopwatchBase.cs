using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.Stopwatch
{
    public class StopwatchBase
    {
        public GameYear GameYear { get; set; }
        public FastestQualifyingLap? FastestLap { get; set; }
        public FastestQualifyingLap? SecondFastestLap { get; set; }
    }
}
