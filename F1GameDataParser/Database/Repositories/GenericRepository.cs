using F1GameDataParser.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<TEntity> GetAsync(int id)
        {
            return await this.table.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await this.table.ToListAsync();
        }

        public async Task InsertAsync(params TEntity[] entities)
        {
            await this.table.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(params TEntity[] entities)
        {
            this.table.RemoveRange(entities);
        }

        public async Task CommitAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
