using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.CarStatus;
using F1GameDataParser.Packets.CarStatus;
using F1GameDataParser.State;

namespace F1GameDataParser.Handlers
{
    public class CarStatusHandler : GenericHandler<CarStatusPacket, CarStatus>
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly CarStatusState _carStatusState;

        public CarStatusHandler(TelemetryClient telemetryClient,
                                CarStatusState carStatusState) 
        {
            _telemetryClient = telemetryClient;
            _carStatusState = carStatusState;

            _telemetryClient.OnCarStatusReceive += OnRecieved;
        }

        protected override IModelFactory<CarStatusPacket, CarStatus> ModelFactory => new CarStatusModelFactory();

        protected override void OnRecieved(CarStatusPacket packet)
        {
            var carStatusModel = ModelFactory.ToModel(packet);
            _carStatusState.Update(carStatusModel);
        }
    }
}
