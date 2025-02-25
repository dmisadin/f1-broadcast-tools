using F1GameDataParser.State;
using F1GameDataParser.ViewModels.DriverOverride;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class DriverOverrideFactory : ViewModelFactoryBase<DriverOverride>
    {
        private readonly ParticipantsState participantsState;
        private readonly LobbyInfoState lobbyInfoState;
        private readonly LapState lapState;
        public DriverOverrideFactory(ParticipantsState participantsState,
                                    LobbyInfoState lobbyInfoState,
                                    LapState lapState)
        {
            this.participantsState = participantsState;
            this.lobbyInfoState = lobbyInfoState;
            this.lapState = lapState;
        }
        public override List<DriverOverride>? GenerateList()
        {
            if (this.participantsState.State == null 
                || this.lapState.State == null)
                return null;

            // TO DO: Join participants and laps
            var participants = this.participantsState.State.ParticipantList;
            var laps = this.lapState.State.LapDetails;
            var merged = participants.Zip(laps, (participants, laps) => new DriverOverride
            {
                RacingNumber = participants.RaceNumber,
                Name = participants.Name,
                Position = laps.CarPosition,
                Team = participants.TeamId,
            });



            return participants.Select((p, index) => new DriverOverride
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
