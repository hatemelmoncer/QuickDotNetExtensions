using System.ComponentModel;
using System.Reflection;

namespace QuickDotNetExtensions;

public static class EnumExtensions
{
    /// <summary>
    /// Returns the Description atttribute value if exists, otherwise apply ToString() to the enum field
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetDescription(this Enum source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        FieldInfo fieldInfo = source.GetType().GetField(source.ToString())!;
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return source.ToString();
    }
}
