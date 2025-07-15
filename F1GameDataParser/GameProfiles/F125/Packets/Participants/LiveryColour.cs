using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F125.Packets.Participants
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LiveryColour
    {
        byte red;
        byte green;
        byte blue;
    }
}
