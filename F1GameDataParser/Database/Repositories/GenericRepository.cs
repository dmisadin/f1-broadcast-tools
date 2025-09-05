using F1GameDataParser.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace F1GameDataParser.Database.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<TEntity> table;
        public GenericRepository(AppDbContext context) 
        {
            this.context = context;
            this.table = this.context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return this.table;
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await this.table.FindAsync(id);
        }

        public async Task<TDto?> GetAsync<TDto>(int id, Expression<Func<TEntity, TDto>> dtoExpression)
        {
            return await Query().Where(e => e.Id == id).Select(dtoExpression).FirstOrDefaultAsync();
        }

        public async Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, TDto>> dtoExpression)
        {
            return await Query().Select(dtoExpression).ToListAsync(); ;
        }

        public async Task InsertAsync(params TEntity[] entities)
        {
            await this.table.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            this.table.Update(entity);
        }

        public void DeleteAsync(params TEntity[] entities)
        {
            this.table.RemoveRange(entities);
        }

        public async Task CommitAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
