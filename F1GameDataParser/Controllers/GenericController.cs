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
        public async Task<TDto?> Get([FromQuery] int id)
        {
            return await repository.GetAsync(id, DtoFactory.ToDtoExpression());
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] TDto dto)
        {
            var entity = DtoFactory.FromDto(dto);
            await this.repository.InsertAsync(entity);
            await this.repository.CommitAsync();

            return entity.Id;
        }

        [HttpPost("add-many")]
        public async Task<ActionResult<List<int>>> AddMany([FromBody] List<TDto> dtos)
        {
            var entities = dtos.Select(DtoFactory.FromDto).ToArray();
            await this.repository.InsertAsync(entities);
            await this.repository.CommitAsync();

            var ids = entities.Select(e => e.Id).ToList();
            return ids;
        }

        [HttpPost("update")]
        public async Task<ActionResult<int>> Update([FromBody] TDto dto)
        {
            var entity = DtoFactory.FromDto(dto);

            this.repository.Update(entity);
            await this.repository.CommitAsync();

            return entity.Id;
        }

        [HttpPost("update-many")]
        public async Task<ActionResult<List<int>>> UpdateMany([FromBody] List<TDto> dtos)
        {
            var entities = dtos.Select(DtoFactory.FromDto).ToArray();

            foreach (var entity in entities)
            {
                this.repository.Update(entity);
            }

            await this.repository.CommitAsync();

            var ids = entities.Select(e => e.Id).ToList();
            return ids;
        }

        [HttpPost("upsert")]
        public async Task<ActionResult<int>> Upsert([FromBody] TDto dto)
        {
            var entity = DtoFactory.FromDto(dto);
            if (dto.Id == 0)
            {
                await this.repository.InsertAsync(entity);
            }
            else
            {
                this.repository.Update(entity);
            }

            await this.repository.CommitAsync();

            return entity.Id;
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
