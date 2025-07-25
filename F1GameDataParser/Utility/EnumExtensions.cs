using F1GameDataParser.Attributes;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetLabel(this Enum value)
    {
        Type type = value.GetType();

        FieldInfo? fieldInfo = type.GetField(value.ToString());

        if (fieldInfo != null)
        {
            LabelAttribute? attribute = fieldInfo.GetCustomAttribute<LabelAttribute>();

            if (attribute != null)
            {
                return attribute.Label;
            }
        }

        return value.ToString();
    }

    public static string GetShortLabel(this Enum value)
    {
        Type type = value.GetType();

        FieldInfo? fieldInfo = type.GetField(value.ToString());

        if (fieldInfo != null)
        {
            LabelAttribute? attribute = fieldInfo.GetCustomAttribute<LabelAttribute>();

            if (attribute != null && !string.IsNullOrEmpty(attribute.ShortLabel))
            {
                return attribute.ShortLabel;
            }
        }

        return value.ToString();
    }

    public static bool ToBool<TEnum>(this TEnum value) where TEnum : Enum
    {
        return Convert.ToInt32(value) != 0;
    }
}
