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
        public async Task<List<PlayerDto>>? SearchPlayers([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return null;
            return null;
           //return await playerService.SearchPlayers(searchTerm);
        }
    }
}
