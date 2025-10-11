using F1GameDataParser.State;
using F1GameDataParser.ViewModels.SpeedDifference;

namespace F1GameDataParser.Mapping.ViewModelFactories;

public class SpeedDifferenceFactory : ViewModelFactoryBase<SpeedDifference>
{
    private readonly LapState lapState;
    private readonly CarTelemetryState carTelemetryState;
    private readonly SessionState sessionState;

    public SpeedDifferenceFactory(LapState lapState, CarTelemetryState carTelemetryState, SessionState sessionState)
    {
        this.lapState = lapState;
        this.carTelemetryState = carTelemetryState;
        this.sessionState = sessionState;
    }

    public override SpeedDifference? Generate()
    {
        if (lapState?.State == null || carTelemetryState?.State == null || sessionState?.State == null) return null;

        var spectatedVehicleIdx = sessionState.State.SpectatorCarIndex;
        var spectatedPosition = lapState.GetModel(sessionState.State.SpectatorCarIndex)?.CarPosition;

        var followingLapState = lapState.State.FirstOrDefault(l => l.Value.CarPosition == spectatedPosition + 1);
        var followingVehicleIdx = followingLapState.Key;
        var followingPosition = followingLapState.Value.CarPosition;

        var spectatedSpeed = carTelemetryState.State.CarTelemetryDetails.ElementAtOrDefault(spectatedVehicleIdx)?.Speed;
        var followingSpeed = carTelemetryState.State.CarTelemetryDetails.ElementAtOrDefault(followingVehicleIdx)?.Speed;

        if (spectatedPosition == null || spectatedSpeed == null || followingSpeed == null) return null;

        return new SpeedDifference 
        {
            SpectatedVehicleIdx = spectatedVehicleIdx,
            SpectatedSpeed = spectatedSpeed.Value,
            SpectatedPosition = spectatedPosition.Value,
            FollowingVehicleIdx = followingVehicleIdx,
            FollowingSpeedDifference = followingSpeed.Value - spectatedSpeed.Value,
            FollowingPosition = followingPosition,
        };
    }
}
