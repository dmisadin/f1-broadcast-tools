using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F1Common.Constants;
using F1GameDataParser.GameProfiles.F1Common.Utility;
using F1GameDataParser.State;
using F1GameDataParser.ViewModels.Minimap;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class MinimapFactory : ViewModelFactoryBase<Minimap>
    {
        private TrackDetails? trackDetails;

        private readonly MotionState _motionState;
        private readonly SessionState _sessionState;
        private readonly ParticipantsState _participantsState;
        private readonly LapState _lapState;

        public MinimapFactory(MotionState motionState, 
                            SessionState sessionState, 
                            ParticipantsState participantsState, 
                            LapState lapState)
        {
            _motionState = motionState;
            _sessionState = sessionState;
            _participantsState = participantsState;
            _lapState = lapState;
        }

        public override Minimap Generate()
        {
            SetTrackDetails();
            if (_motionState == null || this.trackDetails == null) return null;

            var cars = _motionState.State
                        .Where(m => _lapState.State.TryGetValue(m.Key, out var car) 
                                    && (car.ResultStatus == ResultStatus.Active || car.ResultStatus == ResultStatus.Finished))
                        .Select(car => new MinimapCar
                        {
                            X = Normalize(car.Value.WorldPositionX, trackDetails.minX, trackDetails.maxX) * 100f,
                            Y = Normalize(car.Value.WorldPositionZ, trackDetails.minZ, trackDetails.maxZ) * 100f,
                            Rotation = car.Value.Yaw * (180f / MathF.PI),
                            VehicleIdx = car.Key,
                            TeamColor = GetTeamColor(car.Key)
                        })
                        .ToList();

            return new Minimap { TrackId = trackDetails.Id, Cars = cars, SpectatorCarIdx = 255, Rotation = trackDetails.rotation };
        }

        private float Normalize(float value, float min, float max) => (value - min) / (max - min);

        private void SetTrackDetails()
        {
            if (_sessionState.State == null || _sessionState.State.TrackId == trackDetails?.Id) return;

            if (Tracks.AllTracks.TryGetValue(_sessionState.State.TrackId, out var track))
                this.trackDetails = track;
        }

        private string GetTeamColor(int vehicleIdx)
        {
            if (_participantsState.State == null) return "white";

            var teamId = _participantsState.State.ParticipantList.ElementAtOrDefault(vehicleIdx)?.TeamId;

            if (teamId == null) return "white";

            var gameYear = _participantsState.State.Header.GameYear;

            return GameSpecifics.GetTeamDetails(gameYear, teamId ?? Team.Undefined)?.PrimaryColor ?? "white";
        }
    }
}
