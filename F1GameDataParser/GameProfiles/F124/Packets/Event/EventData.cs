﻿using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.Event
{
    /// <summary>
    /// Fastest lap event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FastestLap
    {
        /// <summary>
        /// Index of the car that achieved the fastest lap
        /// </summary>
        public byte vehicleIdx;

        /// <summary>
        /// Lap time in seconds
        /// </summary>
        public float lapTime;
    }

    /// <summary>
    /// Car retirement event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Retirement
    {
        /// <summary>
        /// Index of the car retiring
        /// </summary>
        public byte vehicleIdx;
    }

    /// <summary>
    /// Team mate in pits event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TeamMateInPits
    {
        /// <summary>
        /// Index of the car in pits
        /// </summary>
        public byte vehicleIdx;
    }

    /// <summary>
    /// Race winner event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RaceWinner
    {
        /// <summary>
        /// Index of the car that won the race
        /// </summary>
        public byte vehicleIdx;
    }

    /// <summary>
    /// Penalty event
    /// </summary>
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

    /// <summary>
    /// Speed trap event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SpeedTrap
    {
        /// <summary>
        /// Index of the car triggering the event
        /// </summary>
        public byte vehicleIdx;

        /// <summary>
        /// Top speed achieved in kilometers per hour
        /// </summary>
        public float speed;

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

    /// <summary>
    /// Start lights event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StartLights
    {
        /// <summary>
        /// Number of lights showing
        /// </summary>
        public byte numLights;
    }

    /// <summary>
    /// Drive through served event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DriveThroughPenaltyServed
    {
        /// <summary>
        /// Index of the car serving the drive through
        /// </summary>
        public byte vehicleIdx;
    }

    /// <summary>
    /// Stop &amp; go served event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StopGoPenaltyServed
    {
        /// <summary>
        /// Index of the car serving the stop &amp; go
        /// </summary>
        public byte vehicleIdx;
    }

    /// <summary>
    /// Flashback event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Flashback
    {
        /// <summary>
        /// Frame identifier flashed back to
        /// </summary>
        public uint flashbackFrameIdentifier;

        /// <summary>
        /// Session time flashed back to
        /// </summary>
        public float flashbackSessionTime;
    }

    /// <summary>
    /// Button event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Buttons
    {
        /// <summary>
        /// Bit flags specifying which buttons are being pressed currently
        /// </summary>
        public Button buttonStatus;
    }

    /// <summary>
    /// Overtake event
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Overtake
    {
        /// <summary>
        /// Index of the car overtaking
        /// </summary>
        public byte overtakingVehicleIdx;

        /// <summary>
        /// Index of the car being overtaken
        /// </summary>
        public byte beingOvertakenVehicleIdx;
    }

    // Added in F1 24
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SafetyCar
    {
        public SafetyCarType safetyCarType;
        public SafetyCarEventType eventType;
    }

    // Added in F1 24
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Collision
    {
        public byte vehicle1Idx;
        public byte vehicle2Idx;
    }

    /// <summary>
    /// The event details packet is different for each type of event.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct EventData
    {
        [FieldOffset(0)]
        public FastestLap fastestLap;

        [FieldOffset(0)]
        public Retirement retirement;

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
