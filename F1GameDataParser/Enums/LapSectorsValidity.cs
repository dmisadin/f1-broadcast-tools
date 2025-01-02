namespace F1GameDataParser.Enums
{
    // 0x01 bit set-lap valid, 0x02 bit set-sector 1 valid
    // 0x04 bit set-sector 2 valid, 0x08 bit set-sector 3 valid
    [Flags]
    public enum LapSectorsValidity : byte
    {
        None = 0,
        LapValid = 0x01,
        Sector1Valid = 0x02,
        Sector2Valid = 0x04,
        Sector3Valid = 0x08,

        Sector1And2Valid = Sector1Valid | Sector2Valid,
        Sector1And3Valid = Sector1Valid | Sector3Valid,
        Sector1And2And3Valid = Sector1Valid | Sector2Valid | Sector3Valid,
        LapAndSector1And2And3Valid = LapValid | Sector1Valid | Sector2Valid | Sector3Valid,
    }
}
