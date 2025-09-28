using F1GameDataParser.Enums;
using F1GameDataParser.Enums.Session;
using F1GameDataParser.GameProfiles.F1Common.Packets;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.Session
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SessionPacket
    {
        public PacketHeader header;
        public Weather weather;
        public sbyte trackTemperature; // celsius
        public sbyte airTemperature; // celsius
        public byte totalLaps;
        public ushort trackLength;
        public SessionType sessionType;
        public Track trackId;
        public RacingCategory formula;
        public ushort sessionTimeLeft; // seconds
        public ushort sessionDuration; // seconds
        public byte pitSpeedLimit; // km/h
        public byte gamePaused;
        public byte isSpectating;
        public byte spectatorCarIndex;
        public ActiveState sliProNativeSupport;
        public byte numMarshalZones;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public MarshalZoneData[] marshalZones;

        public SafetyCarStatus safetyCarStatus;
        public NetworkGame networkGame;
        public byte numWeatherForecastSamples;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // F1 24: 56 -> 64
        public WeatherForecastSampleData[] weatherForecastSamples;

        public ForecastAccuracy forecastAccuracy;
        public byte aiDifficulty; // [0, 110]
        public uint seasonLinkIdentifier;
        public uint weekendLinkIdentifier;
        public uint sessionLinkIdentifier;
        public byte pitStopWindowIdealLap;
        public byte pitStopWindowLatestLap;
        public byte pitStopRejoinPosition;
        public Assist steeringAssist;
        public AssistBraking brakingAssist;
        public AssistGearbox gearboxAssist;
        public Assist pitAssist;
        public Assist ERSAssist;
        public Assist DRSAssist;
        public AssistDynamicRacingLine dynamicRacingLine;
        public AssistDynamicRacingLineType dynamicRacingLineType;
        public GameMode gameMode;
        public Ruleset ruleset;
        public uint timeOfDay; // Local time of day - minutes since midnight
        public SessionLength sessionLength;
        public SpeedUnit speedUnitsLeadPlayer;
        public TemperatureUnit temperatureUnitsLeadPlayer;
        public SpeedUnit speedUnitsSecondaryPlayer;
        public TemperatureUnit temperatureUnitsSecondaryPlayer;
        public byte numSafetyCarPeriods;
        public byte numVirutalSafetyCarPeriods;

        // Added in F1 24
        public byte numRedFlagPeriods;
        public Toggle equalCarPerformance;
        public RecoveryMode recoveryMode;
        public FlashbackLimit flashbackLimit;
        public SurfaceType surfaceType;
        public LowFuelMode lowFuelMode;
        public RaceStart raceStarts;
        public TyreTemperature tyreTemperature;
        public Toggle pitLaneTyreSim;
        public Enums.Session.CarDamage carDamage;
        public CarDamageRate carDamageRate;
        public Collision collisions;
        public Toggle collisionsOffForFirstLapOnly;
        public Toggle mpUnsafePitRelease; // Multiplayer
        public Toggle mpOffForGriefing; // Multiplayer
        public CornerCuttingStringency cornerCuttingStringency;
        public Toggle parcFermeRules;
        public PitStopExperience pitStopExperience;
        public RuleFrequency safetyCar;
        public IntermissionExperience safetyCarExperience;
        public Toggle formationLap;
        public IntermissionExperience formationLapExperience;
        public RuleFrequency redFlags;
        public Toggle affectsLicenceLevelSolo;
        public Toggle affectsLicenceLevelMP;
        public byte numSessionsInWeekend;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] weekendStructure;

        public float sector2LapDistanceStart;
        public float sector3LapDistanceStart;
    }
}
