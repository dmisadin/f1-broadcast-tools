using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Utility;

namespace F1GameDataParser.GameProfiles.F125
{

    public static class Teams
    {
        public static readonly Dictionary<Team, TeamDetails> AllTeams = new Dictionary<Team, TeamDetails>
        {
            { Team.RedBullRacing, new TeamDetails(Team.RedBullRacing, "Red Bull Racing",  "#2D2564", "#F7F600") },
            { Team.Ferrari,       new TeamDetails(Team.Ferrari,       "Ferrari",          "#DC0000", "#FAF100") },
            { Team.Mercedes,      new TeamDetails(Team.Mercedes,      "Mercedes",         "#00D2BE", "#000000", "#000000") },
            { Team.Alpine,        new TeamDetails(Team.Alpine,        "Alpine",           "#0090FF", "#FFFFFF") },
            { Team.McLaren,       new TeamDetails(Team.McLaren,       "McLaren",          "#FF8700", "#2D2D2D", "#2D2D2D") },
            { Team.AlfaRomeo,     new TeamDetails(Team.AlfaRomeo,     "Kick Sauber",      "#00E900", "#FFFFFF") },
            { Team.AstonMartin,   new TeamDetails(Team.AstonMartin,   "Aston Martin",     "#006F62", "#CEDC00") },
            { Team.Haas,          new TeamDetails(Team.Haas,          "Haas",             "#D2D3D5", "#C50D17") },
            { Team.AlphaTauri,    new TeamDetails(Team.AlphaTauri,    "Racing Bulls",     "#150BA9", "#FFFFFF") },
            { Team.Williams,      new TeamDetails(Team.Williams,      "Williams",         "#1256DA", "#FFFFFF") }
        };
    }
}
