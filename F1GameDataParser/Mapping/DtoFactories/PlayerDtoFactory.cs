using F1GameDataParser.Database.Entities;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public class PlayerDtoFactory : DtoFactoryBase<Player, PlayerDto>
    {
        public override Player FromDto(PlayerDto dto)
        {
            return new Player
            {
                Name = dto.Name,
                Nationality = dto.Nationality
            };
        }

        public override Expression<Func<Player, PlayerDto>> ToDtoExpression()
        {
            return entity => new PlayerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Nationality = entity.Nationality,
            };
        }
    }
}
