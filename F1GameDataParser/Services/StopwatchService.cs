using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Utility;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels;
using F1GameDataParser.ViewModels.Enums;
using F1GameDataParser.ViewModels.Stopwatch;

namespace F1GameDataParser.Services
{
    public class StopwatchService
    {
        private readonly LapState lapState;
        private readonly ParticipantsState participantsState;
        private readonly SessionState sessionState;
        private readonly SessionHistoryState sessionHistoryState;
        private readonly PersonalBestLapState personalBestLapState;
        private readonly DriverOverrideState driverOverrideState;

        public StopwatchService(LapState lapState,
                                ParticipantsState participantsState,
                                SessionState sessionState,
                                SessionHistoryState sessionHistoryState,
                                PersonalBestLapState personalBestLapState,
                                DriverOverrideState driverOverrideState)
        {
            this.lapState = lapState;
            this.participantsState = participantsState;
            this.sessionState = sessionState;
            this.sessionHistoryState = sessionHistoryState;
            this.personalBestLapState = personalBestLapState;
            this.driverOverrideState = driverOverrideState;
        }

        public void UpdateQualifyingSessionState() // change name
        {
            var driversOnHotlap = FindDriversOnHotlap();

            if (driversOnHotlap.Count() == 0)
                return;
            var driverIdxOnHotlap = driversOnHotlap.Select(d => d.Driver.VehicleIdx).ToList();
            var driversPersonalBestLap = personalBestLapState.GetModels(driverIdxOnHotlap);
        }

        public IEnumerable<StopwatchCar> FindDriversOnHotlap()
        {
            if (lapState?.State == null || participantsState?.State == null || sessionState?.State == null)
                return Enumerable.Empty<StopwatchCar>();

            var gameYear = participantsState.State.Header.GameYear;
            var participants = participantsState.State.ParticipantList;

            return lapState.State
                .Where(driver => driver.Value.DriverStatus == DriverStatus.FlyingLap ||
                                 driver.Value.DriverStatus == DriverStatus.OnTrack)
                .OrderByDescending(driver => driver.Value.LapDistance)
                .Select(driver =>
                {
                    int vehicleIdx = driver.Key;
                    var lap = driver.Value;
                    var participant = participants.ElementAtOrDefault(vehicleIdx);
                    var overrideDriver = driverOverrideState.GetModel(vehicleIdx);

                    return new StopwatchCar
                    {
                        Driver = new DriverBasicDetails
                        {
                            VehicleIdx = vehicleIdx,
                            TeamId = participant?.TeamId ?? 0,
                            TeamDetails = GameSpecifics.GetTeamDetails(gameYear, participant?.TeamId ?? 0),
                            Name = overrideDriver?.Player?.Name ?? participant?.Name ?? "Unknown"
                        },
                        Position = lap.CarPosition,
                        CurrentTime = TimeUtility.MillisecondsToGap(lap.CurrentLapTimeInMS),
                        IsLapValid = lap.CurrentLapInvalid.ToBool(),
                        LapProgress = Convert.ToByte(lap.LapDistance / sessionState.State.TrackLength)
                    };
                })
                .ToList();
        }
    }
}
