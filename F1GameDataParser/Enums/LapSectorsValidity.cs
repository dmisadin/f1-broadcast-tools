namespace F1GameDataParser.Enums
{
    // 0x01 bit set-lap valid, 0x02 bit set-sector 1 valid
    // 0x04 bit set-sector 2 valid, 0x08 bit set-sector 3 valid
    [Flags]
    public enum LapSectorsValidity : byte
    {
        LapValid = 0x01,
        Sector1Valid = 0x02,
        Sector2Valid = 0x04,
        Sector3Valid = 0x08
    }
}
