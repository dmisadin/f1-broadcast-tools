using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.Services;

namespace F1GameDataParser.State
{
    public class SessionHistoryState : ListStateBase<SessionHistory>
    {
        private readonly LapTimeService sessionFastestLapService;

        public SessionHistoryState(LapTimeService sessionFastestLapService)
        {
            this.sessionFastestLapService = sessionFastestLapService;
        }

        protected override int? GetModelKey(SessionHistory sessionHistory) => sessionHistory.CarIdx;

        protected override void OnModelAdded(int key, SessionHistory newModel)
        {
            OnModelMerged(key, null, newModel);
        }

        protected override void OnModelMerged(int key, SessionHistory? existingModel, SessionHistory newModel)
        {
            this.sessionFastestLapService.UpdateSessionFastestLaps(newModel);
            this.sessionFastestLapService.UpdateLatestLapTimes(newModel);
        }   
    }
}
