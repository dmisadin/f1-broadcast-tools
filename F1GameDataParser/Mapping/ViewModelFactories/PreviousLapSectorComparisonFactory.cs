using F1GameDataParser.Enums;
using F1GameDataParser.State;
using F1GameDataParser.State.ComputedStates;
using F1GameDataParser.State.WidgetStates;
using F1GameDataParser.ViewModels;
using F1GameDataParser.ViewModels.LastLapSectorComparison;

namespace F1GameDataParser.Mapping.ViewModelFactories;

public class PreviousLapSectorComparisonFactory : ViewModelFactoryBase<PreviousLapSectorComparison>
{
    private readonly PreviousLapSectorComparisonState previousLapSectorComparisonState;
    private readonly LatestLapTimeState latestLapTimeState;
    private readonly SessionHistoryState sessionHistoryState;
    private readonly SessionState sessionState;
    private readonly LapState lapState;

    public PreviousLapSectorComparisonFactory(PreviousLapSectorComparisonState previousLapSectorComparisonState,
                                        LatestLapTimeState latestLapTimeState,
                                        SessionHistoryState sessionHistoryState,
                                        SessionState sessionState,
                                        LapState lapState)
    {
        this.previousLapSectorComparisonState = previousLapSectorComparisonState;
        this.latestLapTimeState = latestLapTimeState; // use only for qualifying
        this.sessionHistoryState = sessionHistoryState;
        this.sessionState = sessionState; // get sessiontype Q or Race
        this.lapState = lapState;
    }

    public override PreviousLapSectorComparison? Generate()
    {
        if (latestLapTimeState?.State == null
            || sessionHistoryState?.State == null
            || sessionState?.State == null
            || lapState?.State == null)
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

        var previousLap = sessionHistoryState.GetModel(vehicleIdx)?.LapHistoryDetails.LastOrDefault(l => l.LapTimeInMS > 0);
        var comparingPreviousLap = sessionHistoryState.GetModel(comparingVehicleIdx.Value)?.LapHistoryDetails.LastOrDefault(l => l.LapTimeInMS > 0);

        if (previousLap == null || comparingPreviousLap == null)
            return null;

        return new PreviousLapSectorComparison
        {
            DriverPreviousLapDetails = new DriverPreviousLapDetails
            {
                VehicleIdx = vehicleIdx,
                Position = lapDetails.CarPosition,
                LapTiming = new LapTiming
                {
                    LapTimeInMS = previousLap.LapTimeInMS,
                    Sector1TimeInMS = previousLap.Sector1TimeInMS,
                    Sector2TimeInMS = previousLap.Sector2TimeInMS,
                    Sector3TimeInMS = previousLap.Sector3TimeInMS,
                }
            },
            ComparingDriverPreviousLapDetails = new DriverPreviousLapDetails
            {
                VehicleIdx = comparingVehicleIdx.Value,
                Position = comparingLapDetails.CarPosition,
                LapTiming = new LapTiming
                {
                    LapTimeInMS = comparingPreviousLap.LapTimeInMS,
                    Sector1TimeInMS = comparingPreviousLap.Sector1TimeInMS,
                    Sector2TimeInMS = comparingPreviousLap.Sector2TimeInMS,
                    Sector3TimeInMS = comparingPreviousLap.Sector3TimeInMS,
                }
            }
        };
    }
}
