using F1GameDataParser.Database.Entities;

namespace F1GameDataParser.Database.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Query();
        Task<TEntity> GetAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task InsertAsync(params TEntity[] entities);
        Task DeleteAsync(params TEntity[] entities);
        Task CommitAsync();
    }
}
