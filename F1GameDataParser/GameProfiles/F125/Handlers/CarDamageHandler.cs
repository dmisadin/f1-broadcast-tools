using F1GameDataParser.GameProfiles.F125.ModelFactories;
using F1GameDataParser.GameProfiles.F125.Packets.CarDamage;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.CarDamage;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F125.Handlers
{
    public class CarDamageHandler : GenericHandler<CarDamagePacket, CarDamage>
    {
        private readonly CarDamageState _carDamageState;

        public CarDamageHandler(CarDamageState carDamageState)
        {
            _carDamageState = carDamageState;
        }

        protected override IModelFactory<CarDamagePacket, CarDamage> ModelFactory => new CarDamageModelFactory();

        public override void OnReceived(CarDamagePacket packet)
        {
            var carDamage = ModelFactory.ToModel(packet);
            _carDamageState.Update(carDamage);
        }
    }
}
