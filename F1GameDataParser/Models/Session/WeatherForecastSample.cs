using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.Session
{
    public class WeatherForecastSample
    {
        public SessionType SessionType { get; set; }
        public byte TimeOffset { get; set; }
        public Weather Weather { get; set; }
        public sbyte TrackTemperature { get; set; }
        public ChangeType TrackTemperatureChange { get; set; }
        public sbyte AirTemperature { get; set; }
        public ChangeType AirTemperatureChange { get; set; }
        public byte RainPercentage { get; set; }
    }
}
