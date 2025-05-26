using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Mapping.DtoFactories;
using F1GameDataParser.Models.Grid;
using F1GameDataParser.Utility;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    public abstract class GenericController<TEntity, TDto> : ControllerBase
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        private readonly IRepository<TEntity> repository;
        public GenericController(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }
        protected abstract IDtoFactory<TEntity, TDto> DtoFactory { get; }

        [HttpGet("get")]
        public async Task<TEntity> Get([FromQuery] int id)
        {
            return await this.repository.GetAsync(id);
        }

        [HttpPost("add")]
        public async Task Add([FromBody] TEntity entity) // TODO: mora doci Dto, pa ga pretvorit u entity
        {
            await this.repository.InsertAsync(entity);
            await this.repository.CommitAsync();
        }

        [HttpGet("get-grid-structure")]
        public GridStructure GetGridStructure()
        {
            return GridUtility.GetStructure<TDto>();
        }

        [HttpPost("get-grid-data")]
        public GridResponse<TDto> GetGridData([FromBody] GridRequest gridRequest)
        {
            var gridResponse = new GridResponse<TDto>
            {
                Records = new List<TDto>()
            };

            var recordsQuery = this.repository.Query().Select(DtoFactory.ToDtoExpression());

            var totalRecords = recordsQuery.Count();

            int page = gridRequest.Page < 1 ? 1 : gridRequest.Page;
            int pageSize = gridRequest.PageSize < 1 ? 10 : gridRequest.PageSize;
            int skip = (page - 1) * pageSize;

            var pagedRecords = recordsQuery
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            gridResponse.TotalRecords = totalRecords;
            gridResponse.Records = pagedRecords;

            return gridResponse;
        }
    }
}
