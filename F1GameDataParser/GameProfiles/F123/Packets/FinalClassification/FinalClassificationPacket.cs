using F1GameDataParser.GameProfiles.F1Common.Packets;
using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.FinalClassification
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FinalClassificationPacket
    {
        public PacketHeader header;
        public byte numCars;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public FinalClassificationData[] classificationDetails;
    }
}
