using F1GameDataParser.Services;
using F1GameDataParser.State;
using F1GameDataParser.State.WidgetStates;
using F1GameDataParser.ViewModels.SpeedTrapLeaderboard;

namespace F1GameDataParser.Mapping.ViewModelFactories;

public class SpeedTrapLeaderboardFactory : ViewModelFactoryBase<SpeedTrapCar>
{
    private readonly LapState lapState;
    private readonly SpeedTrapLeaderboardState speedTrapLeaderboardState;
    private readonly DriverOverrideService driverOverrideService;

    public SpeedTrapLeaderboardFactory(LapState lapState, 
                                    SpeedTrapLeaderboardState speedTrapLeaderboardState,
                                    DriverOverrideService driverOverrideService)
    {
        this.lapState = lapState;
        this.speedTrapLeaderboardState = speedTrapLeaderboardState;
        this.driverOverrideService = driverOverrideService;
    }

    public override IList<SpeedTrapCar>? GenerateList()
    {
        if (lapState?.State == null) 
            return null;

        var sortedCars = lapState.GetAll().Select((l, index) => new
        {
            VehicleIdx = index,
            Speed = l.SpeedTrapFastestSpeed
        })
        .Where(x => x.Speed != 0)
        .OrderByDescending(x => x.Speed)
        .Select((x, index) => new
        {
            OrdinalNumber = index + 1,
            VehicleIdx = x.VehicleIdx,
            Speed = x.Speed
        });

        List<SpeedTrapCar> cars = new List<SpeedTrapCar>();
        int i = 0;

        foreach (var vehicle in sortedCars) 
        {
            i++;
            if (i > 5 || (speedTrapLeaderboardState?.State != null 
                        && speedTrapLeaderboardState.State.SelectedVehicles.Count() > 0
                        && !speedTrapLeaderboardState.State.SelectedVehicles.Contains(vehicle.VehicleIdx)))
                continue;

            var fastestCar = cars.FirstOrDefault();
            short speed = 0;
            if (fastestCar == null && vehicle.Speed != null)
                speed = (short)Math.Round(vehicle.Speed.Value);
            else if (fastestCar != null && vehicle.Speed != null)
                speed = (short)Math.Round(vehicle.Speed.Value - fastestCar.Speed);

            cars.Add(new SpeedTrapCar
            {
                VehicleIdx = vehicle.VehicleIdx,
                Driver = driverOverrideService.GetDriverBasicDetails(vehicle.VehicleIdx),
                Speed = speed,
                OrdinalNumber = vehicle.OrdinalNumber,
                NeedDivider = cars.LastOrDefault()?.OrdinalNumber - vehicle.OrdinalNumber >= 2
            });
        }

        return cars;
    }
}
