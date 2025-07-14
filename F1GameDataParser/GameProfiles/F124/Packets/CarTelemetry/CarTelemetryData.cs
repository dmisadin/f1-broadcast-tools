using F1GameDataParser.Enums;
using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F124.Packets.CarTelemetry
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarTelemetryData
    {
        public ushort speed; // km/h
        public float throttle; // [0.0, 1.0]
        public float steer; // [-1.0, 1.0]
        public byte clutch; // [0, 100]
        public sbyte gear; // [1, 8], N=0, R=-1
        public ushort engineRPM;
        public byte drs; // 0 = off, 1 = on
        public byte revLightsPercent;
        public ushort revLightsBitValue; // Rev lights (bit 0 = leftmost LED, bit 14 = rightmost LED

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] brakesTemperature; // Celsius

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] tyresSurfaceTemperature; // Celsius

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] tyresInnerTemperature; // Celsius

        public ushort engineTemperature; // Celsius

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyresPressure; // PSI

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Surface[] surfaceType;
    }
}
