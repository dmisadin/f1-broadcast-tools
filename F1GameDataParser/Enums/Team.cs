using F1GameDataParser.Attributes;

namespace F1GameDataParser.Enums
{
    public enum Team : byte
    {
        [Label("Mercedes", "MER")]
        Mercedes,
        [Label("Ferarri", "FER")]
        Ferrari,
        [Label("Red Bull Racing", "RBR")]
        RedBullRacing,
        [Label("Williams", "WIL")]
        Williams,
        [Label("Aston Martin", "AM")]
        AstonMartin,
        [Label("Alpine", "ALP")]
        Alpine,
        [Label("AlphaTauri", "AT")]
        AlphaTauri,
        [Label("Haas", "HAAS")]
        Haas,
        [Label("McLaren", "MCL")]
        McLaren,
        [Label("Alfa Romeo", "AR")]
        AlfaRomeo,

        Mercedes2020 = 85,
        Ferrari2020,
        RedBull2020,
        Williams2020,
        RacingPoint2020,
        Renault2020,
        AlphaTauri2020,
        Haas2020,
        McLaren2020,
        AlfaRomeo2020,

        AstonMartinDB11V12 = 95,
        AstonMartinVantageF1Edition,
        AstonMartinVantageSafetyCar,
        FerrariF8Tributo,
        FerrariRoma,
        McLaren720S,
        McLarenArtura,
        MercedesAMGGTBlackSeriesSafetyCar,
        MercedesAMGGTRPro,
        F1CustomTeam,

        Prema21 = 106,
        UniVirtuosi21,
        Varlin21,
        Hitech21,
        ArtGP21,
        MPMotorsports21,
        Charouz21,
        Dams21,
        Campos21,
        BWT21,
        Trident21,

        MercedesAMGGTBlackSeries,

        Prema22,
        Virtuosi22,
        Carlin22,
        Hitech22,
        ArtGP22,
        MPMotorsports22,
        Charouz22,
        Dams22,
        Campos22,
        VanAmersfoortRacing22,
        Trident22
    }
}
