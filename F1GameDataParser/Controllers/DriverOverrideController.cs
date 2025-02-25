using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/driver-override")]
    [ApiController]
    public class DriverOverrideController : ControllerBase
    {
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {


            return NotFound();
        }
    }
}
