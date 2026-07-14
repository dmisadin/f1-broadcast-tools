using F1GameDataParser.State;
using F1GameDataParser.ViewModels.SessionClassification;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class SessionClassificationFactory : ViewModelFactoryBase<SessionClassification>
    {
        readonly FinalClassificationState finalClassificationState;

        public SessionClassificationFactory(FinalClassificationState finalClassificationState)
        {
            this.finalClassificationState = finalClassificationState;
        }

        public override SessionClassification? Generate()
        {
            return base.Generate();
        }
    }
}
