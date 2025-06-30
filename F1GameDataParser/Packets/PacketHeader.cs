using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.Packets
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketHeader
    {
        public ushort packetFormat;
        public GameYear gameYear;
        public byte gameMajorVersion;
        public byte gameMinorVersion;
        public byte packetVersion;
        public PacketType packetId;
        public ulong sessionUID;
        public float sessionTime;
        public uint frameIdentifier;
        public uint overallFrameIdentifier;
        public byte playerCarIndex;
        public byte secondaryPlayerCarIndex;
    }
}
