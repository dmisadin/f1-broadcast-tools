using F1GameDataParser.State;
using F1GameDataParser.Dtos.PlayerOverride;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public class PlayerOverrideFactory : DtoFactoryBase<PlayerOverride>
    {
        private readonly ParticipantsState participantsState;
        private readonly LobbyInfoState lobbyInfoState;
        private readonly LapState lapState;
        public PlayerOverrideFactory(ParticipantsState participantsState,
                                    LobbyInfoState lobbyInfoState,
                                    LapState lapState)
        {
            this.participantsState = participantsState;
            this.lobbyInfoState = lobbyInfoState;
            this.lapState = lapState;
        }
        public override List<PlayerOverride>? GenerateList()
        {
            if (this.participantsState.State == null 
                || this.lapState.State == null)
                return null;

            // TO DO: Join participants and laps
            var participants = this.participantsState.State.ParticipantList;
            var laps = this.lapState.State.LapDetails;
            var merged = participants.Zip(laps, (participants, laps) => new PlayerOverride
            {
                RacingNumber = participants.RaceNumber,
                Name = participants.Name,
                Position = laps.CarPosition,
                Team = participants.TeamId,
            });



            return participants.Select((p, index) => new PlayerOverride
            {
                Id = index,
                RacingNumber = p.RaceNumber,
                Name = p.Name,
                NameOverride = "",
                //Position and Team missing
            }).ToList();
        }
    }
}
