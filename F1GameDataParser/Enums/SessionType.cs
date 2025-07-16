namespace F1GameDataParser.Enums
{
    public enum SessionType : byte
    {
        Unknown,
        Practice1,
        Practice2,
        Practice3,
        ShortPractice,
        Q1,
        Q2,
        Q3,
        ShortQualifying,
        OneShotQualifying,

        // Added in F1 24
        SprintShootout1,
        SprintShootout2,
        SprintShootout3,
        ShortSprintShootout,
        OneShotSprintShootout,

        Race,
        Race2,
        Race3,
        TimeTrial
    }
}
