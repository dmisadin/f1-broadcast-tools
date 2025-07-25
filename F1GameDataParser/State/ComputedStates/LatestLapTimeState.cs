using F1GameDataParser.Models.LapTime;

namespace F1GameDataParser.State.ComputedStates
{
    public class LatestLapTimeState : ListStateBase<LapTime>
    {
        public event EventHandler<LapTimeChange>? LapTimeChanged;
        protected override int? GetModelKey(LapTime model) => model.VehicleIdx;

        protected override void OnModelMerged(int key, LapTime existingModel, LapTime newModel)
        {
            var args = new LapTimeChange
            {
                VehicleIdx = (byte)key,
                Sector1Changed = existingModel.Sector1TimeInMS != newModel.Sector1TimeInMS,
                Sector2Changed = existingModel.Sector2TimeInMS != newModel.Sector2TimeInMS,
                Sector3Changed = existingModel.Sector3TimeInMS != newModel.Sector3TimeInMS,
                LapTimeChanged = existingModel.LapTimeInMS != newModel.LapTimeInMS
            };

            if (args.Sector1Changed || args.Sector2Changed || args.Sector3Changed)
                LapTimeChanged?.Invoke(this, args);
        }
    }
}
