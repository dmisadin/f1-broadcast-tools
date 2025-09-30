using F1GameDataParser.Enums;

namespace F1GameDataParser.ViewModels.WeatherForecast
{
    public class WeatherForecastSample
    {
        public SessionType SessionType { get; set; }
        public byte TimeOffset { get; set; }
        public Weather Weather { get; set; }
        public byte RainPercentage { get; set; }
    }
}
