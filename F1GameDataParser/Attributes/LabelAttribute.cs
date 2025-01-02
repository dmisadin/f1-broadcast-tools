namespace F1GameDataParser.Attributes
{
    public class LabelAttribute : Attribute
    {
        public string Label { get; }
        public string ShortLabel { get; }
        public LabelAttribute(string label, string shortLabel = "")
        {
            this.Label = label;
            this.ShortLabel = shortLabel;
        }
    }
}
