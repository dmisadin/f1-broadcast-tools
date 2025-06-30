using F1GameDataParser.Enums;

namespace F1GameDataParser.Models
{
    public class Header
    {
        public ushort PacketFormat { get; set; }
        public GameYear GameYear { get; set; }
        public byte GameMajorVersion { get; set; }
        public byte GameMinorVersion { get; set; }
        public byte PacketVersion { get; set; }
        public PacketType PacketId { get; set; }
        public ulong SessionUID { get; set; }
        public float SessionTime { get; set; }
        public uint FrameIdentifier { get; set; }
        public uint OverallFrameIdentifier { get; set; }
        public byte PlayerCarIndex { get; set; }
        public byte SecondaryPlayerCarIndex { get; set; }
    }
}
