using F1GameDataParser.Enums;
using F1GameDataParser.Services;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.State.WidgetStates;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels.LastLapSectorComparison;
using F1GameDataParser.ViewModels.PreviousLapSectorComparison;

namespace F1GameDataParser.Mapping.ViewModelFactories;

public class PreviousLapSectorComparisonFactory : ViewModelFactoryBase<PreviousLapSectorComparison>
{
    private readonly PreviousLapSectorComparisonState previousLapSectorComparisonState;
    private readonly LatestLapTimeState latestLapTimeState;
    private readonly SessionHistoryState sessionHistoryState;
    private readonly SessionState sessionState;
    private readonly LapState lapState;
    private readonly CarStatusState carStatusState;
    private readonly DriverOverrideService driverOverrideService;

    public PreviousLapSectorComparisonFactory(PreviousLapSectorComparisonState previousLapSectorComparisonState,
                                        LatestLapTimeState latestLapTimeState,
                                        SessionHistoryState sessionHistoryState,
                                        SessionState sessionState,
                                        LapState lapState,
                                        CarStatusState carStatusState,
                                        DriverOverrideService driverOverrideService)
    {
        this.previousLapSectorComparisonState = previousLapSectorComparisonState;
        this.latestLapTimeState = latestLapTimeState; // use only for qualifying
        this.sessionHistoryState = sessionHistoryState;
        this.sessionState = sessionState; // get sessiontype Q or Race
        this.lapState = lapState;
        this.carStatusState = carStatusState;
        this.driverOverrideService = driverOverrideService;
    }

    public override PreviousLapSectorComparison? Generate()
    {
        if (latestLapTimeState?.State == null
            || sessionHistoryState?.State == null
            || sessionState?.State == null
            || lapState?.State == null
            || carStatusState?.State == null)
            return null;

        var vehicleIdx = previousLapSectorComparisonState?.State?.VehicleIdx ?? sessionState.State.SpectatorCarIndex;

        if (!lapState.State.TryGetValue(vehicleIdx, out var lapDetails))
            return null;

        var comparingVehicleIdx = previousLapSectorComparisonState?.State?.ComparingVehicleIdx ?? lapState.GetVehicleIdxAtPosition(lapDetails.CarPosition + 1);

        if (comparingVehicleIdx == null)
            return null;

        if (!lapState.State.TryGetValue(comparingVehicleIdx.Value, out var comparingLapDetails))
            return null;

        var isRaceSession = sessionState.State.SessionType >= SessionType.Race;

        var previousLapAndIndex = previousLapSectorComparisonState?.State?.LapNumber is int lapNumber
            ? sessionHistoryState.GetModel(vehicleIdx)?.LapHistoryDetails
                .Select((lap, idx) => new { lap, idx })
                .ElementAtOrDefault(lapNumber - 1)
            : null;

        if (previousLapAndIndex == null) // Fallback: get previous completed lap
            previousLapAndIndex = sessionHistoryState.GetModel(vehicleIdx)?.LapHistoryDetails
                .Select((lap, idx) => new { lap, idx })
                .LastOrDefault(l => l.lap.LapTimeInMS > 0);

        if (previousLapAndIndex == null)
            return null;

        var previousLap = previousLapAndIndex.lap;
        var previousLapIndex = previousLapAndIndex.idx;
        var comparingPreviousLap = sessionHistoryState.GetModel(comparingVehicleIdx.Value)?.LapHistoryDetails.ElementAtOrDefault(previousLapIndex);

        if (comparingPreviousLap == null || comparingPreviousLap.LapTimeInMS == 0) // Try to match laps with comparingCar's current lap
        {
            previousLapIndex--;
            previousLap = sessionHistoryState.GetModel(vehicleIdx)?.LapHistoryDetails.ElementAtOrDefault(previousLapIndex);
            comparingPreviousLap = sessionHistoryState.GetModel(comparingVehicleIdx.Value)?.LapHistoryDetails.ElementAtOrDefault(previousLapIndex);

            if (previousLap == null || comparingPreviousLap == null)
                return null;
        }

        var carStatus = carStatusState.State.Details.ElementAtOrDefault(vehicleIdx);
        var comparingCarStatus = carStatusState.State.Details.ElementAtOrDefault(comparingVehicleIdx.Value);
        var driver = driverOverrideService.GetDriverBasicDetails(vehicleIdx);
        var comparingDriver = driverOverrideService.GetDriverBasicDetails(comparingVehicleIdx.Value);

        if (carStatus == null || comparingCarStatus == null || driver == null || comparingDriver == null)
            return null;

        return new PreviousLapSectorComparison
        {
            LapNumber = previousLapIndex + 1,
            DriverPreviousLapDetails = new DriverPreviousLapDetails
            {
                VehicleIdx = vehicleIdx,
                Position = lapDetails.CarPosition,
                VisualTyreCompound = carStatus!.VisualTyreCompound.ToString(),
                Driver = driver,
                LapTiming = new LapTimingStatus
                {
                    LapTime = TimeUtility.MillisecondsToTime(previousLap.LapTimeInMS),
                    Sector1Time = TimeUtility.MillisecondsToTime(previousLap.Sector1TimeInMS),
                    Sector2Time = TimeUtility.MillisecondsToTime(previousLap.Sector2TimeInMS),
                    Sector3Time = TimeUtility.MillisecondsToTime(previousLap.Sector3TimeInMS)
                }
            },
            ComparingDriverPreviousLapDetails = new DriverPreviousLapDetails
            {
                VehicleIdx = comparingVehicleIdx.Value,
                Position = comparingLapDetails.CarPosition,
                VisualTyreCompound = comparingCarStatus!.VisualTyreCompound.ToString(),
                Driver = comparingDriver,
                LapTiming = new LapTimingStatus
                {
                    LapTime = TimeUtility.MillisecondsToDifference((long)comparingPreviousLap.LapTimeInMS - previousLap.LapTimeInMS) ?? "0.000",
                    Sector1Time = TimeUtility.MillisecondsToDifference(comparingPreviousLap.Sector1TimeInMS - previousLap.Sector1TimeInMS) ?? "0.000",
                    Sector2Time = TimeUtility.MillisecondsToDifference(comparingPreviousLap.Sector2TimeInMS - previousLap.Sector2TimeInMS) ?? "0.000",
                    Sector3Time = TimeUtility.MillisecondsToDifference(comparingPreviousLap.Sector3TimeInMS - previousLap.Sector3TimeInMS) ?? "0.000",
                    LapTimeStatus = LapTimingUtility.CompareSectorTimes(comparingPreviousLap.LapTimeInMS, previousLap.LapTimeInMS),
                    Sector1TimeStatus = LapTimingUtility.CompareSectorTimes(comparingPreviousLap.Sector1TimeInMS, previousLap.Sector1TimeInMS),
                    Sector2TimeStatus = LapTimingUtility.CompareSectorTimes(comparingPreviousLap.Sector2TimeInMS, previousLap.Sector2TimeInMS),
                    Sector3TimeStatus = LapTimingUtility.CompareSectorTimes(comparingPreviousLap.Sector3TimeInMS, previousLap.Sector3TimeInMS)
                }
            }
        };
    }
}
