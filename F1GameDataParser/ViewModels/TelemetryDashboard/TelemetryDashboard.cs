namespace F1GameDataParser.ViewModels.TelemetryDashboard
{
    public class TelemetryDashboard
    {
        public byte VehicleIdx { get; set; }
        public ushort Speed { get; set; }
        public float Throttle { get; set; }
        public float Brake { get; set; }
        public sbyte Gear { get; set; }
        public ushort EngineRPM { get; set; }
        public bool DRS { get; set; } = false;
        public byte RevLightsPercent { get; set; }

        public byte Position { get; set; }
    }
}
