namespace F1GameDataParser.Models.Motion
{
    public class CarMotionDetails : MergeableBase<CarMotionDetails>
    {
        public float WorldPositionX { get; set; }
        public float WorldPositionY { get; set; }
        public float WorldPositionZ { get; set; }
        public float WorldVelocityX { get; set; }
        public float WorldVelocityY { get; set; }
        public float WorldVelocityZ { get; set; }
        public short WorldForwardDirX { get; set; }
        public short WorldForwardDirY { get; set; }
        public short WorldForwardDirZ { get; set; }
        public short WorldRightDirX { get; set; }
        public short WorldRightDirY { get; set; }
        public short WorldRightDirZ { get; set; }
        public float GForceLateral { get; set; }
        public float GForceLongitudinal { get; set; }
        public float GForceVertical { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
    }
}
