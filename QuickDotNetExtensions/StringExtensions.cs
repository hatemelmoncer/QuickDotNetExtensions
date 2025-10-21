using System.Globalization;

namespace QuickDotNetExtensions;

public static partial class StringExtensions
{
    /// <summary>
    /// Convert string value to the equivalent Enum field otherwise return null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T? ToEnumOrNull<T>(this string? source) where T : struct
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (Enum.TryParse(typeof(T), source, out object? result))
            return (T?)result;

        return null;
    }

    /// <summary>
    /// Convenience method for string.Format using the current culture.
    /// Usage: "Hello {0}".FormatWith("world")
    /// </summary>
    public static string FormatWith(this string? format, params object[] args)
    {
        if (format == null)
            throw new ArgumentNullException(nameof(format));

        return string.Format(CultureInfo.CurrentCulture, format, args);
    }
}