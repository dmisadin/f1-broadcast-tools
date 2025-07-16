using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.LobbyInfo
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LobbyInfoData
    {
        public Toggle aiControlled; // Whether the vehicle is AI (1) or Human (0) controlled
        public Team teamId;
        public Nationality nationality;
        public Platform platform;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public char[] name;

        public byte carNumber;
        public ReadyStatus readyStatus;
    }
}
