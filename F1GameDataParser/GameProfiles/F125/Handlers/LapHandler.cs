using F1GameDataParser.GameProfiles.F125.ModelFactories;
using F1GameDataParser.GameProfiles.F125.Packets.Lap;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Lap;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F125.Handlers
{
    public class LapHandler : GenericHandler<LapPacket, IEnumerable<LapDetails>>
    {
        private readonly LapState _state;

        public LapHandler(LapState lapState)
        {
            _state = lapState;
        }

        protected override IModelFactory<LapPacket, IEnumerable<LapDetails>> ModelFactory => new LapModelFactory();

        public override void OnReceived(LapPacket packet)
        {
            var lapModel = ModelFactory.ToModel(packet);
            _state.Update(lapModel);
        }
    }
}
