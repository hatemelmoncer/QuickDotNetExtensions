using System.Text;

namespace QuickDotNetExtensions;

public static partial class StringExtensions
{
    /// <summary>
    /// Converts the string to Base64 using the provided encoding (default UTF8).
    /// </summary>
    public static string ToBase64(this string? source, Encoding? encoding = null)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        encoding = encoding ?? Encoding.UTF8;

        var bytes = encoding.GetBytes(source);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes a Base64 string to text using the provided encoding (default UTF8).
    /// </summary>
    public static string FromBase64(this string? base64, Encoding? encoding = null)
    {
        if (base64 == null)
            throw new ArgumentNullException(nameof(base64));
        encoding = encoding ?? Encoding.UTF8;

        var bytes = Convert.FromBase64String(base64);
        return encoding.GetString(bytes);
    }
}
