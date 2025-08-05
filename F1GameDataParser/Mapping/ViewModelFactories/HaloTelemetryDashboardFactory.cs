using F1GameDataParser.State;
using F1GameDataParser.ViewModels.TelemetryDashboard;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class HaloTelemetryDashboardFactory
    {
        private readonly CarTelemetryState carTelemetryState;
        private readonly SessionState sessionState;

        public HaloTelemetryDashboardFactory(CarTelemetryState carTelemetryState, SessionState sessionState)
        {
            this.carTelemetryState = carTelemetryState;
            this.sessionState = sessionState;
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

            return new HaloTelemetryDashboard
            {
                VehicleIdx = vehicleIdx,
                Speed = carTelemetry.Speed,
                Throttle = carTelemetry.Throttle,
                Brake = carTelemetry.Brake,
                Gear = carTelemetry.Gear,
                EngineRPM = carTelemetry.EngineRPM,
                DRS = carTelemetry.DRS,
                RevLightsPercent = carTelemetry.RevLightsPercent,
            };
        }
    }
}
