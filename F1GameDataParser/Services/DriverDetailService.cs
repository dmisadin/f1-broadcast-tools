using F1GameDataParser.GameProfiles.F1Common.Utility;
using F1GameDataParser.State;
using F1GameDataParser.ViewModels;

namespace F1GameDataParser.Services
{
    public class DriverDetailService
    {
        private readonly ParticipantsState participantsState;
        private readonly DriverOverrideState driverOverrideState;

        public DriverDetailService(ParticipantsState participantsState,
                                   DriverOverrideState driverOverrideState)
        {
            this.participantsState = participantsState;
            this.driverOverrideState = driverOverrideState;
        }

        public IEnumerable<DriverBasicDetails> GetDriverBasicDetails() 
        {
            if (participantsState?.State == null) 
                return Enumerable.Empty<DriverBasicDetails>();

            var gameYear = participantsState.State.Header.GameYear;
            var participants = participantsState.State.ParticipantList;

            return participants.Select((driver, index) => new DriverBasicDetails 
                                {
                                    VehicleIdx = index,
                                    TeamId = driver.TeamId,
                                    TeamDetails = GameSpecifics.GetTeamDetails(gameYear, driver.TeamId),
                                    Name = driverOverrideState.GetModel(index)?.Player?.Name ?? driver.Name ?? $"Driver #{driver.RaceNumber}",
                                });
        }
    }
}
