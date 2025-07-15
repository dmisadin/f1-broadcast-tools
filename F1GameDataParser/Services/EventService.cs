using F1GameDataParser.Enums;
using F1GameDataParser.Models.Event;
using F1GameDataParser.State;

namespace F1GameDataParser.Services
{
    public class EventService
    {
        private readonly LapState lapState;
        public EventService(LapState lapState)
        {
            this.lapState = lapState;
        }

        public void HandlePenalty(Penalty penalty, GameYear gameYear)
        {
            // Note: In F1 23, you can't serve 10s time penalty, only 5s. Tested in 50% race.
            if (gameYear == GameYear.F123 && penalty.Time != 5)
                return;
            else if (penalty.Time != 5 || penalty.Time != 10)
                return;

            lock (lapState.Lock)
            {
                if (!lapState.State.TryGetValue(penalty.VehicleIdx, out var driverLapData))
                    return;

                driverLapData?.UnservedPenalties.Enqueue(penalty.Time);
            }
        }
    }
}
