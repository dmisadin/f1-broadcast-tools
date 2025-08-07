using F1GameDataParser.Enums;
using F1GameDataParser.Services;
using F1GameDataParser.State;
using F1GameDataParser.ViewModels;
using F1GameDataParser.ViewModels.TelemetryDashboard;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class HaloTelemetryDashboardFactory
    {
        private readonly CarTelemetryState carTelemetryState;
        private readonly SessionState sessionState;
        private readonly CarStatusState carStatusState;
        private readonly DriverOverrideService driverOverrideService;
        private readonly LapState lapState;

        public HaloTelemetryDashboardFactory(CarTelemetryState carTelemetryState, 
                                             SessionState sessionState, 
                                             CarStatusState carStatusState, 
                                             DriverOverrideService driverOverrideService,
                                             LapState lapState)
        {
            this.carTelemetryState = carTelemetryState;
            this.sessionState = sessionState;
            this.carStatusState = carStatusState;
            this.driverOverrideService = driverOverrideService;
            this.lapState = lapState;
        }

        public HaloTelemetryDashboard? Generate()
        {
            if (carTelemetryState?.State == null
                || sessionState?.State == null)
                return null;

            var vehicleIdx = sessionState.State.SpectatorCarIndex;

            if (vehicleIdx == 255)
                vehicleIdx = sessionState.State.Header.PlayerCarIndex;

            var carTelemetry = carTelemetryState.State.CarTelemetryDetails.ElementAtOrDefault(vehicleIdx);

            if (carTelemetry == null)
                return null;

            var carStatus = carStatusState.State?.Details.ElementAtOrDefault(vehicleIdx);
            double maxRpm = carStatus?.MaxRPM ?? 13500;
            double idleRpm = carStatus?.IdleRPM ?? 3000;

            DriverBasicDetails? nextDriver = null;
            var driverLapState = lapState.GetModel(vehicleIdx);

            if (driverLapState != null && driverLapState.CarPosition > 1)
            {
                var nextDriverVehicleIdx = lapState.State.Values.ToList().FindIndex(driver => driver.CarPosition == driverLapState.CarPosition - 1);
                nextDriver = driverOverrideService.GetDriverBasicDetails(nextDriverVehicleIdx);
            }

            return new HaloTelemetryDashboard
            {
                VehicleIdx = vehicleIdx,
                Speed = carTelemetry.Speed,
                Throttle = carTelemetry.Throttle,
                Brake = carTelemetry.Brake,
                Gear = carTelemetry.Gear,
                EngineRPM = carTelemetry.EngineRPM,
                EngineRPMPercentage = 1 - ((carTelemetry.EngineRPM - idleRpm) / (maxRpm - idleRpm)),
                DRS = carTelemetry.DRS,
                Position = driverLapState?.CarPosition ?? 0,
                ErsDeployMode = carStatus?.ErsDeployMode ?? ERSDeployMode.None,
                Driver = driverOverrideService.GetDriverBasicDetails(vehicleIdx),
                NextDriver = nextDriver,
            };
        }
    }
}
