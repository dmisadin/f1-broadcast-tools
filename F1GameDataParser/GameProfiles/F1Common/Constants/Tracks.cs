using F1GameDataParser.Enums;

namespace F1GameDataParser.GameProfiles.F1Common.Constants
{
    public record TrackDetails(Track Id, string Name, int minX, int maxX, int minZ, int maxZ, int? rotation = null);
    public static class Tracks
    {
        // Should move to DB with primary key (GameYear, TrackId)
        // Or make instance per game, like Teams
        public static readonly Dictionary<Track, TrackDetails> AllTracks = new Dictionary<Track, TrackDetails>
        {
            { Track.Melbourne,      new TrackDetails(Track.Melbourne, "Albert Park Circuit", -731, 743, -866, 872) },
            { Track.PaulRicard,     new TrackDetails(Track.PaulRicard, "Circuit Paul Ricard", -1070, 1042, -653, 569) }, // X(-1070,2809, 1041,9323), Z(-652,5916, 568,8919)
            { Track.Shanghai,       new TrackDetails(Track.Shanghai, "Shanghai International Circuit", -614, 616, -541, 546, -123) },
            { Track.SakhirBahrain,  new TrackDetails(Track.SakhirBahrain, "Bahrain International Circuit\r\n", -412, 422, -613, 613, -90) }, // X(-410.82977, 402.56824), Z(-598.0331, 590.3659)
            { Track.Catalunya,      new TrackDetails(Track.Catalunya, "Circuit de Barcelona-Catalunya", -530, 450, -611, 565, -23) },
            { Track.Monaco,         new TrackDetails(Track.Monaco, "Circuit de Monaco", -382, 397, -456, 509) }, // X(-381.82074, 397.54895), Z(-456.55325, 509.31097)
            { Track.Montreal,       new TrackDetails(Track.Montreal, "Circuit Gilles Villeneuve", -382, 437, -449, 1476) }, // X(-381.82074, 437.20325), Z(-448.4564, 1476.2909)
            { Track.Silverstone,    new TrackDetails(Track.Silverstone, "Silverstone Circuit", -617, 413, -752, 990, 97) }, // X(-617.15576, 413.04468), Z(-752.2607, 989.97186)
            { Track.Hungaroring,    new TrackDetails(Track.Hungaroring, "Hungaroring", -613, 470, -620, 598) }, // X(-613.2486, 470.357), Z(-620.1932, 597.7817)
            { Track.Spa,            new TrackDetails(Track.Spa, "Circuit de Spa-Francorchamps", -730, 580, -1073, 1000, -63) }, // X(-729.8278, 544.0512), Z(-1073.2139, 980.416)
            { Track.Monza,          new TrackDetails(Track.Monza, "Autodromo Nazionale di Monza", -660, 660, -1120, 1120, -35) }, // X(-634.3492, 632.15155), Z(-1089.3832, 1092.8539)
            { Track.Singapore,      new TrackDetails(Track.Singapore, "Marina Bay Circuit", -720, 735, -436, 487) }, // X(-719.64233, 735.19495), Z(-436.81287, 487.10208)
            { Track.Suzuka,         new TrackDetails(Track.Suzuka, "Suzuka International Racing Course", -1004, 999, -501, 501) }, // X(-1003.7501, 999.09875), Z(-500.73993, 500.61777)
            { Track.AbuDhabi,       new TrackDetails(Track.AbuDhabi, "Yas Marina Circuit", -726, 805, -322, 651) },
            { Track.Texas,          new TrackDetails(Track.Texas, "Circuit of the Americas", -815, 986, -39, 1027, 5) }, // X(-837.20496, 998.7751), Z(-99.69961, 1044.6469)
            { Track.Brazil,         new TrackDetails(Track.Brazil, "Autódromo José Carlos Pace", -575, 104, -346, 706, -75) }, // X(-574.3106, 104.285194), Z(-346.3198, 705.8594)
            { Track.Austria,        new TrackDetails(Track.Austria, "Red Bull Ring", -539, 738, -487, 308) }, // X(-538.9823, 738.35345), Z(-486.83215, 308.53595)
            { Track.Mexico,         new TrackDetails(Track.Mexico, "Autódromo Hermanos Rodríguez", -1025, 516, -1029, 73) }, // X(-1024.8362, 515.6248), Z(-1029.2969, 72.45685)
            { Track.Baku,           new TrackDetails(Track.Baku, "Baku Street Circuit", -1190, 893, -896, 598) }, // X(-1190.0176, 893.4282), Z(-895.45294, 598.36743)
            { Track.Zandvoort,      new TrackDetails(Track.Zandvoort, "Circuit Zandvoort", -473, 505, -417, 426) }, // X(-473.43445, 504.63614), Z(-416.661, 426.07544)
            { Track.Imola,          new TrackDetails(Track.Imola, "Imola Circuit", -928, 925, -468, 495, -70) }, // X(-928.2163, 925.0127), Z(-467.9021, 495.47055)
            { Track.Portimao,       new TrackDetails(Track.Portimao, "Algarve International Circuit", -382, 443, -587, 581) }, // X(-381.82074, 443.02036), Z(-587.08014, 581.29236)
            { Track.Jeddah,         new TrackDetails(Track.Jeddah, "Jeddah Corniche Circuit", -382, 300, -1376, 1377) },
            { Track.Miami,          new TrackDetails(Track.Miami, "Miami International Autodrome", -757, 755, -306, 295) }, // X(-757.0272, 755.5082), Z(-305.623, 294.78555)
            { Track.LasVegas,       new TrackDetails(Track.LasVegas, "Las Vegas Strip Circuit", -593, 591, -1000, 958) }, // X(-592.81903, 591.2689), Z(-1000.3192, 957.9117)
            { Track.Losail,         new TrackDetails(Track.Losail, "Lusail International Circuit", -625, 625, -741, 741, -59) } // X(-633.17255, 633.9855), Z(-740.87573, 741.34656)
        };
    }
}
