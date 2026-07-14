using F1GameDataParser.GameProfiles.F125.ModelFactories;
using F1GameDataParser.GameProfiles.F125.Packets.FinalClassification;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.FinalClassification;
using F1GameDataParser.State;

namespace F1GameDataParser.GameProfiles.F125.Handlers
{
    public class FinalClassificationHandler : GenericHandler<FinalClassificationPacket, FinalClassification>
    {
        private readonly FinalClassificationState finalClassificationState;

        public FinalClassificationHandler(FinalClassificationState finalClassificationState) 
        {
            this.finalClassificationState = finalClassificationState;
        }

        protected override IModelFactory<FinalClassificationPacket, FinalClassification> ModelFactory => new FinalClassificationModelFactory();

        public override void OnReceived(FinalClassificationPacket packet)
        {
            var finalClassificationModel = ModelFactory.ToModel(packet);

            this.finalClassificationState.Update(finalClassificationModel.Details);
        }
    }
}
