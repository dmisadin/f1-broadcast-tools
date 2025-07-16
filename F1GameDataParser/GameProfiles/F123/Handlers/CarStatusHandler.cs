using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.CarStatus;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.CarStatus;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class CarStatusHandler : GenericHandler<CarStatusPacket, CarStatus>
    {
        private readonly CarStatusState _carStatusState;

        public CarStatusHandler(CarStatusState carStatusState) 
        {
            _carStatusState = carStatusState;
        }

        protected override IModelFactory<CarStatusPacket, CarStatus> ModelFactory => new CarStatusModelFactory();

        public override void OnReceived(CarStatusPacket packet)
        {
            var carStatusModel = ModelFactory.ToModel(packet);
            _carStatusState.Update(carStatusModel);
        }
    }
}
