using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Packets;
using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F125.Packets.CarTelemetry
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarTelemetryPacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public CarTelemetryData[] carTelemetryDetails;

        public MFDPanel mfdPanelIndex;
        public MFDPanel mfdPanelIndexSecondaryPlayer;
        public sbyte suggestedGear; // 0 if no gear suggested, else [1, 8]
    }
}
