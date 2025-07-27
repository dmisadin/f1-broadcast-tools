using F1GameDataParser.Models.LapTime;

namespace F1GameDataParser.State.ComputedStates
{
    public class LatestLapTimeState : DictionaryStateBase<LapTime>
    {
        protected override int? GetModelKey(LapTime model) => model.VehicleIdx;

        protected override void OnModelMerged(int key, LapTime existingModel, LapTime newModel)
        {
            if (newModel.Sector3Changed.HasValue && newModel.Sector3Changed.Value)
                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    if(State.TryGetValue(key, out var latestLap))
                    {
                        latestLap.Sector1Changed = null;
                        latestLap.Sector2Changed = null;
                        latestLap.Sector3Changed = null;
                        latestLap.LapTimeChanged = null;
                    }
                });
        }
    }
}
