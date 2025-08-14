using F1GameDataParser.Attributes;
using F1GameDataParser.Enums;
using F1GameDataParser.Models.Grid;
using System.Reflection;

namespace F1GameDataParser.Utility
{
    public static class GridUtility
    {
        public static GridStructure GetStructure<TDto>()
        {
            Type type = typeof(TDto);
            var properties = type.GetProperties();


            var structure = new GridStructure
            {
                Columns = new List<GridColumn>()
            };

            foreach (var property in properties)
            {
                var columnAttribute = property.GetCustomAttribute<GridColumnAttribute>();
                if (columnAttribute == null) 
                    continue;

                structure.Columns.Add(new GridColumn
                {
                    Field = property.Name.ToCamelCase(),
                    Title = columnAttribute.Title,
                    IsUnique = columnAttribute.IsUnique,
                    Type = property.PropertyType.ToGridItemType() ?? columnAttribute.Type
                });
            }

            return structure;
        }

        public static string? ToGridItemType(this Type propertyType)
        {
            if (propertyType == typeof(string))
                return "string";
            else if (propertyType == typeof(int) || propertyType == typeof(long) || propertyType == typeof(float) || propertyType == typeof(double) || propertyType == typeof(decimal))
                return "number";
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                return "date";
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                return "bool";
            else
                return null;
        }
    }
}
