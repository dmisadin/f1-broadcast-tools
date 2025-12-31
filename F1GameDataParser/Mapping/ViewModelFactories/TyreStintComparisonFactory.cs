using F1GameDataParser.Services;
using F1GameDataParser.State;
using F1GameDataParser.State.WidgetStates;
using F1GameDataParser.ViewModels.TyreStintComparison;

namespace F1GameDataParser.Mapping.ViewModelFactories;

public class TyreStintComparisonFactory : ViewModelFactoryBase<TyreStintComparison>
{
    private readonly SessionHistoryState sessionHistoryState;
    private readonly TyreStintComparisonState tyreStintComparisonState;
    private readonly SessionState sessionState;
    private readonly LapState lapState;
    private readonly DriverOverrideService driverOverrideService;

    public TyreStintComparisonFactory(SessionHistoryState sessionHistoryState,
                                      TyreStintComparisonState tyreStintComparisonState,
                                      SessionState sessionState,
                                      LapState lapState,
                                      DriverOverrideService driverOverrideService)
    {
        this.sessionHistoryState = sessionHistoryState;
        this.tyreStintComparisonState = tyreStintComparisonState;
        this.sessionState = sessionState;
        this.lapState = lapState;
        this.driverOverrideService = driverOverrideService;
    }

    public override List<TyreStintComparison>? GenerateList()
    {
        if (sessionHistoryState?.State == null) 
            return null;

        List<int> carIdxs = new List<int>();

        if (tyreStintComparisonState?.State != null
            && tyreStintComparisonState.State.SelectedVehicles.Count() > 0)
        {
            carIdxs = tyreStintComparisonState.State.SelectedVehicles;
        }
        else if (sessionState?.State != null)
        {
            int spectatedCarIdx = sessionState.State.SpectatorCarIndex;
            var spectatedAndFollowingVehicleIdxs = lapState.GetModelAndFollowingCarModel(spectatedCarIdx)?.Select(l => (int)l.CarPosition);

            if (spectatedAndFollowingVehicleIdxs != null)
                carIdxs.AddRange(spectatedAndFollowingVehicleIdxs);
        }
        else
            return null;


        var selectedVehicleSessionHistories = sessionHistoryState.GetModels(carIdxs);

        return selectedVehicleSessionHistories.Select(v => new TyreStintComparison
        {
            Driver = driverOverrideService.GetDriverBasicDetails(v.CarIdx),
            TyreStints = v.TyreStintHistoryDetails.Select((t, index) =>
                {
                    var previousStint = v.TyreStintHistoryDetails.ElementAtOrDefault(index - 1);
                    byte duration = previousStint == null
                        ? t.EndLap
                        : (byte)(t.EndLap - previousStint.EndLap);
                    byte totalLaps = sessionState?.State?.TotalLaps ?? 1;

                    return new TyreStint
                    {
                        TyreCompound = t.TyreVisualCompound.ToString().ToLower(),
                        EndLap = t.EndLap,
                        Duration = duration,
                        SizePercentage = (byte)(duration * 100 / totalLaps)
                    };
                })
        }).ToList();
    }
}
