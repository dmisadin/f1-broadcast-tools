using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.SessionHistory
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TyreStintHistoryData
    {
        public byte endLap; // 255 for current tyre
        public TyreCompoundActual tyreActualCompound;
        public TyreCompoundVisual tyreVisualCompound;
    }
}
