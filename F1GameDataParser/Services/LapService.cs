using F1GameDataParser.Enums;
using F1GameDataParser.Models.Lap;
using F1GameDataParser.Models.ComputedModels;
using F1GameDataParser.State.ComputedStates;

namespace F1GameDataParser.Services
{
    public class LapService
    {
        private readonly DriversOnFlyingLapState driversOnFlyingLapState;
        private readonly PersonalBestLapState personalBestLapState;

        public LapService(DriversOnFlyingLapState driversOnFlyingLapState, PersonalBestLapState personalBestLapState)
        {
            this.driversOnFlyingLapState = driversOnFlyingLapState;
            this.personalBestLapState = personalBestLapState;
        }

        public void UpdateDriversOnFlyingLap(IDictionary<int, LapDetails> oldState, IEnumerable<LapDetails> newState)
        {
            var driversOnFlyingLap = new List<DriverOnFlyingLap>();
            var driversNotOnFlyingLap = new List<int>();
            var driversNotOnFlyingLapAndIgnoredSorting = new List<int>(); // Do not move these up with drivers that finished the lap (MarkedForDeletion)

            byte index = 0;
            foreach (var driver in newState)
            {
                if (index >= 20) continue;

                driversOnFlyingLapState.State.TryGetValue(index, out var driverOnFlyingLap);

                if ((driverOnFlyingLap != null && driverOnFlyingLap.MarkedForDeletion) 
                    || driversOnFlyingLapState.CooldownActive.Contains(index))
                {
                    index++;
                    continue;
                }
                else if (driver.CurrentLapInvalid == LapValidity.Invalid
                        || driver.ResultStatus != ResultStatus.Active)
                {
                    driversNotOnFlyingLapAndIgnoredSorting.Add(index);
                    index++;
                    continue;
                }

                var fastestLap = personalBestLapState.GetFastestLap();

                if (fastestLap != null
                    && (fastestLap.Sector1TimeInMS * 1.07 < driver.Sector1TimeInMS
                        || (fastestLap.Sector1TimeInMS + fastestLap.Sector2TimeInMS) * 1.07 < driver.Sector1TimeInMS + driver.Sector2TimeInMS))
                { // 107% rule, in case someone is on slow lap or cooldown lap
                    driversNotOnFlyingLapAndIgnoredSorting.Add(index);
                    index++;
                    continue;
                }

                var newDriver = new DriverOnFlyingLap
                {
                    VehicleIdx = index,
                    LapDistance = driver.LapDistance,
                    FrameIdentifier = driver.FrameIdentifier,
                    MarkedForDeletion = driverOnFlyingLap != null && driverOnFlyingLap.MarkedForDeletion,
                };

                if (driver.DriverStatus == DriverStatus.FlyingLap || driver.DriverStatus == DriverStatus.OnTrack)
                { // Relevant mostly to online players
                    oldState.TryGetValue(index, out var oldDriver);

                    if ((oldDriver != null && oldDriver.CurrentLapInvalid == LapValidity.Invalid && driver.CurrentLapInvalid == LapValidity.Valid))
                    { // If car is staring new lap, but previous was invalid, skip the check old.CurrentLapNum != new.CurrentLapNum
                        driversOnFlyingLap.Add(newDriver);
                    }
                    else if (oldDriver != null && oldDriver.CurrentLapNum != driver.CurrentLapNum)
                    { // If car has crossed the finish line
                        driversNotOnFlyingLap.Add(index);
                    }
                    else if (oldDriver != null && oldDriver.CurrentLapNum == driver.CurrentLapNum)
                    { // If car hasn't crossed the finish line yet
                        driversOnFlyingLap.Add(newDriver);
                    }
                    else if (driverOnFlyingLap == null || oldDriver == null)
                    {
                        driversOnFlyingLap.Add(newDriver);
                    }
                }
                else if (driverOnFlyingLap != null && !driverOnFlyingLap.MarkedForDeletion)
                    driversNotOnFlyingLap.Add(index);
                else if (driver.DriverStatus == DriverStatus.InLap || driver.PitStatus == PitStatus.Pitting)
                    driversNotOnFlyingLapAndIgnoredSorting.Add(index);

                index++;
            }

            this.driversOnFlyingLapState.Update(driversOnFlyingLap);
            this.driversOnFlyingLapState.RemoveEntryAfterDelay(driversNotOnFlyingLap);
            this.driversOnFlyingLapState.RemoveEntryAfterDelay(driversNotOnFlyingLapAndIgnoredSorting, ignoreFiltering: true);
        }
    }
}
