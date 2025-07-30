using F1GameDataParser.Enums;
using F1GameDataParser.Models.Lap;
using F1GameDataParser.Models.ComputedModels;
using F1GameDataParser.State.ComputedStates;

namespace F1GameDataParser.Services
{
    public class LapService
    {
        private readonly DriversOnFlyingLapState driversOnFlyingLapState;

        public LapService(DriversOnFlyingLapState driversOnFlyingLapState)
        {
            this.driversOnFlyingLapState = driversOnFlyingLapState;
        }

        public void UpdateDriversOnFlyingLap(IDictionary<int, LapDetails> oldState, IEnumerable<LapDetails> newState)
        {
            var driversOnFlyingLap = new List<DriverOnFlyingLap>();
            var driversNotOnFlyingLap = new List<int>();

            byte index = 0;
            foreach (var driver in newState)
            { // TODO: add: 1) 107% rule on S1; 2) Invalid lap; 3) ResultStatus == Active
                driversOnFlyingLapState.State.TryGetValue(index, out var driverOnFlyingLap);
                if (driverOnFlyingLap != null && driverOnFlyingLap.MarkedForDeletion)
                {
                    index++;
                    continue;
                }

                if (driver.DriverStatus == DriverStatus.FlyingLap)
                { // Relevant mostly to AI cars
                    driversOnFlyingLap.Add(new DriverOnFlyingLap
                    {
                        VehicleIdx = index,
                        LapDistance = driver.LapDistance,
                        FrameIdentifier = driver.FrameIdentifier,
                        MarkedForDeletion = false
                    });
                }
                else if (driver.DriverStatus == DriverStatus.OnTrack)
                { // Relevant mostly to online players
                    oldState.TryGetValue(index, out var oldDriver);

                    if (driverOnFlyingLap == null || oldDriver == null)
                    {
                        driversOnFlyingLap.Add(new DriverOnFlyingLap
                        {
                            VehicleIdx = index,
                            LapDistance = driver.LapDistance,
                            FrameIdentifier = driver.FrameIdentifier,
                            MarkedForDeletion = false
                        });
                    }
                    else if (oldDriver != null && oldDriver.CurrentLapNum == driver.CurrentLapNum)
                    { // If car hasn't crossed the finish line yet
                        driversOnFlyingLap.Add(new DriverOnFlyingLap
                        {
                            VehicleIdx = index,
                            LapDistance = driver.LapDistance,
                            FrameIdentifier = driver.FrameIdentifier,
                            MarkedForDeletion = false
                        });
                    }
                    else if (oldDriver != null && oldDriver.CurrentLapNum != driver.CurrentLapNum)
                    { // If car has crossed the finish line
                        driversNotOnFlyingLap.Add(index);
                    }
                }
                else if (driverOnFlyingLap != null && !driverOnFlyingLap.MarkedForDeletion)
                    driversNotOnFlyingLap.Add(index);

                index++;
            }

            this.driversOnFlyingLapState.Update(driversOnFlyingLap);
            this.driversOnFlyingLapState.RemoveEntryAfterDelay(driversNotOnFlyingLap);
        }
    }
}
