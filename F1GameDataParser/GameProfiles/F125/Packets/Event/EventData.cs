using F1GameDataParser.Enums;
using F1GameDataParser.Enums.Event;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F125.Packets.Event
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FastestLap
    {
        public byte vehicleIdx;
        public float lapTime;// Lap time in seconds
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Retirement
    {
        public byte vehicleIdx;
        public ResultReason reason;     // Added in F1 25
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DRSDisabled           // Added in F1 25
    {
        public DRSDisabledReason reason;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TeamMateInPits
    {
        public byte vehicleIdx;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RaceWinner
    {
        public byte vehicleIdx;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Penalty
    {
        public PenaltyType penaltyType;
        public InfringementType infringementType;
        public byte vehicleIdx;
        public byte otherVehicleIdx;
        public byte time;
        public byte lapNum;
        public byte placesGained;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SpeedTrap
    {
        public byte vehicleIdx;
        public float speed; // Top speed achieved in kilometers per hour

        /// <summary>
        /// Overall fastest speed in session
        /// 1 = in session; 0 = otherwise
        /// </summary>
        public byte isOverallFastestInSession;

        /// <summary>
        /// Fastest speed for driver in session
        /// 1 = in session; 0 = otherwise
        /// </summary>
        public byte isDriverFastestInSession;

        /// <summary>
        /// Index of the fastest car in the session
        /// </summary>
        public byte fastestVehicleIdxInSession;

        /// <summary>
        /// Speed of the fastest car in the session
        /// </summary>
        public float fastestSpeedInSession;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StartLights
    {
        public byte numLights;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DriveThroughPenaltyServed
    {
        public byte vehicleIdx;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StopGoPenaltyServed
    {
        public byte vehicleIdx;
        public float stopTime; // Added in F1 25
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Flashback
    {
        public uint flashbackFrameIdentifier;
        public float flashbackSessionTime;
    }

    /// <summary>
    /// Button event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Buttons
    {
        public Button buttonStatus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Overtake
    {
        public byte overtakingVehicleIdx;
        public byte beingOvertakenVehicleIdx;
    }

    // F1 24
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SafetyCar
    {
        public SafetyCarType safetyCarType;
        public SafetyCarEventType eventType;
    }

    // F1 24
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Collision
    {
        public byte vehicle1Idx;
        public byte vehicle2Idx;
    }


    [StructLayout(LayoutKind.Explicit)]
    public struct EventData
    {
        [FieldOffset(0)]
        public FastestLap fastestLap;

        [FieldOffset(0)]
        public Retirement retirement;

        [FieldOffset(0)]
        public DRSDisabled drsDisabled;

        [FieldOffset(0)]
        public TeamMateInPits teamMateInPits;

        [FieldOffset(0)]
        public RaceWinner raceWinner;

        [FieldOffset(0)]
        public Penalty penalty;

        [FieldOffset(0)]
        public SpeedTrap speedTrap;

        [FieldOffset(0)]
        public StartLights startLights;

        [FieldOffset(0)]
        public DriveThroughPenaltyServed driveThroughPenaltyServed;

        [FieldOffset(0)]
        public StopGoPenaltyServed stopGoPenaltyServed;

        [FieldOffset(0)]
        public Flashback flashback;

        [FieldOffset(0)]
        public Buttons buttons;

        [FieldOffset(0)]
        public Overtake overtake;

        [FieldOffset(0)]
        public SafetyCar safetyCar;

        [FieldOffset(0)]
        public Collision collsion;
    }
}
