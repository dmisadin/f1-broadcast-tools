using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    public class GenericController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> repository;
        public GenericController(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        [HttpGet("get")]
        public async Task<TEntity> Get(int id)
        {
            return await this.repository.GetAsync(id);
        }

        [HttpPost("add")]
        public async Task Add([FromBody]TEntity entity)
        {
            await this.repository.InsertAsync(entity);
        }
    }
}
