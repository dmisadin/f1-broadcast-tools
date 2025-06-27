using F1GameDataParser.Enums;

namespace F1GameDataParser.GameProfiles.F123
{
    public record TeamDetails(Team Id, string Name, string PrimaryColor, string SecondaryColor, string TextColor = "#FFFFFF");

    public static class Teams
    {
        public static readonly TeamDetails[] AllTeams = new[]
        {
            new TeamDetails(Team.RedBullRacing, "Red Bull Racing",  "#0600EF", "#F7F600"),
            new TeamDetails(Team.Ferrari,       "Ferrari",          "#DC0000", "#FAF100"),
            new TeamDetails(Team.Mercedes,      "Mercedes",         "#00D2BE", "#000000", "#000000"),
            new TeamDetails(Team.Alpine,        "Alpine",           "#0090FF", "#FFFFFF"),
            new TeamDetails(Team.McLaren,       "McLaren",          "#FF8700", "#2D2D2D", "#2D2D2D"),
            new TeamDetails(Team.AlfaRomeo,     "Alfa Romeo",       "#720600", "#FFFFFF"),
            new TeamDetails(Team.AstonMartin,   "Aston Martin",     "#006F62", "#CEDC00"),
            new TeamDetails(Team.Haas,          "Haas",             "#D2D3D5", "#C50D17"),
            new TeamDetails(Team.AlphaTauri,    "AlphaTauri",       "#2B4562", "#FFFFFF"),
            new TeamDetails(Team.Williams,      "Williams",         "#1256DA", "#FFFFFF")
        };
    }
}
