using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.Participants;
using F1GameDataParser.Models.Participants;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class ParticipantsHandler : GenericHandler<ParticipantsPacket, Participants>
    {
        private readonly ParticipantsState _participantsState;
        public ParticipantsHandler(ParticipantsState participantsState)
        {
            _participantsState = participantsState;
        }
        protected override IModelFactory<ParticipantsPacket, Participants> ModelFactory => new ParticipantsModelFactory();

        public override void OnReceived(ParticipantsPacket packet)
        {
            var participants = ModelFactory.ToModel(packet);
            _participantsState.Update(participants);
        }
    }
}
