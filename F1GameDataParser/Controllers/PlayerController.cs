using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Mapping.DtoFactories;
using F1GameDataParser.Services;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : GenericController<Player, PlayerDto>
    {
        private readonly PlayerService playerService;
        public PlayerController(IRepository<Player> playerRepository,
                                PlayerService playerService)
            :base(playerRepository)
        {
            this.playerService = playerService;
        }

        protected override IDtoFactory<Player, PlayerDto> DtoFactory => new PlayerDtoFactory();

        [HttpGet("search")]
        public async Task<List<LookupDto>> SearchPlayers([FromQuery] string query, [FromQuery] string field = "name", [FromQuery] int limit = 20)
        {
            if (string.IsNullOrEmpty(query))
                return new List<LookupDto>();

            return await playerService.SearchPlayers(query, limit);
        }
    }
}
