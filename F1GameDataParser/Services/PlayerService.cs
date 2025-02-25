﻿using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Models.Participants;
using F1GameDataParser.State;
using Microsoft.EntityFrameworkCore;

namespace F1GameDataParser.Services
{
    public class PlayerService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly ParticipantsState participantsState;
        public PlayerService(IRepository<Player> playerRepository,
                            ParticipantsState participantsState) 
        {
            this.playerRepository = playerRepository;
            this.participantsState = participantsState;
        }

        public Participants? GetParticipants()
        {
            return this.participantsState.State;
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
    }
}
