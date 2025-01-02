using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.Lap
{
    public class LapDetails
    {
        public uint LastLapTimeInMS { get; set; }
        public uint CurrentLapTimeInMS { get; set; }
        public ushort Sector1TimeInMS { get; set; } // Sector 1 time in milliseconds
        public byte Sector1TimeMinutes { get; set; } // Sector 1 whole minute part
        public ushort Sector2TimeInMS { get; set; } // Sector 2 time in milliseconds
        public byte Sector2TimeMinutes { get; set; } // Sector 2 whole minute part

        public ushort DeltaToCarInFrontInMS { get; set; }
        public ushort DeltaToRaceLeaderInMS { get; set; }

        public float LapDistance { get; set; } // Distance vehicle is around current lap in metres – could be negative if line hasn’t been crossed yet
        public float TotalDistance { get; set; } // Total distance travelled in session in metres – could be negative if line hasn’t been crossed yet
        public float SafetyCarDelta { get; set; }

        public byte CarPosition { get; set; }
        public byte CurrentLapNum { get; set; }
        public PitStatus PitStatus { get; set; }
        public byte NumPitStops { get; set; }
        public Sector Sector { get; set; }
        public LapValidity CurrentLapInvalid { get; set; }
        public byte Penalties { get; set; } // Accumulated time penalties in seconds to be added
        public byte TotalWarnings { get; set; } // Accumulated number of warnings issued
        public byte CornerCuttingWarnings { get; set; } // Accumulated number of corner cutting warnings issued
        public byte NumUnservedDriveThroughPens { get; set; }
        public byte NumUnservedStopGoPens { get; set; }
        public byte GridPosition { get; set; }
        public DriverStatus DriverStatus { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public ActiveState PitLaneTimerActive { get; set; }
        public ushort PitLaneTimeInLaneInMS { get; set; } // If active, the current time spent in the pit lane in ms
        public ushort PitStopTimerInMS { get; set; } // Time of the actual pit stop in ms
        public byte PitStopShouldServePen { get; set; } // Whether the car should serve a penalty at this stop
    }

}
