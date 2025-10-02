using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.ViewModels.LastLapSectorComparison;
using F1GameDataParser.ViewModels.WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/static-widget")]
    [ApiController]
    public class StaticWidgetController : ControllerBase
    {
        private readonly WeatherForecastFactory weatherForecastFactory;
        private readonly PreviousLapSectorComparisonFactory previousLapSectorComparisonFactory;

        public StaticWidgetController(WeatherForecastFactory weatherForecastFactory,
                                      PreviousLapSectorComparisonFactory previousLapSectorComparisonFactory)
        {
            this.weatherForecastFactory = weatherForecastFactory;
            this.previousLapSectorComparisonFactory = previousLapSectorComparisonFactory;
        }

        [HttpGet("weather-forecast")]
        public WeatherForecast? GetWeatherForecast()
        {
            return weatherForecastFactory.Generate();
        }

        [HttpGet("previous-lap-sector-comparison")]
        public PreviousLapSectorComparison? PreviousLapSectorComparison()
        {
            return previousLapSectorComparisonFactory.Generate();
        }
    }
}
