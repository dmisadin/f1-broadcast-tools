namespace F1GameDataParser.Attributes
{
    class GridColumnAttribute : Attribute
    {
        public GridColumnAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
        public bool Hide { get; set; }
        public string MaxWidth { get; set; }
        public bool IsUnique { get; set; } = false;
        public string Type { get; set; }
    }
}
