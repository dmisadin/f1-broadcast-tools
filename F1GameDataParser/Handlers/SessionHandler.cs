using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.Session;
using F1GameDataParser.Packets.Session;
using F1GameDataParser.State;

namespace F1GameDataParser.Handlers
{
    public class SessionHandler : GenericHandler<SessionPacket, Session>
    {

        private readonly TelemetryClient _telemetryClient;
        private readonly SessionState _sessionState;
        public SessionHandler(TelemetryClient telemetryClient,
                                   SessionState sessionState)
        {
            _telemetryClient = telemetryClient;
            _sessionState = sessionState;

            _telemetryClient.OnSessionReceive += OnRecieved;
        }
        protected override IModelFactory<SessionPacket, Session> ModelFactory => new SessionModelFactory();

        protected override void OnRecieved(SessionPacket packet)
        {
            var sessionModel = ModelFactory.ToModel(packet);
            _sessionState.Update(sessionModel);
        }
    }
}
