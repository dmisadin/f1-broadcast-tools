using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.Session;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Session;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class SessionHandler : GenericHandler<SessionPacket, Session>
    {
        private readonly SessionState _sessionState;
        public SessionHandler(SessionState sessionState)
        {
            _sessionState = sessionState;
        }
        protected override IModelFactory<SessionPacket, Session> ModelFactory => new SessionModelFactory();

        public override void OnReceived(SessionPacket packet)
        {
            var sessionModel = ModelFactory.ToModel(packet);
            _sessionState.Update(sessionModel);
        }
    }
}
