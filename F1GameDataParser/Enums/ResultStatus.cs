using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum ResultStatus : byte
    {
        [Label("Invalid", "INV")]
        Invalid = 0,
        [Label("Inactive", "INA")]
        Inactive,
        [Label("Active", "ACT")]
        Active,
        [Label("Finished", "FIN")]
        Finished,
        [Label("Did not finish", "DNF")]
        DNF,
        [Label("Disqualified", "DSQ")]
        DSQ,
        [Label("Not classified", "NC")]
        NC,
        [Label("Retired", "DNF")]
        Retired
    }
}
