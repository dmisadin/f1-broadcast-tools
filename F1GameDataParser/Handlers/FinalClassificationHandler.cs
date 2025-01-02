using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.FinalClassification;
using F1GameDataParser.Packets.FinalClassification;

namespace F1GameDataParser.Handlers
{
    public class FinalClassificationHandler : GenericHandler<FinalClassificationPacket, FinalClassification>
    {
        private readonly TelemetryClient _telemetryClient;

        public FinalClassificationHandler(TelemetryClient telemetryClient) 
        {
            _telemetryClient = telemetryClient;

            _telemetryClient.OnFinalClassificationReceive += OnRecieved;
        }

        protected override IModelFactory<FinalClassificationPacket, FinalClassification> ModelFactory => new FinalClassificationModelFactory();

        protected override void OnRecieved(FinalClassificationPacket packet)
        {
            var finalClassificationModel = ModelFactory.ToModel(packet);
        }
    }
}
