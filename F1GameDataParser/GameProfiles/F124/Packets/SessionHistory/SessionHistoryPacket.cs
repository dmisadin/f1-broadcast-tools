using F1GameDataParser.GameProfiles.F1Common.Packets;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.SessionHistory
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SessionHistoryPacket
    {
        public PacketHeader header;

        public byte carIdx;
        public byte numLaps;
        public byte numTyreStings;

        public byte bestLapTimeLapNum;
        public byte bestSector1LapNum;
        public byte bestSector2LapNum;
        public byte bestSector3LapNum;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public LapHistoryData[] lapHistoryData;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public TyreStintHistoryData[] tyreStintHistoryDetails;
    }
}