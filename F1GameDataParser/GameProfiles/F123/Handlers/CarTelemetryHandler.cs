using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.CarTelemetry;
using F1GameDataParser.Models.CarTelemetry;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class CarTelemetryHandler : GenericHandler<CarTelemetryPacket, CarTelemetry>
    {
        private readonly CarTelemetryState _carTelemetryState;

        public CarTelemetryHandler(CarTelemetryState carTelemetryState) 
        {
            _carTelemetryState = carTelemetryState;
        }

        protected override IModelFactory<CarTelemetryPacket, CarTelemetry> ModelFactory => new CarTelemetryModelFactory();

        public override void OnReceived(CarTelemetryPacket packet)
        {
            var carTelemetryModel = ModelFactory.ToModel(packet);
            _carTelemetryState.Update(carTelemetryModel);
        }
    }
}
