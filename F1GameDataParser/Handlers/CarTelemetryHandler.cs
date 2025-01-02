using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.CarTelemetry;
using F1GameDataParser.Packets.CarTelemetry;
using F1GameDataParser.State;

namespace F1GameDataParser.Handlers
{
    public class CarTelemetryHandler : GenericHandler<CarTelemetryPacket, CarTelemetry>
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly CarTelemetryState _carTelemetryState;

        public CarTelemetryHandler(TelemetryClient telemetryClient,
                                   CarTelemetryState carTelemetryState) 
        {
            _telemetryClient = telemetryClient;
            _carTelemetryState = carTelemetryState;

            _telemetryClient.OnCarTelemetryReceive += OnRecieved;
        }

        protected override IModelFactory<CarTelemetryPacket, CarTelemetry> ModelFactory => new CarTelemetryModelFactory();

        protected override void OnRecieved(CarTelemetryPacket packet)
        {
            var carTelemetryModel = ModelFactory.ToModel(packet);
            _carTelemetryState.Update(carTelemetryModel);
        }
    }
}
