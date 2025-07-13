using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.CarTelemetry;
using F1GameDataParser.Models.CarTelemetry;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class CarTelemetryHandler : GenericHandler<CarTelemetryPacket, CarTelemetry>
    {
        private readonly F123TelemetryClient _telemetryClient;
        private readonly CarTelemetryState _carTelemetryState;

        public CarTelemetryHandler(F123TelemetryClient telemetryClient,
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
