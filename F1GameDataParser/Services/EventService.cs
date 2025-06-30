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

        public void HandlePenalty(Penalty penalty)
        { 
            // penalty.PenaltyType != PenaltyType.TimePenalty
            if (penalty.Time != 5)
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
