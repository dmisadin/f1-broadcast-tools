using F1GameDataParser.Attributes;

namespace F1GameDataParser.Database.Dtos
{
    public class BaseDto
    {
        [GridColumn("Id", IsUnique = true)]
        public int Id { get; set; }
    }
}
