using F1GameDataParser.Models;
using F1GameDataParser.Models.SessionHistory;

namespace F1GameDataParser.State
{
    public class SessionHistoryState
    {
        private readonly object _lock = new();
        public SessionHistory[] State { get; private set; }

        public SessionHistoryState()
        {
            State = new SessionHistory[Sizes.MaxPlayers];
        }

        public virtual void Update(SessionHistory newState)
        {
            lock (_lock)
            {
                if (newState.CarIdx < Sizes.MaxPlayers)
                    State[newState.CarIdx] = newState;
            }
        }
    }
}
