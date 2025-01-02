using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum ResultStatus : byte
    {
        [Label("Invalid")]
        Invalid = 0,
        [Label("Inactive")]
        Inactive,
        [Label("Active")]
        Active,
        [Label("Finished")]
        Finished,
        [Label("Did not finish", "DNF")]
        DNF,
        [Label("Disqualified", "DSQ")]
        DSQ,
        [Label("Not classified")]
        NC,
        [Label("Retired", "DNF")]
        Retired
    }
}
