using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Models.WidgetModels;
using F1GameDataParser.Services;
using F1GameDataParser.State.WidgetStates;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/widget-state")]
    [ApiController]
    public class WidgetStateController : ControllerBase
    {
        private readonly SectorTimingComparisonState sectorTimingComparisonState;
        private readonly SpeedTrapLeaderboardState speedTrapLeaderboardState;
        private readonly TyreStintComparisonState tyreStintComparisonState;
        private readonly DriverDetailService driverDetailService;

        public WidgetStateController(SectorTimingComparisonState sectorTimingComparisonState,
                                    SpeedTrapLeaderboardState speedTrapLeaderboardState,
                                    TyreStintComparisonState tyreStintComparisonState,
                                    DriverDetailService driverDetailService)
        {
            this.sectorTimingComparisonState = sectorTimingComparisonState;
            this.speedTrapLeaderboardState = speedTrapLeaderboardState;
            this.tyreStintComparisonState = tyreStintComparisonState;
            this.driverDetailService = driverDetailService;
        }

        [HttpGet("get-sector-timing-comparison-model")]
        public SectorTimingComparisonModel? GetSectorTimingComparisonModel()
        {
            return this.sectorTimingComparisonState?.State;
        }

        [HttpPost("update-sector-timing-comparison")]
        public void UpdateSectorTimingComparison([FromBody] SectorTimingComparisonModel previousLapSectorComparisonModel)
        {
            this.sectorTimingComparisonState.Update(previousLapSectorComparisonModel);
        }

        [HttpGet("get-speed-trap-leaderboard-model")]
        public SpeedTrapLeaderboardModel? GetSpeedTrapLeaderboardModel()
        {
            return this.speedTrapLeaderboardState?.State;
        }

        [HttpPost("update-speed-trap-leaderboard")]
        public void UpdateSpeedTrapLeaderboard([FromBody] SpeedTrapLeaderboardModel speedTrapLeaderboardModel)
        {
            this.speedTrapLeaderboardState.Update(speedTrapLeaderboardModel);
        }

        [HttpGet("get-tyre-stint-comparison-lookup")]
        public List<LookupDto> GetTyreStintComparisonLookup()
        {
            var selectedVehicles = this.tyreStintComparisonState?.State?.SelectedVehicles;
            if (selectedVehicles == null)
                return new List<LookupDto>();

            return driverDetailService.GetDriverLookupDto(selectedVehicles);
        }

        [HttpPost("update-tyre-stint-comparison")]
        public void UpdateSpeedTrapLeaderboard([FromBody] List<int> selectedVehicles)
        {
            this.tyreStintComparisonState.Update(new TyreStintComparisonModel { SelectedVehicles = selectedVehicles });
        }
    }
}
