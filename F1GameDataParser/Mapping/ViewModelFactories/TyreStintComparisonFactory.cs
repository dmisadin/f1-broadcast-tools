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
            var spectatedAndFollowingVehicleIdxs = lapState.GetCarAndFollowingCarVehicleIdx(spectatedCarIdx);

            if (spectatedAndFollowingVehicleIdxs != null)
                carIdxs.AddRange(spectatedAndFollowingVehicleIdxs);
        }
        else
            return null;


        var selectedVehicleSessionHistories = sessionHistoryState.GetModels(carIdxs);
        int currentLap = lapState.GetLeadingLapNumber();

        return selectedVehicleSessionHistories.Select(v => new TyreStintComparison
        {
            Driver = driverOverrideService.GetDriverBasicDetails(v.CarIdx),
            TyreStints = v.TyreStintHistoryDetails.Select((t, index) =>
                {
                    var previousStint = v.TyreStintHistoryDetails.ElementAtOrDefault(index - 1);
                    byte? endLap = t.EndLap == byte.MaxValue ? null : t.EndLap;
                    int duration = previousStint == null
                        ? (endLap ?? currentLap)
                        : (endLap ?? currentLap) - previousStint.EndLap;
                    byte totalLaps = sessionState?.State?.TotalLaps ?? 1;

                    return new TyreStint
                    {
                        TyreCompound = t.TyreVisualCompound.ToString().ToLower(),
                        EndLap = endLap,
                        Duration = (byte)duration,
                        SizePercentage = (byte)(duration * 100 / totalLaps)
                    };
                })
        }).ToList();
    }
}
