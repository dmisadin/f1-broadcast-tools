using F1GameDataParser.Models.ComputedModels;

namespace F1GameDataParser.State.ComputedStates
{
    public class LatestLapTimeState : DictionaryStateBase<LapTime>
    {
        private readonly HashSet<int> pendingReset = new();
        private readonly object resetLock = new();
        protected override int? GetModelKey(LapTime model) => model.VehicleIdx;

        protected override void OnModelMerged(int key, LapTime existingModel, LapTime newModel)
        {
            if (newModel.LapTimeChanged.HasValue && newModel.LapTimeChanged.Value)
            {
                this.ResetSectorTimeChanges([key]);
            }
        }

        public void ResetSectorTimeChanges(IEnumerable<int> keys)
        {
            lock (this.resetLock)
            {
                foreach (var key in keys)
                {
                    if (this.pendingReset.Contains(key))
                        continue;

                    this.pendingReset.Add(key);

                    this.ScheduleReset(key);
                }
            }
        }

        private void ScheduleReset(int key)
        {
            Task.Run(async () =>
            {
                await Task.Delay(5000);

                lock (_lock)
                {
                    if (State.TryGetValue(key, out var latestLap))
                    {
                        latestLap.Sector1Changed = null;
                        latestLap.Sector2Changed = null;
                        latestLap.Sector3Changed = null;
                        latestLap.LapTimeChanged = null;
                    }
                }

                lock (resetLock)
                {
                    this.pendingReset.Remove(key);
                }
            });
        }
    }
}
