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

        public void UpdateDriversOnFlyingLap(IEnumerable<LapDetails> newState)
        {
            var driversOnFlyingLap = new List<DriverOnFlyingLap>();
            var driversNotOnFlyingLap = new List<int>();

            byte index = 0;
            foreach (var driver in newState)
            {
                if (driver.DriverStatus == DriverStatus.FlyingLap)
                {
                    driversOnFlyingLap.Add(new DriverOnFlyingLap
                    {
                        VehicleIdx = index,
                        LapDistance = driver.LapDistance,
                        FrameIdentifier = driver.FrameIdentifier,
                        MarkedForDeletion = false
                    });
                }
                else if (this.driversOnFlyingLapState.State.TryGetValue(index, out var driverOnFlyingLap)
                          && !driverOnFlyingLap.MarkedForDeletion)
                {
                    driversNotOnFlyingLap.Add(index);
                }

                index++;
            }

            this.driversOnFlyingLapState.Update(driversOnFlyingLap);
            this.driversOnFlyingLapState.RemoveEntryAfterDelay(driversNotOnFlyingLap);
        }
    }
}
