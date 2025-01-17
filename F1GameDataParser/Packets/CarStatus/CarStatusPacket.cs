﻿using F1GameDataParser.Models;
using System.Runtime.InteropServices;

namespace F1GameDataParser.Packets.CarStatus
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarStatusPacket
    {
        public PacketHeader header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Sizes.MaxPlayers)]
        public CarStatusData[] carStatusDetails;
    }
}
