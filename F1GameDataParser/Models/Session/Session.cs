using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.Session
{
    public class Session : MergeableBase<Session>
    {
        public Header Header { get; set; }
        public Weather Weather { get; set; }
        public sbyte TrackTemperature { get; set; } // Celsius
        public sbyte AirTemperature { get; set; } // Celsius
        public byte TotalLaps { get; set; }
        public ushort TrackLength { get; set; }
        public SessionType SessionType { get; set; }
        public Track TrackId { get; set; }
        public RacingCategory Formula { get; set; }
        public ushort SessionTimeLeft { get; set; } // Seconds
        public ushort SessionDuration { get; set; } // Seconds
        public byte PitSpeedLimit { get; set; } // km/h
        public byte GamePaused { get; set; }
        public byte IsSpectating { get; set; }
        public byte SpectatorCarIndex { get; set; }
        public ActiveState SliProNativeSupport { get; set; }
        public byte NumMarshalZones { get; set; }

        public IEnumerable<MarshalZone> MarshalZones { get; set; }

        public SafetyCarStatus SafetyCarStatus { get; set; }
        public NetworkGame NetworkGame { get; set; }
        public byte NumWeatherForecastSamples { get; set; }
        public IEnumerable<WeatherForecastSample> WeatherForecastSamples { get; set; }
        public ForecastAccuracy ForecastAccuracy { get; set; }
        public byte AiDifficulty { get; set; } // [0, 110]
        public uint SeasonLinkIdentifier { get; set; }
        public uint WeekendLinkIdentifier { get; set; }
        public uint SessionLinkIdentifier { get; set; }
        public byte PitStopWindowIdealLap { get; set; }
        public byte PitStopWindowLatestLap { get; set; }
        public byte PitStopRejoinPosition { get; set; }
        public Assist SteeringAssist { get; set; }
        public AssistBraking BrakingAssist { get; set; }
        public AssistGearbox GearboxAssist { get; set; }
        public Assist PitAssist { get; set; }
        public Assist ERSAssist { get; set; }
        public Assist DRSAssist { get; set; }
        public AssistDynamicRacingLine DynamicRacingLine { get; set; }
        public AssistDynamicRacingLineType DynamicRacingLineType { get; set; }
        public GameMode GameMode { get; set; }
        public Ruleset Ruleset { get; set; }
        public uint TimeOfDay { get; set; } // Local time of day - minutes since midnight
        public SessionLength SessionLength { get; set; }
        public SpeedUnit SpeedUnitsLeadPlayer { get; set; }
        public TemperatureUnit TemperatureUnitsLeadPlayer { get; set; }
        public SpeedUnit SpeedUnitsSecondaryPlayer { get; set; }
        public TemperatureUnit TemperatureUnitsSecondaryPlayer { get; set; }
        public byte NumSafetyCarPeriods { get; set; }
        public byte NumVirtualSafetyCarPeriods { get; set; }
        public byte Tbc { get; set; } // tbc - wtf?
    }
}
