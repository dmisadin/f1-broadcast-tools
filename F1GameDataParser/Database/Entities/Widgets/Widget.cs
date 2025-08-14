namespace F1GameDataParser.Database.Entities.Widgets
{
    public class Widget : BaseEntity
    {
        public int OverlayId { get; set; }
        public Overlay Overlay { get; set; }

        public WidgetType WidgetType { get; set; }
        public int PositionX { get; set; } = 0;
        public int PositionY { get; set; } = 0;
    }
}
