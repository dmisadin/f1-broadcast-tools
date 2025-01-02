using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.Packets.CarDamage
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarDamagePacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public CarDamageData[] carDamageDetails;
    }
}
