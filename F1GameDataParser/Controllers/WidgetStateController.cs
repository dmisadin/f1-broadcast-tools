using F1GameDataParser.Models.WidgetModels;
using F1GameDataParser.State.WidgetStates;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/widget-state")]
    [ApiController]
    public class WidgetStateController : ControllerBase
    {
        private readonly PreviousLapSectorComparisonState previousLapSectorComparisonState;

        public WidgetStateController(PreviousLapSectorComparisonState previousLapSectorComparisonState)
        {
            this.previousLapSectorComparisonState = previousLapSectorComparisonState;
        }

        [HttpGet("get-sector-timing-comparison-model")]
        public PreviousLapSectorComparisonModel? GetSectorTimingComparisonModel()
        {
            return this.previousLapSectorComparisonState?.State;
        }

        [HttpPost("update-sector-timing-comparison")]
        public void UpdateSectorTimingComparison([FromBody] PreviousLapSectorComparisonModel previousLapSectorComparisonModel)
        {
            this.previousLapSectorComparisonState.Update(previousLapSectorComparisonModel);
        }
    }
}
