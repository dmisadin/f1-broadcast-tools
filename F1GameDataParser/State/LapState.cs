using F1GameDataParser.Enums;
using F1GameDataParser.Models.Lap;

namespace F1GameDataParser.State
{
    public class LapState : ListStateBase<LapDetails>
    {
        protected override void OnModelMerged(int key, LapDetails existingModel, LapDetails newModel)
        {
            newModel.UnservedPenalties = existingModel.UnservedPenalties;
            newModel.IsServingPenalty = existingModel.IsServingPenalty;
        }
    }
}
