using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Utility;

namespace F1GameDataParser.GameProfiles.F125
{

    public static class Teams
    {
        public static readonly Dictionary<Team, TeamDetails> AllTeams = new Dictionary<Team, TeamDetails>
        {
            { Team.RedBullRacing, new TeamDetails(GameYear.F125, Team.RedBullRacing, "Red Bull Racing",  "#2D2564", "#F7F600") },
            { Team.Ferrari,       new TeamDetails(GameYear.F125, Team.Ferrari,       "Ferrari",          "#DC0000", "#FAF100") },
            { Team.Mercedes,      new TeamDetails(GameYear.F125, Team.Mercedes,      "Mercedes",         "#00D2BE", "#000000", "#000000") },
            { Team.Alpine,        new TeamDetails(GameYear.F125, Team.Alpine,        "Alpine",           "#0090FF", "#FFFFFF") },
            { Team.McLaren,       new TeamDetails(GameYear.F125, Team.McLaren,       "McLaren",          "#FF8700", "#2D2D2D", "#2D2D2D") },
            { Team.AlfaRomeo,     new TeamDetails(GameYear.F125, Team.AlfaRomeo,     "Kick Sauber",      "#00E900", "#FFFFFF") },
            { Team.AstonMartin,   new TeamDetails(GameYear.F125, Team.AstonMartin,   "Aston Martin",     "#006F62", "#CEDC00") },
            { Team.Haas,          new TeamDetails(GameYear.F125, Team.Haas,          "Haas",             "#D2D3D5", "#C50D17") },
            { Team.AlphaTauri,    new TeamDetails(GameYear.F125, Team.AlphaTauri,    "Racing Bulls",     "#150BA9", "#FFFFFF") },
            { Team.Williams,      new TeamDetails(GameYear.F125, Team.Williams,      "Williams",         "#1256DA", "#FFFFFF") }
        };
    }
}
