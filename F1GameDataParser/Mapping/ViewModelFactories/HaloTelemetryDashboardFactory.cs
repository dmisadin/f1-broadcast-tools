using F1GameDataParser.State;
using F1GameDataParser.ViewModels.TelemetryDashboard;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class HaloTelemetryDashboardFactory
    {
        private readonly CarTelemetryState carTelemetryState;
        private readonly SessionState sessionState;
        private readonly CarStatusState carStatusState;

        public HaloTelemetryDashboardFactory(CarTelemetryState carTelemetryState, SessionState sessionState, CarStatusState carStatusState)
        {
            this.carTelemetryState = carTelemetryState;
            this.sessionState = sessionState;
            this.carStatusState = carStatusState;
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
            };
        }
    }
}
