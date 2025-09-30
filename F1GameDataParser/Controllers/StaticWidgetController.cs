using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.ViewModels.WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/static-widget")]
    [ApiController]
    public class StaticWidgetController : ControllerBase
    {
        private readonly WeatherForecastFactory weatherForecastFactory;

        public StaticWidgetController(WeatherForecastFactory weatherForecastFactory)
        {
            this.weatherForecastFactory = weatherForecastFactory;
        }

        [HttpGet("weather-forecast")]
        public WeatherForecast? GetWeatherForecast()
        {
            return weatherForecastFactory.Generate();
        }
    }
}
