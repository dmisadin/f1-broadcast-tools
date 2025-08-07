namespace F1GameDataParser.ViewModels.TelemetryDashboard
{
    public class HaloTelemetryDashboard : TelemetryDashboard
    {
        public byte Turn { get; set; } = 0;
        public DriverBasicDetails? Driver { get; set; }
        public DriverBasicDetails? NextDriver { get; set; }
    }
}
