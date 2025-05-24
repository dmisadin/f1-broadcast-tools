using F1GameDataParser.Attributes;
using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Enums;

namespace F1GameDataParser.Database.Entities
{
    public class PlayerDto : BaseDto
    {
        [GridColumn("Name")]
        public string Name { get; set; }

        [GridColumn("Nationality", Type="string")]
        public Nationality? Nationality { get; set; }
    }
}
