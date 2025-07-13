using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.LobbyInfo;
using F1GameDataParser.Models.LobbyInfo;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class LobbyInfoHandler : GenericHandler<LobbyInfoPacket, LobbyInfo>
    {
        private readonly F123TelemetryClient _telemetryClient;
        private readonly LobbyInfoState _state;
        public LobbyInfoHandler(F123TelemetryClient telemetryClient,
                                LobbyInfoState state)
        {
            _telemetryClient = telemetryClient;
            _state = state;

            _telemetryClient.OnLobbyInfoReceive += OnRecieved;
        }
        protected override IModelFactory<LobbyInfoPacket, LobbyInfo> ModelFactory => new LobbyInfoModelFactory();

        protected override void OnRecieved(LobbyInfoPacket packet)
        {
            var lobbyInfoModel = ModelFactory.ToModel(packet);
            _state.Update(lobbyInfoModel);
        }
    }
}
