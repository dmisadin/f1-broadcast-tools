using F1GameDataParser.Database.Entities.Widgets;

namespace F1GameDataParser.Database.Entities
{
    public class Overlay : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Widget> Widgets { get; set; } = new List<Widget>();
    }
}
