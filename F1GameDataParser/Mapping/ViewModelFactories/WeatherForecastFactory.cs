using F1GameDataParser.State;
using F1GameDataParser.ViewModels.WeatherForecast;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class WeatherForecastFactory : ViewModelFactoryBase<WeatherForecast>
    {
        private readonly SessionState sessionState;

        public WeatherForecastFactory(SessionState sessionState)
        {
            this.sessionState = sessionState;
        }

        public override WeatherForecast? Generate()
        {
            var sessionState = this.sessionState?.State;

            if (sessionState == null)
                return null;

            var timeOfDayInHours = sessionState.TimeOfDay / 60.0f;

            return new WeatherForecast
            {
                Samples = sessionState.WeatherForecastSamples
                            .Take(sessionState.NumWeatherForecastSamples)
                            .Select(s => new WeatherForecastSample
                            {
                                SessionType = s.SessionType,
                                TimeOffset = s.TimeOffset,
                                Weather = s.Weather,
                                RainPercentage = s.RainPercentage
                            }),
                IsNightRace = (timeOfDayInHours >= 18 && timeOfDayInHours < 24) || (timeOfDayInHours >= 0 &&  timeOfDayInHours < 6)
            };
        }
    }
}
