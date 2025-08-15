using F1GameDataParser.Database.Entities;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public abstract class DtoFactoryBase<TEntity, TDto> : IDtoFactory<TEntity, TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        public abstract Expression<Func<TEntity, TDto>> ToDtoExpression();
        public abstract TEntity FromDto(TDto dto);

        public virtual TDto ToDto(TEntity entity)
        {
            var expression = ToDtoExpression();
            return expression.Compile().Invoke(entity);
        }
    }
}
