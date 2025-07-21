using F1GameDataParser.GameProfiles.F125.ModelFactories;
using F1GameDataParser.GameProfiles.F125.Packets.Motion;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Motion;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F125.Handlers
{
    public class MotionHandler : GenericHandler<MotionPacket, IEnumerable<CarMotionDetails>>
    {
        private readonly MotionState _state;

        public MotionHandler(MotionState motionState)
        {
            _state = motionState;
        }

        protected override IModelFactory<MotionPacket, IEnumerable<CarMotionDetails>> ModelFactory => new MotionModelFactory();

        public override void OnReceived(MotionPacket packet)
        {
            var lapModel = ModelFactory.ToModel(packet);
            _state.Update(lapModel);
        }
    }
}
