using F1GameDataParser.Attributes;

namespace F1GameDataParser.Database.Dtos
{
    public class OverlayDto : BaseDto
    {
        [GridColumn("Title")]
        public string Title { get; set; }
        public IEnumerable<WidgetDto> Widgets { get; set; } = new List<WidgetDto>();
    }
}
