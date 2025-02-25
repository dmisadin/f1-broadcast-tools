using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/player-override")]
    [ApiController]
    public class PlayerOverrideController : ControllerBase
    {
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {


            return NotFound();
        }
    }
}
