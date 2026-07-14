namespace F1GameDataParser.ViewModels.SessionClassification;

public class SessionClassification // Dont forget to support all session types (practice, sprint qual, sprint, qual, race)
{
    public string SessionTitle { get; set; }
    public IEnumerable<SessionClassificationVehicle> Vehicles { get; set; }
}
