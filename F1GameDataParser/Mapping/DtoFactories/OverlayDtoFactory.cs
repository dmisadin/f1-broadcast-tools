using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public class OverlayDtoFactory : DtoFactoryBase<Overlay, OverlayDto>
    {
        public override Overlay FromDto(OverlayDto dto)
        {
            return new Overlay
            {
                Id = dto.Id,
                Title = dto.Title
            };
        }

        public override Expression<Func<Overlay, OverlayDto>> ToDtoExpression()
        {
            return entity => new OverlayDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Widgets = entity.Widgets.Select(widget => new WidgetDto
                {
                    Id = widget.Id,
                    OverlayId = widget.OverlayId,
                    WidgetType = widget.WidgetType,
                    PositionX = widget.PositionX,
                    PositionY = widget.PositionY
                })
            };
        }
    }
}
