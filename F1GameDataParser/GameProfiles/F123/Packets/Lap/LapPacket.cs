using F1GameDataParser.GameProfiles.F123.Packets;
using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.Lap
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LapPacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public LapData[] lapDetails;

        public byte timeTrialPBCarIdx; // Index of Personal Best car in time trial (255 if invalid)
        public byte timeTrialRivalCarIdx; // Index of Rival car in time trial (255 if invalid)
    }
}
