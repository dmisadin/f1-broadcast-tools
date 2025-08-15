using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Database.Entities.Widgets;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.DtoFactories
{
    public class WidgetDtoFactory : DtoFactoryBase<Widget, WidgetDto>
    {
        public override Widget FromDto(WidgetDto dto)
        {
            return new Widget
            {
                Id = dto.Id,
                OverlayId = dto.OverlayId,
                WidgetType = dto.WidgetType,
                PositionX = dto.PositionX,
                PositionY = dto.PositionY
            };
        }

        public override Expression<Func<Widget, WidgetDto>> ToDtoExpression()
        {
            return entity => new WidgetDto
            {
                Id = entity.Id,
                OverlayId = entity.OverlayId,
                WidgetType = entity.WidgetType,
                PositionX = entity.PositionX,
                PositionY = entity.PositionY
            };
        }
    }
}
