using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.SessionHistory
{
    public class LapHistoryDetails
    {
        public uint LapTimeInMS { get; set; }
        public ushort Sector1TimeInMS { get; set; } // Sector 1 time in milliseconds
        public byte Sector1TimeMinutes { get; set; } // Sector 1 whole minute part
        public ushort Sector2TimeInMS { get; set; }
        public byte Sector2TimeMinutes { get; set; }
        public ushort Sector3TimeInMS { get; set; }
        public byte Sector3TimeMinutes { get; set; }

        // 0x01 bit set-lap valid, 0x02 bit set-sector 1 valid
        // 0x04 bit set-sector 2 valid, 0x08 bit set-sector 3 valid
        // example: enum.HasFlag(LapSectorsValidity.Sector1Valid)
        public LapSectorsValidity LapValidBitFlags { get; set; }
    }

}
