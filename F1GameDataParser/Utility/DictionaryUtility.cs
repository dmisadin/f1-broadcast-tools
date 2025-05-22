using System.Reflection;

namespace F1GameDataParser.Utility
{
    public static class DictionaryUtility
    {
        public static Dictionary<int, TModel> FromModelToDictionary<TModel>(IEnumerable<TModel> models)
            where TModel : class
        {
            if (models == null) throw new ArgumentNullException(nameof(models));

            var idProperty = typeof(TModel).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

            if (idProperty == null || idProperty.PropertyType != typeof(int))
                throw new InvalidOperationException($"Type {typeof(TModel).Name} must have a public int 'Id' property.");

            return models.ToDictionary(model => (int)idProperty.GetValue(model), model => model);
        }
    }
}
