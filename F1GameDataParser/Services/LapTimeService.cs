using F1GameDataParser.Models.LapTime;
using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.State.ComputedStates;

namespace F1GameDataParser.Services
{
    public class LapTimeService
    {
        private readonly PersonalBestLapState personalBestLapState;
        private readonly LatestLapTimeState latestLapTimeState;

        public LapTimeService(PersonalBestLapState personalBestLapState, LatestLapTimeState latestLapTimeState)
        {
            this.personalBestLapState = personalBestLapState;
            this.latestLapTimeState = latestLapTimeState;
        }

        public void UpdatePersonalBestLap(SessionHistory sessionHistory)
        {
            // NOTE: Laps with Exceeding Track Limits and penalties for Multiple Warnings are valid laps in Race Sesion
            var fastestLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.BestLapTimeLapNum - 1);

            if (fastestLap == null) return;

            var personalBestLap = new LapTime
            {
                VehicleIdx = sessionHistory.CarIdx,
                LapTimeInMS = fastestLap.LapTimeInMS,
                Sector1TimeInMS = fastestLap.Sector1TimeInMS,
                Sector2TimeInMS = fastestLap.Sector2TimeInMS,
                Sector3TimeInMS = fastestLap.Sector3TimeInMS
            };

            personalBestLapState.Update(personalBestLap);
        }

        public void UpdateLatestLapTimes(SessionHistory sessionHistory)
        {
            var currentLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.NumLaps - 1);
            var previousLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.NumLaps - 2);
            
            if (currentLap?.LapTimeInMS > 0)
            {
                previousLap = currentLap;
                currentLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.NumLaps); // Take(numLaps+1) in factory
            }

            if (currentLap == null) return;

            var currentLatestLapState = latestLapTimeState.GetModel(sessionHistory.CarIdx);

            ushort? s1TimeInMS = currentLap.Sector1TimeInMS > 0 ? currentLap.Sector1TimeInMS : previousLap?.Sector1TimeInMS;
            ushort? s2TimeInMS = currentLap.Sector2TimeInMS > 0 ? currentLap.Sector2TimeInMS : previousLap?.Sector2TimeInMS;
            ushort? s3TimeInMS = currentLap.Sector3TimeInMS > 0 ? currentLap.Sector3TimeInMS : previousLap?.Sector3TimeInMS;
            uint?   lapTimeInMS = currentLap.LapTimeInMS    > 0 ? currentLap.LapTimeInMS     : previousLap?.LapTimeInMS;

            var previousLapModel = new LapTime
            {
                VehicleIdx = sessionHistory.CarIdx,
                LapTimeInMS = lapTimeInMS ?? 0,
                Sector1TimeInMS = s1TimeInMS ?? 0,
                Sector2TimeInMS = s2TimeInMS ?? 0,
                Sector3TimeInMS = s3TimeInMS ?? 0,
                Sector1Changed = (currentLatestLapState?.Sector1Changed ?? false) || (currentLatestLapState?.Sector1TimeInMS ?? 0) != (s1TimeInMS ?? 0),
                Sector2Changed = (currentLatestLapState?.Sector2Changed ?? false) || (currentLatestLapState?.Sector2TimeInMS ?? 0) != (s2TimeInMS ?? 0),
                Sector3Changed = (currentLatestLapState?.Sector3Changed ?? false) || (currentLatestLapState?.Sector3TimeInMS ?? 0) != (s3TimeInMS ?? 0),
                LapTimeChanged = (currentLatestLapState?.LapTimeChanged ?? false) || (currentLatestLapState?.LapTimeInMS ?? 0) != (lapTimeInMS ?? 0)
            };

            latestLapTimeState.Update(previousLapModel);
        }
    }
}
