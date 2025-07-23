using F1GameDataParser.Models.PersonalBestLap;

namespace F1GameDataParser.State.ComputedStates
{
    public class PersonalBestLapState : ListStateBase<PersonalBestLap>
    {
        protected override int? GetModelKey(PersonalBestLap model) => model.VehicleIdx;

        public PersonalBestLap? GetFastestLap()
        {
            return this.State.Values.MinBy(lap => lap.LapTimeInMS);
        }
    }
}
