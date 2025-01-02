using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.FinalClassification
{
    public class FinalClassificationDetails
    {
        public byte Position { get; set; }
        public byte NumLaps { get; set; }
        public byte GridPosition { get; set; }
        public byte Points { get; set; }
        public byte NumPitStops { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public uint BestLapTimeInMS { get; set; }
        public double TotalRaceTime { get; set; } // Total race time in seconds without penalties
        public byte PenaltiesTime { get; set; } // Total penalties accumulated in seconds
        public byte NumPenalties { get; set; }
        public byte NumTyreStints { get; set; }

        public IEnumerable<TyreCompoundActual> TyreStintsActual { get; set; }
        public IEnumerable<TyreCompoundVisual> TyreStintsVisual { get; set; }
        public IEnumerable<byte> TyreStintsEndLaps { get; set; }
    }
}
