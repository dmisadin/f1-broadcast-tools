using F1GameDataParser.Database.Entities;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public class PlayerDtoFactory : DtoFactoryBase<Player, PlayerDto>
    {
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
