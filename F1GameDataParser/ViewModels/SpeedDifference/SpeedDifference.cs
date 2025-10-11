namespace F1GameDataParser.ViewModels.SpeedDifference
{
    public class SpeedDifference
    {
        public int SpectatedVehicleIdx { get; set; }
        public ushort SpectatedSpeed { get; set; }
        public byte SpectatedPosition { get; set; }
        public int FollowingVehicleIdx { get; set; }
        public int FollowingSpeedDifference { get; set; }
        public byte FollowingPosition { get; set; }
    }
}
