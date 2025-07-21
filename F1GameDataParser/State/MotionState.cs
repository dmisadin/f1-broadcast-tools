using F1GameDataParser.Models.Motion;

namespace F1GameDataParser.State
{
    public class MotionState : ListStateBase<CarMotionDetails>
    {
        public float MinX { get; private set; } = float.MaxValue;
        public float MaxX { get; private set; } = float.MinValue;
        public float MinZ { get; private set; } = float.MaxValue;
        public float MaxZ { get; private set; } = float.MinValue;

        public override void Update(IEnumerable<CarMotionDetails> newState)
        {
            base.Update(newState);
            AnalyzeTrack(newState);
        }

        /// <summary>
        /// Use this when driving on the track to find bounds on map.
        /// Override Update method and call AnalyzeTrack.
        /// </summary>
        private void AnalyzeTrack(IEnumerable<CarMotionDetails> carMotionData)
        {
            foreach (var car in carMotionData)
            {
                if (car.WorldPositionX < MinX) MinX = car.WorldPositionX;
                if (car.WorldPositionX > MaxX) MaxX = car.WorldPositionX;

                if (car.WorldPositionZ < MinZ) MinZ = car.WorldPositionZ;
                if (car.WorldPositionZ > MaxZ) MaxZ = car.WorldPositionZ;
            }
            Console.WriteLine($"Bounds so far: X({MinX}, {MaxX}), Z({MinZ}, {MaxZ})");
        }
    }
}
