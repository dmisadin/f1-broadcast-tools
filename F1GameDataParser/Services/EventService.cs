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

        public void HandlePenalty(Penalty penalty)
        {
            if (penalty.PenaltyType != PenaltyType.TimePenalty && penalty.Time <= 3)
                return;

            
            if (this.lapState.State.TryGetValue(penalty.VehicleIdx, out var driverLapData))
                return;

            driverLapData?.UnservedPenalties.Enqueue(penalty.Time);
        }
    }
}
