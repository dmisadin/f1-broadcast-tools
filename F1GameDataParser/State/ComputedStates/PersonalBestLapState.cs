using F1GameDataParser.Enums;
using F1GameDataParser.Models.LapTime;

namespace F1GameDataParser.State.ComputedStates
{
    public class PersonalBestLapState : DictionaryStateBase<LapTime>
    {
        protected override int? GetModelKey(LapTime model) => model.VehicleIdx;

        public LapTime? GetFastestLap()
        {
            return this.State.Values.OrderBy(lap => lap.LapTimeInMS).FirstOrDefault();
        }

        public IDictionary<Sector, ushort?> GetFastestSectors()
        {
            var lapWithFastestS1 = this.State.Values.OrderBy(lap => lap.Sector1TimeInMS).FirstOrDefault();
            var lapWithFastestS2 = this.State.Values.OrderBy(lap => lap.Sector2TimeInMS).FirstOrDefault();
            var lapWithFastestS3 = this.State.Values.OrderBy(lap => lap.Sector3TimeInMS).FirstOrDefault();

            return new Dictionary<Sector, ushort?>
            {
                { Sector.Sector1, lapWithFastestS1?.Sector1TimeInMS },
                { Sector.Sector2, lapWithFastestS2?.Sector2TimeInMS },
                { Sector.Sector3, lapWithFastestS3?.Sector3TimeInMS }
            };
        }
    }
}
