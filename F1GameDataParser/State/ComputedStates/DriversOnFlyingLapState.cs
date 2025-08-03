using F1GameDataParser.Models.ComputedModels;

namespace F1GameDataParser.State.ComputedStates
{
    public class DriversOnFlyingLapState : DictionaryStateBase<DriverOnFlyingLap>
    {
        private readonly Dictionary<int, long> _deletionSchedule = new();
        public HashSet<int> CooldownActive { get; private set; } = new();

        protected override int? GetModelKey(DriverOnFlyingLap model)
        {
            return model.VehicleIdx;
        }

        public void RemoveEntryAfterDelay(IEnumerable<int> keys, int delayMilliseconds = 4000, bool ignoreFiltering = false)
        {
            long scheduledAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            lock (_lock)
            {
                foreach (var key in keys)
                {
                    if (State.TryGetValue(key, out var driver))
                    {
                        driver.MarkedForDeletion = true;
                        driver.IgnoreFiltering = ignoreFiltering;
                        this.CooldownActive.Add(key);

                        // Record when deletion was scheduled
                        _deletionSchedule[key] = scheduledAt;
                    }
                }
            }

            _ = Task.Run(async () =>
            {
                await Task.Delay(delayMilliseconds);

                long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                lock (_lock)
                {
                    foreach (var key in keys)
                    {
                        // Only remove if not updated since scheduled
                        if (_deletionSchedule.TryGetValue(key, out var scheduledTime)
                            && scheduledTime == scheduledAt)
                        {
                            State.Remove(key);
                            _deletionSchedule.Remove(key);
                        }
                    }
                }
            });

            _ = Task.Run(async () =>
            {
                await Task.Delay(30000);
                lock (_lock)
                {
                    foreach (var key in keys)
                        CooldownActive.Remove(key);
                }
            });
        }
    }
}
