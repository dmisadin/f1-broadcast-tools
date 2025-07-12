using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.CarDamage;
using F1GameDataParser.Models.CarDamage;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class CarDamageHandler : GenericHandler<CarDamagePacket, CarDamage>
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly CarDamageState _carDamageState;

        public CarDamageHandler(TelemetryClient telemetryClient,
                                CarDamageState carDamageState)
        {
            _telemetryClient = telemetryClient;
            _carDamageState = carDamageState;

            _telemetryClient.OnCarDamageReceive += OnRecieved;
        }

        protected override IModelFactory<CarDamagePacket, CarDamage> ModelFactory => new CarDamageModelFactory();

        protected override void OnRecieved(CarDamagePacket packet)
        {
            var carDamage = ModelFactory.ToModel(packet);
            _carDamageState.Update(carDamage);
        }
    }
}
