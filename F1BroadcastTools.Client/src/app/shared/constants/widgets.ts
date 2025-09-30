import { WidgetType } from "../dtos/widget.dto";
import { SessionType, Weather } from "../models/Enumerations";

export const WidgetDetails: Record<WidgetType, string> = {
    [WidgetType.TimingTower]: "Timing Tower",
    [WidgetType.Stopwatch]: "Stopwatch",
    [WidgetType.Minimap]: "Minimap",
    [WidgetType.HaloHUD]: "Halo Telemetry",
};

export const WeatherLabels: Record<Weather, string> = {
    [Weather.Clear]: "Clear",
    [Weather.LightCloud]: "Light Cloud",
    [Weather.Overcast]: "Overcast",
    [Weather.LightRain]: "Light Rain",
    [Weather.HeavyRain]: "Heavy Rain",
    [Weather.Storm]: "Storm"
}

export const SessionShortLabels: Record<SessionType, string> = {
    [SessionType.Unknown]: "unknown",
    [SessionType.Practice1]: "P1",
    [SessionType.Practice2]: "P2",
    [SessionType.Practice3]: "P3",
    [SessionType.ShortPractice]: "SP",
    [SessionType.Q1]: "Q1",
    [SessionType.Q2]: "Q2",
    [SessionType.Q3]: "Q3",
    [SessionType.ShortQualifying]: "SQ",
    [SessionType.OneShotQualifying]: "OSQ",
    [SessionType.SprintShootout1]: "SS1",
    [SessionType.SprintShootout2]: "SS2",
    [SessionType.SprintShootout3]: "SS3",
    [SessionType.ShortSprintShootout]: "SSS",
    [SessionType.OneShotSprintShootout]: "OSSS",
    [SessionType.Race]: "Race",
    [SessionType.Race2]: "Race 2",
    [SessionType.Race3]: "Race 3",
    [SessionType.TimeTrial]: "TT"
}