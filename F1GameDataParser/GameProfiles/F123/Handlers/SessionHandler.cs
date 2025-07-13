using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.Session;
using F1GameDataParser.Models.Session;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class SessionHandler : GenericHandler<SessionPacket, Session>
    {

        private readonly F123TelemetryClient _telemetryClient;
        private readonly SessionState _sessionState;
        public SessionHandler(F123TelemetryClient telemetryClient,
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
