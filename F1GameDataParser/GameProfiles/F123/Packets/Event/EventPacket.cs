using F1GameDataParser.GameProfiles.F123.Packets;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.Event
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventPacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] eventStringCode;

        public EventData eventDetails;
    }
}
