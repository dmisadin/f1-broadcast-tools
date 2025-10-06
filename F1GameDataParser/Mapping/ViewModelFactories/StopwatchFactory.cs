using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Utility;
using F1GameDataParser.Models.ComputedModels;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels;
using F1GameDataParser.ViewModels.Enums;
using F1GameDataParser.ViewModels.Stopwatch;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class StopwatchFactory : ViewModelFactoryBase<StopwatchList>
    {
        private readonly LapState lapState;
        private readonly ParticipantsState participantsState;
        private readonly SessionState sessionState;
        private readonly PersonalBestLapState personalBestLapState;
        private readonly LatestLapTimeState latestLapTimeState;
        private readonly DriversOnFlyingLapState driversOnFlyingLapState;
        private readonly FastestSectorTimeState fastestSectorTimeState;
        private readonly DriverOverrideState driverOverrideState;
        private readonly CarStatusState carStatusState;

        public StopwatchFactory(LapState lapState,
                                ParticipantsState participantsState,
                                SessionState sessionState,
                                PersonalBestLapState personalBestLapState,
                                LatestLapTimeState latestLapTimeState,
                                DriversOnFlyingLapState driversOnFlyingLapState,
                                FastestSectorTimeState fastestSectorTimeState,
                                DriverOverrideState driverOverrideState,
                                CarStatusState carStatusState)
        {
            this.lapState = lapState;
            this.participantsState = participantsState;
            this.sessionState = sessionState;
            this.personalBestLapState = personalBestLapState;
            this.latestLapTimeState = latestLapTimeState;
            this.driversOnFlyingLapState = driversOnFlyingLapState;
            this.fastestSectorTimeState = fastestSectorTimeState;
            this.driverOverrideState = driverOverrideState;
            this.carStatusState = carStatusState;
        }

        public override StopwatchList? Generate()
        {
            if (lapState?.State == null || participantsState?.State == null || sessionState?.State == null)
                return null;

            var vehicleIdxOnHotlap = FindDriversOnFlyingLap();
            var stopwatchCars = new List<StopwatchCar>();

            foreach (var vehicleIdx in vehicleIdxOnHotlap)
            {
                var car = BuildStopwatchCar(vehicleIdx);
                if (car != null)
                {
                    stopwatchCars.Add(car);
                }
            }

            var fastestLap = personalBestLapState.GetFastestLap();
            var secondFastestLap = personalBestLapState.GetSecondFastestLap();

            return new StopwatchList
            {
                GameYear = sessionState.State.Header.GameYear,
                FastestLap = GetFastestLapDetails(fastestLap),
                SecondFastestLap = GetFastestLapDetails(secondFastestLap),
                Cars = stopwatchCars
            };
        }

        public StopwatchSpectated? GenerateSpectated()
        {
            if (sessionState?.State == null) return null;

            return GenerateSingle(sessionState.State.SpectatorCarIndex);
        }

        public StopwatchSpectated? GenerateSingle(byte vehicleIdx)
        {
            if (lapState?.State == null || participantsState?.State == null || sessionState?.State == null)
                return null;

            var fastestLap = personalBestLapState.GetFastestLap();
            var secondFastestLap = personalBestLapState.GetSecondFastestLap();

            return new StopwatchSpectated
            {
                GameYear = sessionState.State.Header.GameYear,
                FastestLap = GetFastestLapDetails(fastestLap),
                SecondFastestLap = GetFastestLapDetails(secondFastestLap),
                Car = BuildStopwatchCar(vehicleIdx)
            };
        }

        private StopwatchCar? BuildStopwatchCar(byte vehicleIdx)
        {
            var participants = participantsState.State?.ParticipantList;
            var laps = lapState.State;

            var participant = participants?.ElementAtOrDefault(vehicleIdx);
            var lap = laps?.ElementAtOrDefault(vehicleIdx).Value;
            if (lap == null || participant == null)
                return null;

            var fastestLap = personalBestLapState.GetFastestLap();
            var secondFastestLap = personalBestLapState.GetSecondFastestLap();
            var overrideDriver = driverOverrideState.GetModel(vehicleIdx);
            var teamDetails = GameSpecifics.GetTeamDetails(participantsState.State!.Header.GameYear, participant.TeamId);
            var sectorTimes = GetSectorTimeStatus(vehicleIdx, fastestLap, secondFastestLap);
            var carStatus = carStatusState.State?.Details.ElementAtOrDefault(vehicleIdx);

            return new StopwatchCar
            {
                VehicleIdx = vehicleIdx,
                Position = lap.CarPosition,
                LastLapTime = TimeUtility.MillisecondsToTime(lap.LastLapTimeInMS),
                CurrentTime = TimeUtility.MillisecondsToTime(lap.CurrentLapTimeInMS, 1),
                IsLapValid = !lap.CurrentLapInvalid.ToBool(),
                LapProgress = Convert.ToInt32((lap.LapDistance > 0 ? (lap.LapDistance / sessionState.State!.TrackLength) : 0) * 100),
                TyreCompoundVisual = (carStatus?.VisualTyreCompound ?? TyreCompoundVisual.Soft).ToString(),

                Sector1TimeStatus = sectorTimes?.Sector1Gap?.SectorTimeStatus,
                Sector2TimeStatus = sectorTimes?.Sector2Gap?.SectorTimeStatus,
                Sector3TimeStatus = sectorTimes?.Sector3Gap?.SectorTimeStatus,
                LapTimeStatus = sectorTimes?.LapGap?.SectorTimeStatus,

                Sector1GapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.Sector1Gap?.Gap),
                Sector2GapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.Sector1Gap?.Gap + sectorTimes?.Sector2Gap?.Gap),
                Sector3GapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.Sector3Gap?.Gap),
                LapGapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.LapGap?.Gap),
                Sector1TimeStatusRelativeToPole = sectorTimes?.Sector1Gap?.SectorTimeStatusRelativeToPole,
                Sector2TimeStatusRelativeToPole = sectorTimes?.Sector2Gap?.SectorTimeStatusRelativeToPole,
                LapTimeStatusRelativeToPole = sectorTimes?.LapGap?.SectorTimeStatusRelativeToPole
            };
        }

        private IEnumerable<byte> FindDriversOnFlyingLap()
        {
            var driversOnFlyingLap = driversOnFlyingLapState.GetAll();

            var driversThatFinishedLap = driversOnFlyingLap.Where(driver => driver.MarkedForDeletion && !driver.IgnoreFiltering)
                                            .OrderBy(driver => driver.FrameIdentifier)
                                            .Select(driver => driver.VehicleIdx)
                                            .ToList();

            var driversStillOnFlyingLap = driversOnFlyingLap.Where(driver => !driver.MarkedForDeletion || driver.IgnoreFiltering)
                                            .OrderByDescending(driver => driver.LapDistance < 0.0f ? 0.0f : driver.LapDistance)
                                            .Select(driver => driver.VehicleIdx)
                                            .ToList();

            return driversThatFinishedLap.Concat(driversStillOnFlyingLap).Take(4);
        }

        private LapTimeComparison? GetSectorTimeStatus(int vehicleIdx, PersonalBestLap? fastestLap, PersonalBestLap? secondFastestLap)
        {
            var latestLapTimes = latestLapTimeState.GetModel(vehicleIdx);

            if (latestLapTimes == null)
                return null;

            var personalBestLap = personalBestLapState.GetModel(vehicleIdx);
            var fastestSectors = fastestSectorTimeState.State;

            SectorTimeComparison? s1Gap = null;
            SectorTimeComparison? s2Gap = null;
            SectorTimeComparison? s3Gap = null;
            SectorTimeComparison? lapGap = null;

            if (latestLapTimes.Sector1Changed.GetValueOrDefault() && fastestSectors.TryGetValue((int)Sector.Sector1, out var fastestSector1) && fastestLap != null)
            {
                ushort? personalBestS1 = latestLapTimes.LapTimeChanged.GetValueOrDefault() ? personalBestLap?.PreviousBestLap?.Sector1TimeInMS : personalBestLap?.Sector1TimeInMS;
                s1Gap = new SectorTimeComparison
                {
                    Gap = latestLapTimes.Sector1TimeInMS - fastestLap.Sector1TimeInMS,
                    SectorTimeStatus = LapTimingUtility.CompareSectorTimes(latestLapTimes.Sector1TimeInMS,
                                                                            fastestSector1.TimeInMS,
                                                                            personalBestS1 ?? latestLapTimes.Sector1TimeInMS),
                    SectorTimeStatusRelativeToPole = LapTimingUtility.CompareSectorTimes(latestLapTimes.Sector1TimeInMS, 
                                                                                        fastestLap.Sector1TimeInMS)
                };
            }

            if (latestLapTimes.Sector2Changed.GetValueOrDefault() && fastestSectors.TryGetValue((int)Sector.Sector2, out var fastestSector2) && fastestLap != null)
            {
                ushort? personalBestS2 = latestLapTimes.LapTimeChanged.GetValueOrDefault() ? personalBestLap?.PreviousBestLap?.Sector2TimeInMS : personalBestLap?.Sector2TimeInMS;
                s2Gap = new SectorTimeComparison
                {
                    Gap = latestLapTimes.Sector2TimeInMS - fastestLap.Sector2TimeInMS,
                    SectorTimeStatus = LapTimingUtility.CompareSectorTimes(latestLapTimes.Sector2TimeInMS,
                                                                          fastestSector2.TimeInMS,
                                                                          personalBestS2 ?? latestLapTimes.Sector2TimeInMS),
                    SectorTimeStatusRelativeToPole = LapTimingUtility.CompareSectorTimes((uint)(latestLapTimes.Sector1TimeInMS+latestLapTimes.Sector2TimeInMS), 
                                                                                        (uint)(fastestLap.Sector1TimeInMS+fastestLap.Sector2TimeInMS))
                };
            }

            if (latestLapTimes.Sector3Changed.GetValueOrDefault() && fastestSectors.TryGetValue((int)Sector.Sector3, out var fastestSector3) && fastestLap != null)
            {
                ushort? personalBestS3 = latestLapTimes.LapTimeChanged.GetValueOrDefault() ? personalBestLap?.PreviousBestLap?.Sector3TimeInMS : personalBestLap?.Sector3TimeInMS;
                s3Gap = new SectorTimeComparison
                {
                    Gap = latestLapTimes.Sector3TimeInMS - fastestLap.Sector3TimeInMS,
                    SectorTimeStatus = LapTimingUtility.CompareSectorTimes(latestLapTimes.Sector3TimeInMS,
                                                                          fastestSector3.TimeInMS,
                                                                          personalBestS3 ?? latestLapTimes.Sector3TimeInMS)
                };
            }

            if (latestLapTimes.LapTimeChanged.GetValueOrDefault() && fastestLap != null)
            {
                uint poleLap = 0;
                if (fastestLap.LapTimeInMS == latestLapTimes.LapTimeInMS
                      && secondFastestLap != null)
                {
                    if (fastestLap?.PreviousBestLap == null
                        || secondFastestLap.LapTimeInMS < fastestLap.PreviousBestLap.LapTimeInMS)
                    { // When someone takes pole position from someone
                        poleLap = secondFastestLap.LapTimeInMS;
                    }
                    else if (secondFastestLap.LapTimeInMS >= fastestLap.PreviousBestLap.LapTimeInMS)
                    { // When someone improves on their own pole position
                        poleLap = fastestLap.PreviousBestLap.LapTimeInMS;
                    }
                }
                else
                    poleLap = fastestLap?.LapTimeInMS ?? 0;

                lapGap = new SectorTimeComparison
                {
                    Gap = (int)(latestLapTimes.LapTimeInMS - poleLap),
                    SectorTimeStatus = LapTimingUtility.CompareSectorTimes(latestLapTimes.LapTimeInMS,
                                                                            poleLap,
                                                                            personalBestLap?.LapTimeInMS ?? latestLapTimes.LapTimeInMS),
                    SectorTimeStatusRelativeToPole = LapTimingUtility.CompareSectorTimes(latestLapTimes.LapTimeInMS,
                                                                                        poleLap)
                };
            }

            return new LapTimeComparison
            {
                Sector1Gap = s1Gap,
                Sector2Gap = s2Gap,
                Sector3Gap = s3Gap,
                LapGap = lapGap
            };
        }


        private FastestQualifyingLap? GetFastestLapDetails(PersonalBestLap? lapTime)
        {
            if (lapTime == null || participantsState?.State == null) return null;

            var gameYear = participantsState.State.Header.GameYear;
            var flParticipant = participantsState.State.ParticipantList.ElementAtOrDefault(lapTime.VehicleIdx);
            var flOverrideDriver = driverOverrideState.GetModel(lapTime.VehicleIdx);

            return new FastestQualifyingLap
            {
                Driver = new DriverBasicDetails
                {
                    VehicleIdx = lapTime.VehicleIdx,
                    TeamId = flParticipant?.TeamId ?? 0,
                    TeamDetails = GameSpecifics.GetTeamDetails(gameYear, flParticipant?.TeamId ?? 0),
                    Name = flOverrideDriver?.Player?.Name ?? flParticipant?.Name ?? "Unknown"
                },
                LapTime = TimeUtility.MillisecondsToTime(lapTime.LapTimeInMS),
                Sector1Time = TimeUtility.MillisecondsToTime(lapTime.Sector1TimeInMS),
                Sector1And2Time = TimeUtility.MillisecondsToTime(lapTime.Sector1TimeInMS + lapTime.Sector2TimeInMS)
            };
        }
    }

    public class LapTimeComparison
    {
        public SectorTimeComparison? Sector1Gap { get; set; }
        public SectorTimeComparison? Sector2Gap { get; set; }
        public SectorTimeComparison? Sector3Gap { get; set; }
        public SectorTimeComparison? LapGap { get; set; }
    }

    public class SectorTimeComparison
    {
        public int Gap { get; set; }
        public SectorTimeStatus? SectorTimeStatus { get; set; }
        public SectorTimeStatus? SectorTimeStatusRelativeToPole { get; set; }
    }
}
