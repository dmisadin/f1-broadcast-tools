using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.Participants
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ParticipantData
    {
        public Toggle aiControlled; // Whether the vehicle is AI (1) or Human (0) controlled
        public byte driverId; // Driver id - see appendix, 255 if network human
        public byte networkId; // Network id – unique identifier for network players
        public Team teamId; // Team id - see appendix
        public Toggle myTeam; // My team flag – 1 = My Team, 0 = otherwise
        public byte raceNumber; // Race number of the car
        public Nationality nationality; // Nationality of the driver

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public char[] name; // Name of participant in UTF-8 format – null terminated. Will be truncated with … (U+2026) if too long

        public Toggle yourTelemetry; // The player's UDP setting, 0 = restricted, 1 = public
        public Toggle showOnlineNames; // The player's show online names setting, 0 = off, 1 = on
        public Platform platform; // 1 = Steam, 3 = PlayStation, 4 = Xbox, 6 = Origin, 255 = unknown
    }
}
