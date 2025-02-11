using F1GameDataParser.Database.Dtos;
using F1GameDataParser.Enums;

namespace F1GameDataParser.Database.Entities
{
    public class PlayerDto : BaseDto
    {
        public string Name { get; set; }
        public Nationality? Nationality { get; set; }
    }
}
