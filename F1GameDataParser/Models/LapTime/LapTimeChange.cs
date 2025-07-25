namespace F1GameDataParser.Models.LapTime
{
    public class LapTimeChange : EventArgs
    {
        public byte VehicleIdx { get; set; }
        public bool Sector1Changed { get; set; }
        public bool Sector2Changed { get; set; }
        public bool Sector3Changed { get; set; }
        public bool LapTimeChanged { get; set; }
    }
}
