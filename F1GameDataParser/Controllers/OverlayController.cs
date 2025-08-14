using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Mapping.DtoFactories;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/overlay")]
    [ApiController]
    public class OverlayController : GenericController<Overlay, OverlayDto>
    {
        public OverlayController(IRepository<Overlay> repository) : base(repository)
        {
        }

        protected override IDtoFactory<Overlay, OverlayDto> DtoFactory => new OverlayDtoFactory();
    }
}
