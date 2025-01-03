﻿using F1GameDataParser.Models.Participants;
using F1GameDataParser.Packets.Participants;
using System.Linq.Expressions;
using System.Text;

namespace F1GameDataParser.Mapping.ModelFactories
{
    public class ParticipantsModelFactory : ModelFactoryBase<ParticipantsPacket, Participants>
    {
        public override Expression<Func<ParticipantsPacket, Participants>> ToModelExpression()
        {
            return packet => new Participants
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                NumActiveCars = packet.numActiveCars,
                ParticipantList = packet.participants.Select(p => new ParticipantDetails
                {
                    AiControlled = p.aiControlled,
                    DriverId = p.driverId,
                    NetworkId = p.networkId,
                    TeamId = p.teamId,
                    MyTeam = p.myTeam,
                    RaceNumber = p.raceNumber,
                    Nationality = p.nationality,
                    Name = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(p.name)).TrimEnd((Char)0),
                    YourTelemetry = p.yourTelemetry,
                    ShowOnlineNames = p.showOnlineNames,
                    Platform = p.platform
                }).ToArray()
            };
        }
    }
}
