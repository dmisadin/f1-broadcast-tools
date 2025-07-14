using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.FinalClassification;
using F1GameDataParser.Models.FinalClassification;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    public class FinalClassificationHandler : GenericHandler<FinalClassificationPacket, FinalClassification>
    {

        public FinalClassificationHandler() 
        {
        }

        protected override IModelFactory<FinalClassificationPacket, FinalClassification> ModelFactory => new FinalClassificationModelFactory();

        public override void OnReceived(FinalClassificationPacket packet)
        {
            var finalClassificationModel = ModelFactory.ToModel(packet);
        }
    }
}
