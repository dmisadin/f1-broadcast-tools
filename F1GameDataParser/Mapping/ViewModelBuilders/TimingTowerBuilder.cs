using F1GameDataParser.Enums;
using F1GameDataParser.Models;
using F1GameDataParser.State;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels.TimingTower;

namespace F1GameDataParser.Mapping.ViewModelBuilders
{
    public class TimingTowerBuilder : ViewModelBuilderBase<TimingTower>
    {
        private readonly LapState lapState;
        private readonly SessionState sessionState;
        private readonly ParticipantsState participantsState;
        private readonly CarStatusState carStatusState;
        private readonly SessionHistoryState sessionHistoryState;

        public TimingTowerBuilder(LapState lapState,
            SessionState sessionState,
            ParticipantsState participantsState,
            CarStatusState carStatusState,
            SessionHistoryState sessionHistoryState)
        {
            this.lapState = lapState;
            this.sessionState = sessionState;
            this.participantsState = participantsState;
            this.carStatusState = carStatusState;
            this.sessionHistoryState = sessionHistoryState;
        }

        public override TimingTower? Generate()
        {
            if (lapState.State == null 
                || sessionState.State == null 
                || participantsState.State == null
                || carStatusState.State == null 
                || sessionHistoryState.State == null)
                return null;

            var firstPlaceDriver = lapState.State.LapDetails.Where(detail => detail.CarPosition == 1).FirstOrDefault();
            var currentLap = firstPlaceDriver?.CurrentLapNum ?? 0;
            var totalLaps = sessionState.State.TotalLaps;

            var driverTimingDetails = new DriverTimingDetails[22];
            var fastestLapVehicleIdx = GetFastestLapVehicleIndex();

            for (int i = 0; i < Sizes.MaxPlayers; i++)
            {
                // TO DO: Find a way to skip some
                var lapDetails = lapState.State.LapDetails[i];
                var participantDetails = participantsState.State.ParticipantList[i];
                var carStatusDetails = carStatusState.State.Details[i];

                driverTimingDetails[i] = new DriverTimingDetails
                {
                    VehicleIdx = i,
                    Position = lapDetails.CarPosition,
                    TeamId = participantDetails.TeamId, //ToString()
                    Name = participantDetails.Name,
                    TyreAge = carStatusDetails.TyresAgeLaps,
                    VisualTyreCompound = carStatusDetails.VisualTyreCompound.ToString(),
                    GapOrResultStatus = GetGapOrResultStatus(lapDetails.DeltaToCarInFrontInMS, lapDetails.CarPosition, lapDetails.ResultStatus),
                    Penalties = lapDetails.Penalties,
                    Warnings = (byte)(lapDetails.CornerCuttingWarnings % 3),
                    HasFastestLap = i == fastestLapVehicleIdx,
                };
            }

            return new TimingTower
            {
                CurrentLap = currentLap,
                TotalLaps = totalLaps,
                DriverTimingDetails = driverTimingDetails.Where(x => x.Position > 0).OrderBy(x => x.Position).ToArray(),
            };
        }

        private uint GetFastestLapVehicleIndex()
        {
            if (sessionHistoryState.State == null)
                return 255;

            var fastestDriverSessionHistory = sessionHistoryState.State
                                                .Where(driver => driver != null
                                                    && driver.LapHistoryDetails != null
                                                    && driver.BestLapTimeLapNum < driver.LapHistoryDetails.Count()
                                                    && driver.LapHistoryDetails.ElementAt(driver.BestLapTimeLapNum).LapTimeInMS > 0)
                                                .OrderBy(driver => driver.LapHistoryDetails.ElementAt(driver.BestLapTimeLapNum).LapTimeInMS)
                                                .FirstOrDefault(); // Kinda unsafe to use ElementAt...

            if (fastestDriverSessionHistory == null)
                return 255;

            return fastestDriverSessionHistory.CarIdx;
        }

        private string GetGapOrResultStatus(long gap, int position, ResultStatus resultStatus = ResultStatus.Active)
        {
            if (resultStatus == ResultStatus.Active)
            {
                if (position == 1)
                    return "Interval"; // TO DO: Implement Leader Gap

                return  $"+{TimeUtility.MillisecondsToGap(gap)}";
            }

            return resultStatus.GetShortLabel();
        }
    }
}
