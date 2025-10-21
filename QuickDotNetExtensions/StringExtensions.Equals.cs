namespace QuickDotNetExtensions;

public partial class StringExtensions
{
    [Obsolete($"Use {nameof(EqualsIgnoreCaseInvariant)} instead.")]
    /// <summary>
    /// Compares to a string and ignore case (case insensitive).
    /// Culture is InvariantCulture
    /// </summary>
    /// <param name="source"></param>
    /// <param name="secondString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool EqualsIgnoreCase(this string? source, string secondString)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (secondString == null)
            throw new ArgumentNullException(nameof(secondString));

        return string.Equals(source, secondString, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Compares to a string and ignore case (case insensitive).
    /// Culture is InvariantCulture
    /// </summary>
    /// <param name="source"></param>
    /// <param name="secondString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool EqualsIgnoreCaseInvariant(this string? source, string secondString)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (secondString == null)
            throw new ArgumentNullException(nameof(secondString));

        return string.Equals(source, secondString, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Compares to a string and ignore case (case insensitive).
    /// OrdinalIgnoreCase is used. Does not take culture into account.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="secondString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool EqualsIgnoreCaseOrdinal(this string? source, string secondString)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (secondString == null)
            throw new ArgumentNullException(nameof(secondString));

        return string.Equals(source, secondString, StringComparison.OrdinalIgnoreCase);
    }
}
