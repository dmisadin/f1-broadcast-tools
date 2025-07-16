using F1GameDataParser.GameProfiles.F125.ModelFactories;
using F1GameDataParser.GameProfiles.F125.Packets.LobbyInfo;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.LobbyInfo;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F125.Handlers
{
    public class LobbyInfoHandler : GenericHandler<LobbyInfoPacket, LobbyInfo>
    {
        private readonly LobbyInfoState _state;
        public LobbyInfoHandler(LobbyInfoState state)
        {
            _state = state;

        }
        protected override IModelFactory<LobbyInfoPacket, LobbyInfo> ModelFactory => new LobbyInfoModelFactory();

        public override void OnReceived(LobbyInfoPacket packet)
        {
            var lobbyInfoModel = ModelFactory.ToModel(packet);
            _state.Update(lobbyInfoModel);
        }
    }
}
