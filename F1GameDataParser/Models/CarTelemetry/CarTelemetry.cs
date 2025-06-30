using F1GameDataParser.Enums;

namespace F1GameDataParser.Models.CarTelemetry
{
    public class CarTelemetry : MergeableBase<CarTelemetry>
    {
        public Header Header { get; set; }
        public CarTelemetryDetails[] CarTelemetryDetails { get; set; } = new CarTelemetryDetails[Sizes.MaxPlayers]; // Array of car telemetry details
        public MFDPanel MFDPanelIndex { get; set; }
        public MFDPanel MFDPanelIndexSecondaryPlayer { get; set; }
        public sbyte SuggestedGear { get; set; } // 0 if no gear suggested, else [1, 8]
    }
}