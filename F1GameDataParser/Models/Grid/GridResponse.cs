using F1GameDataParser.Database.Dtos;

namespace F1GameDataParser.Models.Grid
{
    public class GridResponse<TDto> 
        where TDto : BaseDto
    {
        public IList<TDto> Records { get; set; } = new List<TDto>();
        public int TotalRecords { get; set; }
    }
}
