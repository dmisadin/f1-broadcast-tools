using F1GameDataParser.GameProfiles.F123.Packets;
using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.CarStatus
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarStatusPacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public CarStatusData[] carStatusDetails;
    }
}
