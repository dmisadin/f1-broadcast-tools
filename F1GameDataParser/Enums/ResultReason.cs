using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum ResultReason : byte
    {
        [Label("Invalid")]
        Invalid = 0,

        [Label("Retired")]
        Retired = 1,

        [Label("Finished")]
        Finished = 2,

        [Label("Terminal damage")]
        TerminalDamage = 3,

        [Label("Inactive")]
        Inactive = 4,

        [Label("Not enough laps completed")]
        NotEnoughLapsCompleted = 5,

        [Label("Black flagged")]
        BlackFlagged = 6,

        [Label("Red flagged")]
        RedFlagged = 7,

        [Label("Mechanical failure")]
        MechanicalFailure = 8,

        [Label("Session skipped")]
        SessionSkipped = 9,

        [Label("Session simulated")]
        SessionSimulated = 10
    }
}
