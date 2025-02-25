namespace F1GameDataParser.Models.Grid
{
    public class GridStructure
    {
        public IList<GridColumn> Columns { get; set; }
    }

    public class GridColumn
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public bool IsUnique { get; set; }
        public string Type { get; set; }
    }
}
