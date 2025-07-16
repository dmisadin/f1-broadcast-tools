namespace F1GameDataParser.Enums
{
    public enum ResultReason : byte
    {
        Invalid = 0,
        Retired,
        Finished,
        TerminalDamage,
        Inactive,
        NotEnoughLapsCompleted,
        BlackFlagged,
        RedFlagged,
        MechanicalFailure,
        SessionSkipped,
        SessionSimulated
    }
}
