using F1GameDataParser.Database.Entities.Widgets;

namespace F1GameDataParser.Database.Dtos
{
    public class WidgetDto : BaseDto
    {
        public int OverlayId { get; set; }
        public WidgetType WidgetType { get; set; }
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;
    }
}
