namespace F1GameDataParser.ViewModels.WeatherForecast
{
    public class WeatherForecast
    {
        public IEnumerable<WeatherForecastSample> Samples { get; set; } = new List<WeatherForecastSample>();
        public bool IsNightRace { get; set; }
    }
}
