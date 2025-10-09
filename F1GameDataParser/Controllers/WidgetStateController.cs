using F1GameDataParser.Models.WidgetModels;
using F1GameDataParser.State.WidgetStates;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/widget-state")]
    [ApiController]
    public class WidgetStateController : ControllerBase
    {
        private readonly SectorTimingComparisonState sectorTimingComparisonFactory;

        public WidgetStateController(SectorTimingComparisonState sectorTimingComparisonFactory)
        {
            this.sectorTimingComparisonFactory = sectorTimingComparisonFactory;
        }

        [HttpGet("get-sector-timing-comparison-model")]
        public SectorTimingComparisonModel? GetSectorTimingComparisonModel()
        {
            return this.sectorTimingComparisonFactory?.State;
        }

        [HttpPost("update-sector-timing-comparison")]
        public void UpdateSectorTimingComparison([FromBody] SectorTimingComparisonModel previousLapSectorComparisonModel)
        {
            this.sectorTimingComparisonFactory.Update(previousLapSectorComparisonModel);
        }
    }
}
