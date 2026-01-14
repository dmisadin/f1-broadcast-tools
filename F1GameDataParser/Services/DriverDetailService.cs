using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Enums;
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

            return participants.Where(p => p.TeamId != Team.Undefined)
                               .Select((driver, index) => new DriverBasicDetails 
                                {
                                    VehicleIdx = index,
                                    TeamId = driver.TeamId,
                                    TeamDetails = GameSpecifics.GetTeamDetails(gameYear, driver.TeamId),
                                    Name = driverOverrideState.GetModel(index)?.Player?.Name ?? driver.Name ?? $"Driver #{driver.RaceNumber}",
                                });
        }

        public List<LookupDto> GetDriverLookupDto(IEnumerable<int> vehicleIdxs)
        {
            return vehicleIdxs.Select(v => new LookupDto
            {
                Id = v,
                Label = GetDriverName(v),
            }).ToList();
        }

        public string GetDriverName(int vehicleIdx)
        {
            var driverName = driverOverrideState.GetModel(vehicleIdx)?.Player.Name;
            if (driverName != null)
                return driverName;

            var participant = participantsState?.State?.ParticipantList?.ElementAtOrDefault(vehicleIdx);

            return participant?.Name ?? $"Driver #{participant?.RaceNumber}";
        }
    }
}
