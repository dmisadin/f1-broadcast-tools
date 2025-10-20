using F1GameDataParser.Database.Entities.Widgets;
using F1GameDataParser.Enums;
using F1GameDataParser.Models.Event;
using F1GameDataParser.State;
using F1GameDataParser.ViewModels.SessionEvent;

namespace F1GameDataParser.Services
{
    public class SessionEventService
    {
        private readonly LapState lapState;
        private readonly WebSocketBroadcastService webSocketBroadcastService;

        public SessionEventService(LapState lapState, WebSocketBroadcastService webSocketBroadcastService)
        {
            this.lapState = lapState;
            this.webSocketBroadcastService = webSocketBroadcastService;
        }

        public void HandlePenalty(Penalty penalty, GameYear gameYear)
        {
            // Note: In F1 23, you can't serve 10s time penalty, only 5s. Tested in 50% race.
            if (penalty.Time != 5)
                return;

            lock (lapState.Lock)
            {
                if (!lapState.State.TryGetValue(penalty.VehicleIdx, out var driverLapData))
                    return;

                driverLapData?.UnservedPenalties.Enqueue(penalty.Time);
            }
        }


        public async void HandleFastestLap(FastestLap fastestLap) 
        {
            var eventModel = new SessionEvent
            {

            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

    }
}
