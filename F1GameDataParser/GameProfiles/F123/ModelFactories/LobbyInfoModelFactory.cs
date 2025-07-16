using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123.Packets.LobbyInfo;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.LobbyInfo;
using System.Linq.Expressions;
using System.Text;

namespace F1GameDataParser.GameProfiles.F123.ModelFactories
{
    public class LobbyInfoModelFactory : ModelFactoryBase<LobbyInfoPacket, LobbyInfo>
    {
        public override Expression<Func<LobbyInfoPacket, LobbyInfo>> ToModelExpression()
        {
            return packet => new LobbyInfo
            {
                NumPlayers = packet.numPlayers,
                Details = packet.lobbyInfoData.Select(player => new LobbyInfoDetails
                {
                    AiControlled = player.aiControlled == Toggle.Enabled,
                    TeamId = player.teamId,
                    Nationality = player.nationality,
                    Platform = player.platform,
                    Name = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(player.name)).TrimEnd((char)0),
                    CarNumber = player.carNumber,
                    ReadyStatus = player.readyStatus
                }).ToArray()
            };
        }
    }
}
