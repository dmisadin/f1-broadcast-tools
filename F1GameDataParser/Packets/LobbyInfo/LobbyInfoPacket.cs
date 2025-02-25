using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.Packets.LobbyInfo
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LobbyInfoPacket
    {
        public PacketHeader header;
        public byte numPlayers;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public LobbyInfoData[] lobbyInfoData;
    }
}
