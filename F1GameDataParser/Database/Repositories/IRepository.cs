using F1GameDataParser.Database.Entities;
using System.Linq.Expressions;

namespace F1GameDataParser.Database.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Query();
        Task<TEntity?> GetAsync(int id);
        Task<TDto?> GetAsync<TDto>(int id, Expression<Func<TEntity, TDto>> dtoExpression);
        Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> dtoExpression);
        Task InsertAsync(params TEntity[] entities);
        void Update(TEntity entity);
        void DeleteAsync(params TEntity[] entities);
        Task CommitAsync();
    }
}
