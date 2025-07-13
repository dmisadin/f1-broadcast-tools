using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.FinalClassification;
using F1GameDataParser.Models.FinalClassification;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class FinalClassificationHandler : GenericHandler<FinalClassificationPacket, FinalClassification>
    {
        private readonly F123TelemetryClient _telemetryClient;

        public FinalClassificationHandler(F123TelemetryClient telemetryClient) 
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
