import { SessionType, Weather } from "./Enumerations";

export interface WeatherForecast {
    samples: WeatherForecastSample[];
    isNightRace: boolean;
}

export interface WeatherForecastSample {
    sessionType: SessionType;
    timeOffset: number;
    weather: Weather;
    rainPercentage: number;
}