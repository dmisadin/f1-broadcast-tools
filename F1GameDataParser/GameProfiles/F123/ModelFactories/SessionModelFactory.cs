using F1GameDataParser.Enums;
using F123Enum = F1GameDataParser.GameProfiles.F123.Enums;
using F1GameDataParser.GameProfiles.F123.Packets.Session;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Session;
using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F123.ModelFactories
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
                SessionType = ConvertSessionType(packet.sessionType),
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
                    SessionType = ConvertSessionType(sample.sessionType),
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
                PitReleaseAssist = packet.pitReleaseAssist,
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
                NumVirtualSafetyCarPeriods = packet.numVirutalSafetyCarPeriods
            };
        }

        public static SessionType ConvertSessionType(F123Enum.SessionType sessionType)
        {
            return sessionType switch
            {
                F123Enum.SessionType.Unknown => SessionType.Unknown,
                F123Enum.SessionType.Practice1 => SessionType.Practice1,
                F123Enum.SessionType.Practice2 => SessionType.Practice2,
                F123Enum.SessionType.Practice3 => SessionType.Practice3,
                F123Enum.SessionType.ShortPractice => SessionType.ShortPractice,
                F123Enum.SessionType.Q1 => SessionType.Q1,
                F123Enum.SessionType.Q2 => SessionType.Q2,
                F123Enum.SessionType.Q3 => SessionType.Q3,
                F123Enum.SessionType.ShortQualifying => SessionType.ShortQualifying,
                F123Enum.SessionType.OneShotQualifying => SessionType.OneShotQualifying,
                F123Enum.SessionType.Race => SessionType.Race,
                F123Enum.SessionType.Race2 => SessionType.Race2,
                F123Enum.SessionType.Race3 => SessionType.Race3,
                F123Enum.SessionType.TimeTrial => SessionType.TimeTrial,
                _ => SessionType.Unknown
            };
        }
    }
}
