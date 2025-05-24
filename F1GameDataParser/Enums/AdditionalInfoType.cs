namespace F1GameDataParser.Enums
{
    [Flags]
    public enum AdditionalInfoType
    {
        None = 0,
        Warnings = 1 << 0,
        Penalties = 1 << 1,
        NumPitStops = 1 << 2,
        PositionsGained = 1 << 3
    }
}
