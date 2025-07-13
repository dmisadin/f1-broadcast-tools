using F1GameDataParser.GameProfiles.F1Common.Packets;
using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.Motion
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MotionPacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public CarMotionData[] carMotionDetails;

        // to do: extra motion data
    }
}
