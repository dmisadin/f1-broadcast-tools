using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F125.Packets.SessionHistory
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LapHistoryData
    {
        public uint lapTimeInMS;
        public ushort sector1TimeInMS; // Sector 1 time in milliseconds
        public byte sector1TimeMinutes; // Sector 1 whole minute part
        public ushort sector2TimeInMS;
        public byte sector2TimeMinutes;
        public ushort sector3TimeInMS;
        public byte sector3TimeMinutes;

        // 0x01 bit set-lap valid, 0x02 bit set-sector 1 valid
        // 0x04 bit set-sector 2 valid, 0x08 bit set-sector 3 valid
        public LapSectorsValidity lapValidBitFlags;
    }
}
