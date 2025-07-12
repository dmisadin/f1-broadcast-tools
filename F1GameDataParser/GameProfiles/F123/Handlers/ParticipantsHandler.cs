using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.Participants;
using F1GameDataParser.Models.Participants;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class ParticipantsHandler : GenericHandler<ParticipantsPacket, Participants>
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly ParticipantsState _participantsState;
        public ParticipantsHandler(TelemetryClient telemetryClient,
                                   ParticipantsState participantsState)
        {
            _telemetryClient = telemetryClient;
            _participantsState = participantsState;

            _telemetryClient.OnParticipantsReceive += OnRecieved;
        }
        protected override IModelFactory<ParticipantsPacket, Participants> ModelFactory => new ParticipantsModelFactory();

        protected override void OnRecieved(ParticipantsPacket packet)
        {
            var participants = ModelFactory.ToModel(packet);
            _participantsState.Update(participants);
        }
    }
}
