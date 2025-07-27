using F1GameDataParser.Models.LapTime;
using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.State.ComputedStates;

namespace F1GameDataParser.Services
{
    public class LapTimeService
    {
        private readonly PersonalBestLapState sessionFastestLapsState;
        private readonly LatestLapTimeState latestLapTimeState;

        public LapTimeService(PersonalBestLapState sessionFastestLapsState, LatestLapTimeState latestLapTimeState)
        {
            this.sessionFastestLapsState = sessionFastestLapsState;
            this.latestLapTimeState = latestLapTimeState;
        }

        public void UpdateSessionFastestLaps(SessionHistory sessionHistory)
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

            sessionFastestLapsState.Update(personalBestLap);
        }

        public void UpdateLatestLapTimes(SessionHistory sessionHistory)
        {
            var currentLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.NumLaps - 1);
            var previousLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.NumLaps - 2);

            if (currentLap == null) return;

            var currentState = latestLapTimeState.GetModel(sessionHistory.CarIdx);

            if (previousLap == null)
            {
                ushort? s1TimeInMS = currentLap.Sector1TimeInMS > 0 ? currentLap.Sector1TimeInMS : previousLap?.Sector1TimeInMS;
                ushort? s2TimeInMS = currentLap.Sector2TimeInMS > 0 ? currentLap.Sector2TimeInMS : previousLap?.Sector2TimeInMS;
                ushort? s3TimeInMS = currentLap.Sector3TimeInMS > 0 ? currentLap.Sector3TimeInMS : previousLap?.Sector3TimeInMS;
                uint? lapTimeInMS = currentLap.LapTimeInMS      > 0 ? currentLap.LapTimeInMS     : previousLap?.LapTimeInMS;

                var currentLapModel = new LapTime
                {
                    VehicleIdx = sessionHistory.CarIdx,
                    LapTimeInMS = currentLap.LapTimeInMS,
                    Sector1TimeInMS = currentLap.Sector1TimeInMS,
                    Sector2TimeInMS = currentLap.Sector2TimeInMS,
                    Sector3TimeInMS = currentLap.Sector3TimeInMS,
                    Sector1Changed = (currentState?.Sector1Changed ?? false) || currentState?.Sector1TimeInMS != s1TimeInMS,
                    Sector2Changed = (currentState?.Sector2Changed ?? false) || currentState?.Sector2TimeInMS != s2TimeInMS,
                    Sector3Changed = (currentState?.Sector3Changed ?? false) || currentState?.Sector3TimeInMS != s3TimeInMS,
                    LapTimeChanged = (currentState?.LapTimeChanged ?? false) || currentState?.LapTimeInMS != lapTimeInMS
                };

                latestLapTimeState.Update(currentLapModel);
                return;
            }

            ushort sector1TimeInMS = currentLap.Sector1TimeInMS > 0 ? currentLap.Sector1TimeInMS : previousLap.Sector1TimeInMS;
            ushort sector2TimeInMS = currentLap.Sector2TimeInMS > 0 ? currentLap.Sector2TimeInMS : previousLap.Sector2TimeInMS;
            ushort sector3TimeInMS = currentLap.Sector3TimeInMS > 0 ? currentLap.Sector3TimeInMS : previousLap.Sector3TimeInMS;

            var lapModel = new LapTime
            {
                VehicleIdx = sessionHistory.CarIdx,
                LapTimeInMS = currentLap.LapTimeInMS,
                Sector1TimeInMS = sector1TimeInMS,
                Sector2TimeInMS = sector2TimeInMS,
                Sector3TimeInMS = sector3TimeInMS,
                Sector1Changed = (currentState?.Sector1Changed ?? false) || currentState?.Sector1TimeInMS != sector1TimeInMS,
                Sector2Changed = (currentState?.Sector2Changed ?? false) || currentState?.Sector2TimeInMS != sector2TimeInMS,
                Sector3Changed = (currentState?.Sector3Changed ?? false) || currentState?.Sector3TimeInMS != sector3TimeInMS,
                LapTimeChanged = (currentState?.LapTimeChanged ?? false) || currentState?.LapTimeInMS != currentLap.LapTimeInMS
            };

            latestLapTimeState.Update(lapModel);
        }
    }
}
