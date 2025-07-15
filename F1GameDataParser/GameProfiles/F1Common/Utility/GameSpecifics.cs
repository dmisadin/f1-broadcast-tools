using F1GameDataParser.Enums;

namespace F1GameDataParser.GameProfiles.F1Common.Utility
{
    public record TeamDetails(Team Id, string Name, string PrimaryColor, string SecondaryColor, string TextColor = "#FFFFFF");
    public class GameSpecifics
    {
        public static TeamDetails? GetTeamDetails(GameYear year, Team teamId)
        {
            return year switch
            {
                GameYear.F123 => F123.Teams.AllTeams.GetValueOrDefault(teamId),
                GameYear.F125 => F125.Teams.AllTeams.GetValueOrDefault(teamId),
                _ => null
            };
        }
    }
}
