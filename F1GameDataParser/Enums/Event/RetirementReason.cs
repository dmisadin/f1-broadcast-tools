namespace F1GameDataParser.Enums.Event
{
    public enum RetirementReason : byte
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
