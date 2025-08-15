using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities.Widgets;
using F1GameDataParser.Database.Repositories;
using F1GameDataParser.Mapping.DtoFactories;
using Microsoft.AspNetCore.Mvc;

namespace F1GameDataParser.Controllers
{
    [Route("api/widget")]
    [ApiController]
    public class WidgetController : GenericController<Widget, WidgetDto>
    {
        public WidgetController(IRepository<Widget> repository) : base(repository)
        {
        }

        protected override IDtoFactory<Widget, WidgetDto> DtoFactory => new WidgetDtoFactory();
    }
}
