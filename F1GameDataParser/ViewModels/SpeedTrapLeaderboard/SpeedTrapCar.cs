namespace F1GameDataParser.ViewModels.SpeedTrapLeaderboard
{
    public class SpeedTrapCar
    {
        public int VehicleIdx { get; set; }
        public DriverBasicDetails? Driver { get; set; }
        public short Speed { get; set; }  
        public int OrdinalNumber { get; set; }
        public bool NeedDivider { get; set; }
    }
}
