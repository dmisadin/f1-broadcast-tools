using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.Session
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MarshalZoneData
    {
        public float zoneStart; // value [0,1] of the way through the lap
        public ZoneFlag zoneFlag; 
    }
}
