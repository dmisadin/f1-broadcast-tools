using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.Lap
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LapData
    {
        public uint lastLapTimeInMS;
        public uint currentLapTimeInMS;
        public ushort sector1TimeInMS; // Sector 1 time in milliseconds
        public byte sector1TimeMinutes; // Sector 1 whole minute part
        public ushort sector2TimeInMS; // Sector 2 time in milliseconds
        public byte sector2TimeMinutes; // Sector 2 whole minute part

        public ushort deltaToCarInFrontInMS;
        public byte deltaToCarInFrontMinutesPart; // Added in F1 24
        public ushort deltaToRaceLeaderInMS;
        public byte deltaToRaceLeaderMinutesPart; // Added in F1 24

        public float lapDistance; // Distance vehicle is around current lap in metres – could be negative if line hasn’t been crossed yet
        public float totalDistance; // Total distance travelled in session in metres – could be negative if line hasn’t been crossed yet
        public float safetyCarDelta;

        public byte carPosition;
        public byte currentLapNum;
        public PitStatus pitStatus;
        public byte numPitStops;
        public Sector sector;
        public LapValidity currentLapInvalid;
        public byte penalties; // Accumulated time penalties in seconds to be added
        public byte totalWarnings; // Accumulated number of warnings issued
        public byte cornerCuttingWarnings; // Accumulated number of corner cutting warnings issued
        public byte numUnservedDriveThroughPens;
        public byte numUnservedStopGoPens;
        public byte gridPosition;
        public DriverStatus driverStatus;
        public ResultStatus resultStatus;
        public ActiveState pitLaneTimerActive;
        public ushort pitLaneTimeInLaneInMS; // If active, the current time spent in the pit lane in ms
        public ushort pitStopTimerInMS; // Time of the actual pit stop in ms
        public byte pitStopShouldServePen; // Whether the car should serve a penalty at this stop
        public float speedTrapFastestSpeed; // Added in F1 24
        public byte speedTrapFastestLap; // Added in F1 24; Lap number the fastest speed was achieved, 255 = not set
    }
}
