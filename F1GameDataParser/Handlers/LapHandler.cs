using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.Lap;
using F1GameDataParser.Packets.Lap;
using F1GameDataParser.State;

namespace F1GameDataParser.Handlers
{
    public class LapHandler : GenericHandler<LapPacket, IEnumerable<LapDetails>>
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly LapState _state;

        public LapHandler(TelemetryClient telemetryClient,
                          LapState lapState)
        {
            _telemetryClient = telemetryClient;
            _state = lapState;

            _telemetryClient.OnLapReceive += OnRecieved;
        }

        protected override IModelFactory<LapPacket, IEnumerable<LapDetails>> ModelFactory => new LapModelFactory();

        protected override void OnRecieved(LapPacket packet)
        {
            var lapModel = ModelFactory.ToModel(packet);
            _state.Update(lapModel);
        }
    }
}
