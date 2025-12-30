using F1GameDataParser.Models.WidgetModels;
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

        public WidgetStateController(SectorTimingComparisonState sectorTimingComparisonState,
                                    SpeedTrapLeaderboardState speedTrapLeaderboardState)
        {
            this.sectorTimingComparisonState = sectorTimingComparisonState;
            this.speedTrapLeaderboardState = speedTrapLeaderboardState;
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
    }
}
