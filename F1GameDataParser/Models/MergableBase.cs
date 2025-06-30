using System.Reflection;

namespace F1GameDataParser.Models
{
    public abstract class MergeableBase<TModel> : IMergeable<TModel>
        where TModel : class
    {
        public virtual void MergeFrom(TModel source)
        {
            var type = typeof(TModel);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var value = prop.GetValue(source);
                prop.SetValue(this, value);
            }
        }
    }
}
