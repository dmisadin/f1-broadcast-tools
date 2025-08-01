using F1GameDataParser.Models.ComputedModels;

namespace F1GameDataParser.State.ComputedStates
{
    public class DriversOnFlyingLapState : DictionaryStateBase<DriverOnFlyingLap>
    {
        public HashSet<int> CooldownActive { get; private set; } = new();

        protected override int? GetModelKey(DriverOnFlyingLap model)
        {
            return model.VehicleIdx;
        }

        public void RemoveEntryAfterDelay(IEnumerable<int> keys, int delayMilliseconds = 4000, bool ignoreFiltering = false)
        {
            lock (_lock)
            {
                foreach (var key in keys)
                {
                    if (State.TryGetValue(key, out var driver))
                    {
                        driver.MarkedForDeletion = true;
                        driver.IgnoreFiltering = ignoreFiltering;
                        this.CooldownActive.Add(key);
                    }
                }
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

            _ = Task.Run(async () =>
            {
                await Task.Delay(30000);

                lock (_lock)
                {
                    foreach (var key in keys)
                    {
                        this.CooldownActive.Remove(key);
                    }
                }
            });
        }
    }
}
