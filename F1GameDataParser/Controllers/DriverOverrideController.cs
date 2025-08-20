using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Services;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/driver-override")]
    [ApiController]
    public class DriverOverrideController : ControllerBase
    {
        private readonly DriverOverrideService driverOverrideService;

        public DriverOverrideController(DriverOverrideService driverOverrideService)
        {
            this.driverOverrideService = driverOverrideService;   
        }

        [HttpGet("get-all")]
        public List<DriverOverrideDto> GetAll()
        {
            return this.driverOverrideService.GetAll();
        }

        [HttpPost("update")]
        public async Task Update([FromBody] List<DriverOverrideDto> driverOverrides)
        {
            await this.driverOverrideService.UpdateOverrides(driverOverrides);
        }
    }
}
