using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F125.Packets.FinalClassification
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FinalClassificationData
    {
        public byte position;
        public byte numLaps;
        public byte gridPosition;
        public byte points;
        public byte numPitStops;
        public ResultStatus resultStatus;
        public uint bestLapTimeInMS;
        public double totalRaceTime; // Total race time in seconds without penalties
        public byte penaltiesTime; // Total penalties accumulated in seconds
        public byte numPenalties;
        public byte numTyreStints;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public TyreCompoundActual[] tyreStintsActual;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public TyreCompoundVisual[] tyreStintsVisual;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] tyreStintsEndLaps;
    }
}
