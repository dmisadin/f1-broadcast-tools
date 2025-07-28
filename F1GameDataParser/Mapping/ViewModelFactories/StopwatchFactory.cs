using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Utility;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels;
using F1GameDataParser.ViewModels.Enums;
using F1GameDataParser.ViewModels.Stopwatch;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class StopwatchFactory : ViewModelFactoryBase<Stopwatch>
    {
        private readonly LapState lapState;
        private readonly ParticipantsState participantsState;
        private readonly SessionState sessionState;
        private readonly SessionHistoryState sessionHistoryState;
        private readonly PersonalBestLapState personalBestLapState;
        private readonly LatestLapTimeState latestLapTimeState;
        private readonly DriversOnFlyingLapState driversOnFlyingLapState;
        private readonly DriverOverrideState driverOverrideState;
        private readonly CarStatusState carStatusState;

        public StopwatchFactory(LapState lapState,
                                ParticipantsState participantsState,
                                SessionState sessionState,
                                SessionHistoryState sessionHistoryState,
                                PersonalBestLapState personalBestLapState,
                                LatestLapTimeState latestLapTimeState,
                                DriversOnFlyingLapState driversOnFlyingLapState,
                                DriverOverrideState driverOverrideState,
                                CarStatusState carStatusState)
        {
            this.lapState = lapState;
            this.participantsState = participantsState;
            this.sessionState = sessionState;
            this.sessionHistoryState = sessionHistoryState;
            this.personalBestLapState = personalBestLapState;
            this.latestLapTimeState = latestLapTimeState;
            this.driversOnFlyingLapState = driversOnFlyingLapState;
            this.driverOverrideState = driverOverrideState;
            this.carStatusState = carStatusState;
        }
        public override Stopwatch? Generate()
        {
            if (lapState?.State == null || participantsState?.State == null || sessionState?.State == null)
                return null;

            var vehicleIdxOnHotlap = FindDriversOnFlyingLap();
            var stopwatchCars = new List<StopwatchCar>();

            var gameYear = participantsState.State.Header.GameYear;
            var participants = participantsState.State.ParticipantList;
            var laps = lapState.State;

            foreach (var vehicleIdx in vehicleIdxOnHotlap)
            {
                var participant = participants.ElementAtOrDefault(vehicleIdx);
                var lap = laps.ElementAtOrDefault(vehicleIdx).Value;

                var overrideDriver = driverOverrideState.GetModel(vehicleIdx);
                var teamDetails = GameSpecifics.GetTeamDetails(gameYear, participant?.TeamId ?? 0);

                // if (currentLap is null) continue; // better handling for session start, no fastestLap or personalBestLap; probably find fastest times from LatestLapTimeState
                var sectorTimes = GetSectorTimeStatus(vehicleIdx);
                var carStatus = carStatusState.State?.Details.ElementAtOrDefault(vehicleIdx);

                var stopwatchCar = new StopwatchCar
                {
                    Driver = new DriverBasicDetails
                    {
                        VehicleIdx = vehicleIdx,
                        TeamId = participant?.TeamId ?? 0,
                        TeamDetails = teamDetails,
                        Name = overrideDriver?.Player?.Name ?? participant?.Name ?? "Unknown"
                    },

                    Position = lap.CarPosition,
                    CurrentTime = TimeUtility.MillisecondsToGap(lap.CurrentLapTimeInMS, 1),
                    IsLapValid = lap.CurrentLapInvalid.ToBool(),
                    LapProgress = Convert.ToInt32((lap.LapDistance > 0 ? (lap.LapDistance / sessionState.State.TrackLength) : 0 ) * 100),
                    LapDistance =lap.LapDistance,
                    TyreCompoundVisual = (carStatus?.VisualTyreCompound ?? TyreCompoundVisual.Soft).ToString(),

                    Sector1TimeStatus = sectorTimes?.Sector1Gap?.SectorTimeStatus,
                    Sector2TimeStatus = sectorTimes?.Sector2Gap?.SectorTimeStatus,
                    Sector3TimeStatus = sectorTimes?.Sector3Gap?.SectorTimeStatus,
                    LapTimeStatus = sectorTimes?.LapGap?.SectorTimeStatus,

                    Sector1GapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.Sector1Gap?.Gap),
                    Sector2GapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.Sector2Gap?.Gap),
                    Sector3GapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.Sector3Gap?.Gap),
                    LapGapToLeader = TimeUtility.MillisecondsToDifference(sectorTimes?.LapGap?.Gap),
                };

                stopwatchCars.Add(stopwatchCar);
            }


            var fastestLap = personalBestLapState.GetFastestLap();
            if (fastestLap == null) return null;
            var flParticipant = participants.ElementAtOrDefault(fastestLap.VehicleIdx);
            var flOverrideDriver = driverOverrideState.GetModel(fastestLap.VehicleIdx);

            var leaderLap = new FastestQualifyingLap
            {
                Driver = new DriverBasicDetails
                {
                    VehicleIdx = fastestLap.VehicleIdx,
                    TeamId = flParticipant.TeamId,
                    TeamDetails = GameSpecifics.GetTeamDetails(gameYear, flParticipant?.TeamId ?? 0),
                    Name = flOverrideDriver?.Player?.Name ?? flParticipant?.Name ?? "Unknown"
                },
                LapTime = TimeUtility.MillisecondsToGap(fastestLap.LapTimeInMS),
                Sector1Time = TimeUtility.MillisecondsToGap(fastestLap.Sector1TimeInMS),
                Sector1And2Time = TimeUtility.MillisecondsToGap(fastestLap.Sector1TimeInMS + fastestLap.Sector2TimeInMS)
            };

            return new Stopwatch
            {
                LeaderLap = leaderLap,
                Cars = stopwatchCars,
            };
        }


        private IEnumerable<byte> FindDriversOnFlyingLap()
        {
            var driversOnFlyingLap = driversOnFlyingLapState.GetAll();

            var driversThatFinishedLap = driversOnFlyingLap.Where(driver => driver.MarkedForDeletion)
                                            .OrderBy(driver => driver.FrameIdentifier)
                                            .Select(driver => driver.VehicleIdx)
                                            .ToList();

            var driversStillOnFlyingLap = driversOnFlyingLap.Where(driver => !driver.MarkedForDeletion)
                                            .OrderByDescending(driver => driver.LapDistance < 0.0f ? 0.0f : driver.LapDistance)
                                            .Select(driver => driver.VehicleIdx)
                                            .ToList();

            return driversThatFinishedLap.Concat(driversStillOnFlyingLap);
        }

        private LapTimeComparison? GetSectorTimeStatus(int vehicleIdx)
        {
            var latestLapTimes = latestLapTimeState.GetModel(vehicleIdx);

            if (latestLapTimes == null)
                return null;

            var personalBestLap = personalBestLapState.GetModel(vehicleIdx);
            var fastestLap = personalBestLapState.GetFastestLap();
            var fastestSectors = personalBestLapState.GetFastestSectors();

            SectorTimeComparison? s1Gap = null;
            SectorTimeComparison? s2Gap = null;
            SectorTimeComparison? s3Gap = null;
            SectorTimeComparison? lapGap = null;

            if (latestLapTimes.Sector1Changed.GetValueOrDefault() && fastestSectors.TryGetValue(Sector.Sector1, out var fastestSector1) && fastestSector1.HasValue)
            {
                s1Gap = new SectorTimeComparison
                {
                    Gap = latestLapTimes.Sector1TimeInMS - fastestSector1.Value,
                    SectorTimeStatus = CompareSectorTimes(latestLapTimes.Sector1TimeInMS, 
                                                          fastestSector1.Value, 
                                                          personalBestLap?.Sector1TimeInMS ?? latestLapTimes.Sector1TimeInMS)
                };
            }

            if (latestLapTimes.Sector2Changed.GetValueOrDefault() && fastestSectors.TryGetValue(Sector.Sector2, out var fastestSector2) && fastestSector2.HasValue)
            {
                s2Gap = new SectorTimeComparison
                {
                    Gap = latestLapTimes.Sector2TimeInMS - fastestSector2.Value,
                    SectorTimeStatus = CompareSectorTimes(latestLapTimes.Sector2TimeInMS, 
                                                          fastestSector2.Value, 
                                                          personalBestLap?.Sector2TimeInMS ?? latestLapTimes.Sector2TimeInMS)
                };
            }

            if (latestLapTimes.Sector3Changed.GetValueOrDefault() && fastestSectors.TryGetValue(Sector.Sector3, out var fastestSector3) && fastestSector3.HasValue)
            {
                s3Gap = new SectorTimeComparison
                {
                    Gap = latestLapTimes.Sector3TimeInMS - fastestSector3.GetValueOrDefault(),
                    SectorTimeStatus = CompareSectorTimes(latestLapTimes.Sector3TimeInMS,
                                                          fastestSector3.Value, 
                                                          personalBestLap?.Sector3TimeInMS ?? latestLapTimes.Sector3TimeInMS)
                };
            }

            if (latestLapTimes.LapTimeChanged.GetValueOrDefault() && fastestLap != null)
            {
                var secondFastestLap = personalBestLapState.GetSecondFastestLap();
                var poleLap = fastestLap.LapTimeInMS == latestLapTimes.LapTimeInMS && secondFastestLap != null ? secondFastestLap.LapTimeInMS : fastestLap.LapTimeInMS;
                lapGap = new SectorTimeComparison
                {
                    Gap = (int)(latestLapTimes.LapTimeInMS - poleLap),
                    SectorTimeStatus = CompareSectorTimes(latestLapTimes.LapTimeInMS,
                                                          poleLap, 
                                                          personalBestLap?.LapTimeInMS ?? latestLapTimes.LapTimeInMS)
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

        private SectorTimeStatus? CompareSectorTimes(uint sectorTime, uint leaderSectorTime, uint personalBestSectorTime)
        {
            if (sectorTime <= 0)
                return null;
            if (sectorTime <= leaderSectorTime)
                return SectorTimeStatus.FasterThanLeader;
            if (sectorTime <= personalBestSectorTime)
                return SectorTimeStatus.PersonalBest;
            return SectorTimeStatus.NoImprovement;
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
    }
}
