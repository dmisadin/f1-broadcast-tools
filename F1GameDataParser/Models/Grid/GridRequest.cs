namespace F1GameDataParser.Models.Grid
{
    public class GridRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IList<GridFilter> Filters { get; set; } = new List<GridFilter>();
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string Search { get; set; }
    }

    public class GridFilter
    {
        public string FilterType { get; set; }
        public string Property { get; set; }
        public object? Value { get; set; }
    }
}
