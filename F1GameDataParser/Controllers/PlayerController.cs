using F1GameDataParser.Database.Entities;
using F1GameDataParser.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1GameDataParser.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService playerService;
        public PlayerController(PlayerService playerService) 
        {
            this.playerService = playerService;
        }

        [HttpGet("search")]
        public async Task<List<Player>>? SearchPlayers([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return null;

           return await playerService.SearchPlayers(searchTerm);
        }
    }
}
