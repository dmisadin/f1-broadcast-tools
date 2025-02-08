using F1GameDataParser.Models.Participants;
using F1GameDataParser.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1GameDataParser.Services
{
    public class PlayerService
    {
        //public readonly 
        private readonly ParticipantsState participantsState;
        public PlayerService(ParticipantsState participantsState) 
        {
            this.participantsState = participantsState;
        }

        public Participants? GetParticipants()
        {
            return this.participantsState.State;
        }
    }
}
