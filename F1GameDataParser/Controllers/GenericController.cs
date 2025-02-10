using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Mapping.DtoFactories;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    public abstract class GenericController<TEntity, TDto> : ControllerBase 
        where TEntity : BaseEntity
        where TDto : class
    {
        private readonly IRepository<TEntity> repository;
        public GenericController(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }
        protected abstract IDtoFactory<TEntity, TDto> DtoFactory { get; }

        [HttpGet("get")]
        public async Task<TEntity> Get([FromQuery]int id)
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
