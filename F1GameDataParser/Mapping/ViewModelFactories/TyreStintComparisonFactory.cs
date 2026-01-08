using F1GameDataParser.GameProfiles.F1Common.Constants;
using F1GameDataParser.Models.SessionHistory;
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

    public override TyreStintComparison? Generate()
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
        byte currentLap = lapState.GetLeadingLapNumber();
        byte totalLaps = sessionState?.State?.TotalLaps ?? 1;

        var pitStopLapMarkers = new HashSet<byte> { 1 };

        var cars = new List<CarTyreStints>();

        foreach (var v in selectedVehicleSessionHistories)
        {
            var carTyreStints = new CarTyreStints
            {
                Driver = driverOverrideService.GetDriverBasicDetails(v.CarIdx),
                TyreStints = new List<TyreStint>()
            };

            TyreStintHistoryDetails? previousStint = null;

            foreach (var t in v.TyreStintHistoryDetails)
            {
                byte? endLap = t.EndLap == byte.MaxValue ? null : t.EndLap;

                int duration = previousStint == null
                    ? (endLap ?? currentLap)
                    : (endLap ?? currentLap) - previousStint.EndLap;

                pitStopLapMarkers.Add(endLap ?? currentLap);

                carTyreStints.TyreStints.Add(new TyreStint
                {
                    TyreCompound = t.TyreVisualCompound.ToString().ToLower(),
                    TyreColor = Tyres.Colors.GetValueOrDefault(t.TyreVisualCompound) ?? "#fff",
                    EndLap = endLap,
                    Duration = (byte)duration,
                    SizePercentage = (byte)(duration * 100 / currentLap)
                });

                previousStint = t;
            }

            cars.Add(carTyreStints);
        }

        pitStopLapMarkers.Add(totalLaps);

        return new TyreStintComparison
        {
            TotalLaps = totalLaps,
            TotalSizePercentage = (byte)Math.Ceiling(currentLap * 100.0 / totalLaps),
            Cars = cars,
            PitStopLapMarkers = pitStopLapMarkers.OrderBy(lap => lap).ToList()
        };
    }
}
