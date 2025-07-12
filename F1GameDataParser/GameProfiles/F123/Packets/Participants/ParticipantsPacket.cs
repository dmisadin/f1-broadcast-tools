using F1GameDataParser.GameProfiles.F123.Packets;
using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.Participants
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ParticipantsPacket
    {
        public PacketHeader header; // Header
        public byte numActiveCars; // Number of active cars in the data – should match number of cars on HUD

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public ParticipantData[] participants;
    }
}
