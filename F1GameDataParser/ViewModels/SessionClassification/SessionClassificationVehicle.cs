namespace F1GameDataParser.ViewModels.SessionClassification;

public class SessionClassificationVehicle
{
    public byte Position { get; set; }
    public DriverBasicDetails Driver { get; set; }
    public sbyte PositionsGained { get; set; }
    public byte Points { get; set; }
    public string BestLapTime { get; set; }
    public string Result { get; set; }
}
