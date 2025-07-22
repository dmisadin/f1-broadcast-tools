using F1GameDataParser.Models;
using F1GameDataParser.Models.SessionHistory;

namespace F1GameDataParser.State
{
    public class SessionHistoryState : ListStateBase<SessionHistory>
    {
        public void Update(SessionHistory newState)
        {
            lock (_lock)
            {
                if (State.TryGetValue(newState.CarIdx, out var existingModel))
                {
                    existingModel.MergeFrom(newState);
                    OnModelMerged(newState.CarIdx, existingModel, newState);
                }
                else
                {
                    State[newState.CarIdx] = newState;
                    OnModelAdded(newState.CarIdx, newState);
                }
            }
        }
    }
}
