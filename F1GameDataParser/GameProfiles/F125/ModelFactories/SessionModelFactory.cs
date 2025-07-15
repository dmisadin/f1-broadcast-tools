using F1GameDataParser.GameProfiles.F125.Packets.Session;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Session;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F125.ModelFactories
{
    public class SessionModelFactory : ModelFactoryBase<SessionPacket, Session>
    {
        public override Expression<Func<SessionPacket, Session>> ToModelExpression()
        {
            return packet => new Session
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                Weather = packet.weather,
                TrackTemperature = packet.trackTemperature,
                AirTemperature = packet.airTemperature,
                TotalLaps = packet.totalLaps,
                TrackLength = packet.trackLength,
                SessionType = packet.sessionType,
                TrackId = packet.trackId,
                Formula = packet.formula,
                SessionTimeLeft = packet.sessionTimeLeft,
                SessionDuration = packet.sessionDuration,
                PitSpeedLimit = packet.pitSpeedLimit,
                GamePaused = packet.gamePaused,
                IsSpectating = packet.isSpectating,
                SpectatorCarIndex = packet.spectatorCarIndex,
                SliProNativeSupport = packet.sliProNativeSupport,
                NumMarshalZones = packet.numMarshalZones,
                MarshalZones = packet.marshalZones.Select(zone => new MarshalZone
                {
                    ZoneStart = zone.zoneStart,
                    ZoneFlag = zone.zoneFlag,
                }),
                SafetyCarStatus = packet.safetyCarStatus,
                NetworkGame = packet.networkGame,
                NumWeatherForecastSamples = packet.numWeatherForecastSamples,
                WeatherForecastSamples = packet.weatherForecastSamples.Select(sample => new WeatherForecastSample
                {
                    SessionType = sample.sessionType,
                    TimeOffset = sample.timeOffset,
                    Weather = sample.weather,
                    TrackTemperature = sample.trackTemperature,
                    TrackTemperatureChange = sample.trackTemperatureChange,
                    AirTemperature = sample.airTemperature,
                    AirTemperatureChange = sample.airTemperatureChange,
                    RainPercentage = sample.rainPercentage,
                }),
                ForecastAccuracy = packet.forecastAccuracy,
                AiDifficulty = packet.aiDifficulty,
                SeasonLinkIdentifier = packet.seasonLinkIdentifier,
                WeekendLinkIdentifier = packet.weekendLinkIdentifier,
                SessionLinkIdentifier = packet.sessionLinkIdentifier,
                PitStopWindowIdealLap = packet.pitStopWindowIdealLap,
                PitStopWindowLatestLap = packet.pitStopWindowLatestLap,
                PitStopRejoinPosition = packet.pitStopRejoinPosition,
                SteeringAssist = packet.steeringAssist,
                BrakingAssist = packet.brakingAssist,
                GearboxAssist = packet.gearboxAssist,
                PitAssist = packet.pitAssist,
                ERSAssist = packet.ERSAssist,
                DRSAssist = packet.DRSAssist,
                DynamicRacingLine = packet.dynamicRacingLine,
                DynamicRacingLineType = packet.dynamicRacingLineType,
                GameMode = packet.gameMode,
                Ruleset = packet.ruleset,
                TimeOfDay = packet.timeOfDay,
                SessionLength = packet.sessionLength,
                SpeedUnitsLeadPlayer = packet.speedUnitsLeadPlayer,
                TemperatureUnitsLeadPlayer = packet.temperatureUnitsLeadPlayer,
                SpeedUnitsSecondaryPlayer = packet.speedUnitsSecondaryPlayer,
                TemperatureUnitsSecondaryPlayer = packet.temperatureUnitsSecondaryPlayer,
                NumSafetyCarPeriods = packet.numSafetyCarPeriods,
                NumVirtualSafetyCarPeriods = packet.numVirutalSafetyCarPeriods,
            };
        }
    }
}
