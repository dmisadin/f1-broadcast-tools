using F1GameDataParser.Models.PersonalBestLap;
using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.State.ComputedStates;

namespace F1GameDataParser.Services
{
    public class PersonalBestLapService
    {
        private readonly PersonalBestLapState sessionFastestLapsState;

        public PersonalBestLapService(PersonalBestLapState sessionFastestLapsState)
        {
            this.sessionFastestLapsState = sessionFastestLapsState;
        }

        public void UpdateSessionFastestLaps(SessionHistory sessionHistory)
        {
            // NOTE: Laps with Exceeding Track Limits and penalties for Multiple Warnings are valid laps in Race Sesion
            var fastestLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.BestLapTimeLapNum - 1);

            if (fastestLap == null) return;

            var personalBestLap = new PersonalBestLap
            {
                VehicleIdx = sessionHistory.CarIdx,
                LapTimeInMS = fastestLap.LapTimeInMS,
                Sector1TimeInMS = fastestLap.Sector1TimeInMS,
                Sector2TimeInMS = fastestLap.Sector2TimeInMS,
                Sector3TimeInMS = fastestLap.Sector3TimeInMS
            };

            sessionFastestLapsState.Update(personalBestLap);
        }
    }
}
