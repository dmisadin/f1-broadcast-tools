using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.ViewModels.SectorTimingComparison;
using F1GameDataParser.ViewModels.WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/static-widget")]
    [ApiController]
    public class StaticWidgetController : ControllerBase
    {
        private readonly WeatherForecastFactory weatherForecastFactory;
        private readonly SectorTimingComparisonFactory sectorTimingComparisonFactory;

        public StaticWidgetController(WeatherForecastFactory weatherForecastFactory,
                                      SectorTimingComparisonFactory sectorTimingComparisonFactory)
        {
            this.weatherForecastFactory = weatherForecastFactory;
            this.sectorTimingComparisonFactory = sectorTimingComparisonFactory;
        }

        [HttpGet("weather-forecast")]
        public WeatherForecast? GetWeatherForecast()
        {
            return weatherForecastFactory.Generate();
        }

        [HttpGet("previous-lap-sector-comparison")]
        public SectorTimingComparison? PreviousLapSectorComparison()
        {
            return sectorTimingComparisonFactory.Generate();
        }
    }
}
