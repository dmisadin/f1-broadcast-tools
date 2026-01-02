using F1GameDataParser.Mapping.ViewModelFactories;
using F1GameDataParser.ViewModels.SectorTimingComparison;
using F1GameDataParser.ViewModels.SpeedTrapLeaderboard;
using F1GameDataParser.ViewModels.TyreStintComparison;
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
        private readonly SpeedTrapLeaderboardFactory speedTrapLeaderboardFactory;
        private readonly TyreStintComparisonFactory tyreStintComparisonFactory;

        public StaticWidgetController(WeatherForecastFactory weatherForecastFactory,
                                      SectorTimingComparisonFactory sectorTimingComparisonFactory,
                                      SpeedTrapLeaderboardFactory speedTrapLeaderboardFactory,
                                      TyreStintComparisonFactory tyreStintComparisonFactory)
        {
            this.weatherForecastFactory = weatherForecastFactory;
            this.sectorTimingComparisonFactory = sectorTimingComparisonFactory;
            this.speedTrapLeaderboardFactory = speedTrapLeaderboardFactory;
            this.tyreStintComparisonFactory = tyreStintComparisonFactory;
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

        [HttpGet("get-speed-trap-leaderboard")]
        public IList<SpeedTrapCar>? GetSpeedTrapLeaderboard()
        {
            return speedTrapLeaderboardFactory.GenerateList();
        }

        [HttpGet("get-tyre-stint-comparison")]
        public IList<TyreStintComparison>? GetTyreStintComparison()
        {
            return tyreStintComparisonFactory.GenerateList();
        }
    }
}
