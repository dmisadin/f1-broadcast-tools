using F1GameDataParser.Enums;
using F1GameDataParser.Models.ComputedModels;

namespace F1GameDataParser.State.ComputedStates
{
    public class PersonalBestLapState : DictionaryStateBase<PersonalBestLap>
    {
        protected override int? GetModelKey(PersonalBestLap model) => model.VehicleIdx;

        public PersonalBestLap? GetFastestLap()
        {
            return this.State.Values.OrderBy(lap => lap.LapTimeInMS).FirstOrDefault();
        }

        public PersonalBestLap? GetSecondFastestLap()
        {
            var topTwoFastestDrivers = this.State.Values.OrderBy(lap => lap.LapTimeInMS).Take(2);
            var fastestDriver = topTwoFastestDrivers?.FirstOrDefault();
            var secondFastestDriver = topTwoFastestDrivers?.Skip(1).FirstOrDefault();

            if (secondFastestDriver == null) 
                return fastestDriver;

            if (fastestDriver?.PreviousBestLap?.LapTimeInMS < secondFastestDriver.LapTimeInMS)
                return fastestDriver.PreviousBestLap;

            return secondFastestDriver;
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
