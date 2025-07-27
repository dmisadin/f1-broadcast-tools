using F1GameDataParser.Models.ComputedModels;

namespace F1GameDataParser.State.ComputedStates
{
    public class DriversOnFlyingLapState : DictionaryStateBase<DriverOnFlyingLap>
    {
        protected override int? GetModelKey(DriverOnFlyingLap model)
        {
            return model.VehicleIdx;
        }

        public void RemoveEntryAfterDelay(IEnumerable<int> keys, int delayMilliseconds = 4000)
        {
            foreach (var key in keys)
            {
                if (State.TryGetValue(key, out var driver))
                    driver.MarkedForDeletion = true;
            }

            _ = Task.Run(async () =>
            {
                await Task.Delay(delayMilliseconds);

                lock (_lock)
                {
                    foreach (var key in keys)
                    {
                        State.Remove(key);
                    }
                }
            });
        }
    }
}
