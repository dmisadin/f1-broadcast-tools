using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.State;

namespace F1GameDataParser.Services
{
    public class DriverOverrideService
    {
        private readonly ParticipantsState participantsState;
        private readonly LobbyInfoState lobbyInfoState;
        private readonly LapState lapState;
        private readonly DriverOverrideState driverOverrideState;
        private readonly IRepository<Player> playerRepository;

        public DriverOverrideService(ParticipantsState participantsState,
                                    LobbyInfoState lobbyInfoState,
                                    LapState lapState,
                                    DriverOverrideState driverOverrideState,
                                    IRepository<Player> playerRepository)
        {
            this.participantsState = participantsState;
            this.lobbyInfoState = lobbyInfoState;
            this.lapState = lapState;
            this.driverOverrideState = driverOverrideState;
            this.playerRepository = playerRepository;
        }

        public List<DriverOverrideDto> GetAll()
        {
            if (participantsState.State == null || lapState.State == null)
                return new List<DriverOverrideDto>();

            var participants = participantsState.State.ParticipantList;
            var laps = lapState.State?.Select(kvp => kvp.Value).ToList();

            // Generate initial drivers list
            var drivers = participants.Zip(laps, (participant, lap) => new DriverOverrideDto
            {
                RacingNumber = participant.RaceNumber,
                Name = participant.Name,
                Position = lap.CarPosition,
                Team = participant.TeamId
            }).Where(d => d.Position > 0).ToList(); // Materializing the list here to avoid re-evaluations

            var driverOverrides = this.driverOverrideState.State.Select(d => new DriverPlayerDto
            {
                DriverId = d.Value.Id,
                PlayerId = d.Value.PlayerId,
                PlayerName = d.Value.Player.Name
            }).ToDictionary(d => d.DriverId);

            // Construct final list with indexed Id and lookup dictionary for Player
            return drivers.Select((d, index) => new DriverOverrideDto
            {
                Id = index,
                RacingNumber = d.RacingNumber,
                Name = d.Name,
                Position = d.Position,
                Team = d.Team,
                PlayerId = driverOverrides.TryGetValue(index, out var player) ? player.PlayerId : null,
                Player = player
            }).ToList();
        }
    }
}
