using F1GameDataParser.Enums;
using F1GameDataParser.ViewModels.Enums;

namespace F1GameDataParser.ViewModels.Stopwatch
{
    public class StopwatchCar
    {
        public DriverBasicDetails Driver { get; set; } // Participants, DriverOverrides
        public byte Position { get; set; } // Lap
        public string CurrentTime { get; set; } // SessionHistory.LapHistory
        public bool IsLapValid { get; set; } // SessionHistory.LapHistory
        public int LapProgress { get; set; } // Lap
        public float LapDistance { get; set; } // Lap
        public string TyreCompoundVisual { get; set; } // CarStatus

        public SectorTimeStatus? Sector1TimeStatus { get; set; } 
        public SectorTimeStatus? Sector2TimeStatus { get; set; }
        public SectorTimeStatus? Sector3TimeStatus { get; set; }
        public SectorTimeStatus? LapTimeStatus { get; set; }

        public string? Sector1GapToLeader { get; set; }
        public string? Sector2GapToLeader { get; set; }
        public string? Sector3GapToLeader { get; set; }
        public string? LapGapToLeader { get; set; }
    }
}
