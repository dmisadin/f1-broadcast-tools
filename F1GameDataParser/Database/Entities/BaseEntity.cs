using System.ComponentModel.DataAnnotations;

namespace F1GameDataParser.Database.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
