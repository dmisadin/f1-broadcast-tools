using F1GameDataParser.Enums;
using F1GameDataParser.Services;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.State.WidgetStates;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels.SectorTimingComparison;

namespace F1GameDataParser.Mapping.ViewModelFactories;

public class SectorTimingComparisonFactory : ViewModelFactoryBase<SectorTimingComparison>
{
    private readonly SectorTimingComparisonState sectorTimingComparisonState;
    private readonly LatestLapTimeState latestLapTimeState;
    private readonly SessionHistoryState sessionHistoryState;
    private readonly SessionState sessionState;
    private readonly LapState lapState;
    private readonly CarStatusState carStatusState;
    private readonly DriverOverrideService driverOverrideService;

    public SectorTimingComparisonFactory(SectorTimingComparisonState sectorTimingComparisonState,
                                        LatestLapTimeState latestLapTimeState,
                                        SessionHistoryState sessionHistoryState,
                                        SessionState sessionState,
                                        LapState lapState,
                                        CarStatusState carStatusState,
                                        DriverOverrideService driverOverrideService)
    {
        this.sectorTimingComparisonState = sectorTimingComparisonState;
        this.latestLapTimeState = latestLapTimeState; // use only for qualifying
        this.sessionHistoryState = sessionHistoryState;
        this.sessionState = sessionState; // get sessiontype Q or Race
        this.lapState = lapState;
        this.carStatusState = carStatusState;
        this.driverOverrideService = driverOverrideService;
    }

    public override SectorTimingComparison? Generate()
    {
        if (latestLapTimeState?.State == null
            || sessionHistoryState?.State == null
            || sessionState?.State == null
            || lapState?.State == null
            || carStatusState?.State == null)
            return null;

        var vehicleIdx = sectorTimingComparisonState?.State?.VehicleIdx ?? sessionState.State.SpectatorCarIndex;

        if (!lapState.State.TryGetValue(vehicleIdx, out var lapDetails))
            return null;

        var comparingVehicleIdx = sectorTimingComparisonState?.State?.ComparingVehicleIdx ?? lapState.GetVehicleIdxAtPosition(lapDetails.CarPosition + 1);

        if (comparingVehicleIdx == null)
            return null;

        if (!lapState.State.TryGetValue(comparingVehicleIdx.Value, out var comparingLapDetails))
            return null;

        var isRaceSession = sessionState.State.SessionType >= SessionType.Race;

        var sessionHistory = sessionHistoryState.GetModel(vehicleIdx);

        var previousLapAndIndex = sectorTimingComparisonState?.State?.LapNumber is int lapNumber
            ? sessionHistory?.LapHistoryDetails
                .Select((lap, idx) => new { lap, idx })
                .ElementAtOrDefault(lapNumber - 1)
            : null;

        if (previousLapAndIndex == null) // Fallback: get previous completed lap
            previousLapAndIndex = sessionHistory?.LapHistoryDetails
                .Select((lap, idx) => new { lap, idx })
                .LastOrDefault(l => l.lap.LapTimeInMS > 0);

        if (previousLapAndIndex == null)
            return null;

        var lap = previousLapAndIndex.lap;
        var lapIndex = previousLapAndIndex.idx;
        var comparingSessionHistory = sessionHistoryState.GetModel(comparingVehicleIdx.Value);
        var comparingLap = comparingSessionHistory?.LapHistoryDetails.ElementAtOrDefault(lapIndex);

        if (comparingLap == null || comparingLap.LapTimeInMS == 0) // Try to match laps with comparingCar's current lap
        {
            lapIndex--;
            lap = sessionHistory?.LapHistoryDetails.ElementAtOrDefault(lapIndex);
            comparingLap = comparingSessionHistory?.LapHistoryDetails.ElementAtOrDefault(lapIndex);

            if (lap == null || comparingLap == null)
                return null;
        }

        var carStatus = carStatusState.State.Details.ElementAtOrDefault(vehicleIdx);
        var comparingCarStatus = carStatusState.State.Details.ElementAtOrDefault(comparingVehicleIdx.Value);
        var driver = driverOverrideService.GetDriverBasicDetails(vehicleIdx);
        var comparingDriver = driverOverrideService.GetDriverBasicDetails(comparingVehicleIdx.Value);

        if (carStatus == null || comparingCarStatus == null || driver == null || comparingDriver == null)
            return null;

        var tyreUsedInTheLap = sessionHistory?.TyreStintHistoryDetails.FirstOrDefault(t => lapIndex <= t.EndLap)?.TyreVisualCompound;
        var comparingTyreUsedInTheLap = comparingSessionHistory?.TyreStintHistoryDetails.FirstOrDefault(t => lapIndex <= t.EndLap)?.TyreVisualCompound;

        return new SectorTimingComparison
        {
            LapNumber = lapIndex + 1,
            DriverPreviousLapDetails = new DriverPreviousLapDetails
            {
                VehicleIdx = vehicleIdx,
                Position = lapDetails.CarPosition,
                VisualTyreCompound = (tyreUsedInTheLap ?? carStatus!.VisualTyreCompound).ToString(),
                Driver = driver,
                LapTiming = new LapTimingStatus
                {
                    LapTime = TimeUtility.MillisecondsToTime(lap.LapTimeInMS),
                    Sector1Time = TimeUtility.MillisecondsToTime(lap.Sector1TimeInMS),
                    Sector2Time = TimeUtility.MillisecondsToTime(lap.Sector2TimeInMS),
                    Sector3Time = TimeUtility.MillisecondsToTime(lap.Sector3TimeInMS)
                }
            },
            ComparingDriverPreviousLapDetails = new DriverPreviousLapDetails
            {
                VehicleIdx = comparingVehicleIdx.Value,
                Position = comparingLapDetails.CarPosition,
                VisualTyreCompound = (comparingTyreUsedInTheLap ?? comparingCarStatus!.VisualTyreCompound).ToString(),
                Driver = comparingDriver,
                LapTiming = new LapTimingStatus
                {
                    LapTime = TimeUtility.MillisecondsToDifference((long)comparingLap.LapTimeInMS - lap.LapTimeInMS) ?? "0.000",
                    Sector1Time = TimeUtility.MillisecondsToDifference(comparingLap.Sector1TimeInMS - lap.Sector1TimeInMS) ?? "0.000",
                    Sector2Time = TimeUtility.MillisecondsToDifference(comparingLap.Sector2TimeInMS - lap.Sector2TimeInMS) ?? "0.000",
                    Sector3Time = TimeUtility.MillisecondsToDifference(comparingLap.Sector3TimeInMS - lap.Sector3TimeInMS) ?? "0.000",
                    LapTimeStatus = LapTimingUtility.CompareSectorTimes(comparingLap.LapTimeInMS, lap.LapTimeInMS),
                    Sector1TimeStatus = LapTimingUtility.CompareSectorTimes(comparingLap.Sector1TimeInMS, lap.Sector1TimeInMS),
                    Sector2TimeStatus = LapTimingUtility.CompareSectorTimes(comparingLap.Sector2TimeInMS, lap.Sector2TimeInMS),
                    Sector3TimeStatus = LapTimingUtility.CompareSectorTimes(comparingLap.Sector3TimeInMS, lap.Sector3TimeInMS)
                }
            }
        };
    }
}
