using F1GameDataParser.Enums;
using F1GameDataParser.Models.ComputedModels;
using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.State.ComputedStates;

namespace F1GameDataParser.Services
{
    public class LapTimeService
    {
        private readonly PersonalBestLapState personalBestLapState;
        private readonly LatestLapTimeState latestLapTimeState;
        private readonly FastestSectorTimeState fastestSectorTimeState;

        public LapTimeService(PersonalBestLapState personalBestLapState,
                              LatestLapTimeState latestLapTimeState,
                              FastestSectorTimeState fastestSectorTimeState)
        {
            this.personalBestLapState = personalBestLapState;
            this.latestLapTimeState = latestLapTimeState;
            this.fastestSectorTimeState = fastestSectorTimeState;
        }

        public void UpdatePersonalBestLap(SessionHistory sessionHistory)
        {
            // NOTE: Laps with Exceeding Track Limits and penalties for Multiple Warnings are valid laps in Race Sesion
            var fastestLap = sessionHistory.LapHistoryDetails.ElementAtOrDefault(sessionHistory.BestLapTimeLapNum - 1);
            
            if (fastestLap == null) return;

            PersonalBestLap? newPreviousPB = null;

            if (personalBestLapState.State.TryGetValue(sessionHistory.CarIdx, out var previousPB)
                && previousPB.LapTimeInMS != fastestLap.LapTimeInMS)
            {
                newPreviousPB = new PersonalBestLap
                {
                    VehicleIdx = previousPB.VehicleIdx,
                    LapTimeInMS = previousPB.LapTimeInMS,
                    Sector1TimeInMS = previousPB.Sector1TimeInMS,
                    Sector2TimeInMS = previousPB.Sector2TimeInMS,
                    Sector3TimeInMS = previousPB.Sector3TimeInMS,
                    PreviousBestLap = null
                };
            }
            else
                newPreviousPB = previousPB?.PreviousBestLap;

            var personalBestLap = new PersonalBestLap
            {
                VehicleIdx = sessionHistory.CarIdx,
                LapTimeInMS = fastestLap.LapTimeInMS,
                Sector1TimeInMS = fastestLap.Sector1TimeInMS,
                Sector2TimeInMS = fastestLap.Sector2TimeInMS,
                Sector3TimeInMS = fastestLap.Sector3TimeInMS,
                PreviousBestLap = newPreviousPB,
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

        public void UpdateFastestSectors(SessionHistory driverSessionHistory)
        {
            var newFastestSectors = new List<FastestSectorTime>();

            foreach (var lap in driverSessionHistory.LapHistoryDetails)
            {
                if (lap.Sector1TimeInMS > 0 
                    && lap.LapValidBitFlags.HasFlag(LapSectorsValidity.Sector1Valid)
                    && fastestSectorTimeState.State.TryGetValue((int)Sector.Sector1, out var currentFastestS1)
                    && lap.Sector1TimeInMS < currentFastestS1.TimeInMS)
                {
                    newFastestSectors.Add(new FastestSectorTime
                    {
                        Sector = Sector.Sector1,
                        VehicleIdx = driverSessionHistory.CarIdx,
                        TimeInMS = lap.Sector1TimeInMS,
                        PreviousTimeInMS = currentFastestS1.TimeInMS
                    });
                    Console.WriteLine($"New fastest S1: {lap.Sector1TimeInMS};\t by Car {driverSessionHistory.CarIdx}.");
                }

                if (lap.Sector2TimeInMS > 0
                    && lap.LapValidBitFlags.HasFlag(LapSectorsValidity.Sector2Valid)
                    && fastestSectorTimeState.State.TryGetValue((int)Sector.Sector2, out var currentFastestS2)
                    && lap.Sector2TimeInMS < currentFastestS2.TimeInMS)
                {
                    newFastestSectors.Add(new FastestSectorTime
                    {
                        Sector = Sector.Sector2,
                        VehicleIdx = driverSessionHistory.CarIdx,
                        TimeInMS = lap.Sector2TimeInMS,
                        PreviousTimeInMS = currentFastestS2.TimeInMS
                    });
                    Console.WriteLine($"New fastest S2: {lap.Sector2TimeInMS};\t by Car {driverSessionHistory.CarIdx}.");
                }

                if (lap.Sector3TimeInMS > 0
                    && lap.LapValidBitFlags.HasFlag(LapSectorsValidity.Sector3Valid)
                    && fastestSectorTimeState.State.TryGetValue((int)Sector.Sector3, out var currentFastestS3)
                    && lap.Sector3TimeInMS < currentFastestS3.TimeInMS)
                {
                    newFastestSectors.Add(new FastestSectorTime
                    {
                        Sector = Sector.Sector3,
                        VehicleIdx = driverSessionHistory.CarIdx,
                        TimeInMS = lap.Sector3TimeInMS,
                        PreviousTimeInMS = currentFastestS3.TimeInMS
                    });
                    Console.WriteLine($"New fastest S3: {lap.Sector3TimeInMS};\t by Car {driverSessionHistory.CarIdx}.");
                }
            }

            if (newFastestSectors.Any())
                fastestSectorTimeState.Update(newFastestSectors);
        }
    }
}
