using F1GameDataParser.Database.Entities;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public interface IDtoFactory<TEntity, TDto> 
        where TEntity : BaseEntity
        where TDto : class
    {
        Expression<Func<TEntity, TDto>> ToDtoExpression();
        TDto ToDto(TEntity entity);
    }
}
