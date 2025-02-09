using F1GameDataParser.Enums;
using System.ComponentModel.DataAnnotations;

namespace F1GameDataParser.Database.Entities
{
    public class Player : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public Nationality Nationality { get; set; }
    }
}
