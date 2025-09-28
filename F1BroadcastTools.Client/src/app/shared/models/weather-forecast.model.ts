import { SessionType, Weather } from "./Enumerations";

export interface WeatherForecast {
    samples: WeatherForecastSample[];
    timeOfDay: number;
}

export interface WeatherForecastSample {
    sessionType: SessionType;
    timeOffset: number;
    weather: Weather;
    rainPercentage: number;
}