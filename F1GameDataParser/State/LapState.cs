using F1GameDataParser.Enums;
using F1GameDataParser.Models.Lap;
using F1GameDataParser.Services;

namespace F1GameDataParser.State
{
    public class LapState : DictionaryStateBase<LapDetails>
    {
        public object Lock => _lock;

        private readonly LapService lapService;
        public LapState(LapService lapService) 
        {
            this.lapService = lapService;
        }

        protected override void BeforeEnumerableMerged(IEnumerable<LapDetails> newState)
        {
            lapService.UpdateDriversOnFlyingLap(this.State, newState);
        }

        protected override void OnModelMerged(int key, LapDetails existingModel, LapDetails newModel)
        {
            if (existingModel.GameYear == GameYear.F123)
                this.CheckAndServeTimePenalites(existingModel);
        }

        private void CheckAndServeTimePenalites(LapDetails existingModel)
        {
            if (existingModel.PitStatus != PitStatus.None)
                this.RemoveServedPenalty(existingModel);
            else
                existingModel.IsServingPenalty = false;
        }

        private void RemoveServedPenalty(LapDetails driverLapData)
        {
            if (driverLapData.UnservedPenalties.Count() == 0
                || driverLapData.NumUnservedDriveThroughPens != 0
                || driverLapData.NumUnservedStopGoPens != 0
                || driverLapData.IsServingPenalty)
                return;
            // Speeding in the pit lane could become an edge case
            try
            {
                driverLapData.UnservedPenalties.Dequeue();
                driverLapData.IsServingPenalty = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error removing penalty from queue: " + e.Message);
            }
        }
    }
}
