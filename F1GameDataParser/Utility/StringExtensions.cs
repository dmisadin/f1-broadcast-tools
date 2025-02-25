namespace F1GameDataParser.Utility
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string pascalCase)
        {
            if (string.IsNullOrEmpty(pascalCase) || !char.IsUpper(pascalCase[0]))
            {
                return pascalCase;
            }

            string camelCase = char.ToLower(pascalCase[0]) + pascalCase.Substring(1);

            return camelCase;
        }
    }
}
