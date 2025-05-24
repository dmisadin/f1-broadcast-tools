using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Models.DriverOverride;
using F1GameDataParser.State;
using Microsoft.EntityFrameworkCore;

namespace F1GameDataParser.Services
{
    public class PlayerService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly ParticipantsState participantsState;
        private readonly DriverOverrideState driverOverrideState;
        public PlayerService(IRepository<Player> playerRepository,
                            ParticipantsState participantsState,
                            DriverOverrideState driverOverrideState) 
        {
            this.playerRepository = playerRepository;
            this.participantsState = participantsState;
            this.driverOverrideState = driverOverrideState;
        }

        public List<Player> GetPlayers()
        {
            return this.playerRepository.GetAllAsync().Result;
        }

        public async Task<List<LookupDto>> SearchPlayers(string name, int limit)
        {
            var playersQuery = playerRepository.Query();

            var lookupQuery = playersQuery.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                                        .Select(p => new LookupDto
                                        {
                                            Id = p.Id,
                                            Label = p.Name
                                        });

            return await lookupQuery.Take(limit).ToListAsync();
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

            /*
            var participantsState = this.participantsState.State;
            if (participantsState == null || participantsState.ParticipantList.Length == 0)
                return;

            foreach (var driver in drivers)
            {
                if (driver.Id < 0 || driver.Id >= participantsState.ParticipantList.Length)
                    continue;

                var participant = participantsState.ParticipantList[driver.Id];

                if (driver.PlayerId.HasValue && players.TryGetValue(driver.PlayerId.Value, out var player))
                    participant.Player = player;
            }
            */
        }
    }
}
