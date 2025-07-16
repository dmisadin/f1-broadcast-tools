using System.Runtime.InteropServices;

namespace F1GameDataParser.GameProfiles.F123.Packets.Motion
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarMotionData
    {
        public float worldPositionX;
        public float worldPositionY;
        public float worldPositionZ;
        public float worldVelocityX;
        public float worldVelocityY;
        public float worldVelocityZ;
        public short worldForwardDirX;
        public short worldForwardDirY;
        public short worldForwardDirZ;
        public short worldRightDirX;
        public short worldRightDirY;
        public short worldRightDirZ;
        public float gForceLateral;
        public float gForceLongitudinal;
        public float gForceVertical;
        public float yaw;
        public float pitch;
        public float roll;
    }
}
