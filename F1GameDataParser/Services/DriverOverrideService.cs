using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.GameProfiles.F1Common.Utility;
using F1GameDataParser.Models.DriverOverride;
using F1GameDataParser.State;
using F1GameDataParser.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace F1GameDataParser.Services
{
    public class DriverOverrideService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly ParticipantsState participantsState;
        private readonly LapState lapState;
        private readonly DriverOverrideState driverOverrideState;
        private readonly DriverDetailsBroadcastService driverDetailsBroadcastService;

        public DriverOverrideService(ParticipantsState participantsState,
                                    LapState lapState,
                                    DriverOverrideState driverOverrideState,
                                    DriverDetailsBroadcastService driverDetailsBroadcastService,
                                    IRepository<Player> playerRepository)
        {
            this.participantsState = participantsState;
            this.lapState = lapState;
            this.driverOverrideState = driverOverrideState;
            this.driverDetailsBroadcastService = driverDetailsBroadcastService;
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

        public DriverBasicDetails? GetDriverBasicDetails(int vehicleIdx)
        {
            if (participantsState?.State == null) return null;

            var participant = participantsState.State.ParticipantList.ElementAtOrDefault(vehicleIdx);

            if (participant == null) return null;

            var overrideDriver = driverOverrideState.GetModel(vehicleIdx);

            return new DriverBasicDetails
            {
                VehicleIdx = vehicleIdx,
                TeamId = participant.TeamId,
                TeamDetails = GameSpecifics.GetTeamDetails(participantsState.State.Header.GameYear, participant.TeamId),
                Name = overrideDriver?.Player.Name ?? participant.Name,
            };
        }
        public async Task UpdateOverrides(List<DriverOverrideDto> drivers)
        {

            var playerIds = drivers.Where(d => d.PlayerId.HasValue)
                                    .Select(d => d.PlayerId)
                                    .ToList();

            var players = await playerRepository.Query()
                                                .Where(p => playerIds.Contains(p.Id))
                                                .ToDictionaryAsync(p => p.Id);

            var driverOverrides = players.Select((p, index) => new DriverOverride
            {
                Id = drivers.FirstOrDefault(d => d.PlayerId == p.Key)?.Id ?? 0,
                PlayerId = p.Value.Id,
                Player = p.Value
            });

            this.driverOverrideState.Update(driverOverrides);

            await this.driverDetailsBroadcastService.UpdateDrivers();
        }
    }
}
