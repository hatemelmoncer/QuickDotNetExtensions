using System.Text.RegularExpressions;

namespace QuickDotNetExtensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Ensures the string starts with the specified prefix. If it already does (respecting comparison), returns original string.
	/// </summary>
	public static string EnsureStartsWith(this string? source, string prefix, StringComparison comparisonType = StringComparison.Ordinal)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));
		if (prefix == null)
			throw new ArgumentNullException(nameof(prefix));

		return source.StartsWith(prefix, comparisonType) ? source : prefix + source;
	}

	/// <summary>
	/// Ensures the string ends with the specified suffix. If it already does (respecting comparison), returns original string.
	/// </summary>
	public static string EnsureEndsWith(this string? source, string suffix, StringComparison comparisonType = StringComparison.Ordinal)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));
		if (suffix == null)
			throw new ArgumentNullException(nameof(suffix));

		return source.EndsWith(suffix, comparisonType) ? source : source + suffix;
	}

	/// <summary>
	/// Removes the specified prefix from the start if present (respecting comparison). Otherwise returns the original string.
	/// </summary>
	public static string RemovePrefix(this string? source, string prefix, StringComparison comparisonType = StringComparison.Ordinal)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));
		if (prefix == null)
			throw new ArgumentNullException(nameof(prefix));

		return source.StartsWith(prefix, comparisonType) ? source.Substring(prefix.Length) : source;
	}

	/// <summary>
	/// Removes the specified suffix from the end if present (respecting comparison). Otherwise returns the original string.
	/// </summary>
	public static string RemoveSuffix(this string? source, string suffix, StringComparison comparisonType = StringComparison.Ordinal)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));
		if (suffix == null)
			throw new ArgumentNullException(nameof(suffix));

		return source.EndsWith(suffix, comparisonType) ? source.Substring(0, source.Length - suffix.Length) : source;
	}

	/// <summary>
	/// Normalizes line endings to the provided newline (default: Environment.NewLine).
	/// Handles CRLF, CR, LF.
	/// </summary>
	public static string NormalizeLineEndings(this string? source, string? newLine = null)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));
		if (newLine == null)
			newLine = Environment.NewLine;

		return Regex.Replace(source, @"\r\n|\r|\n", newLine);
	}

	/// <summary>
	/// Splits a string into lines using CRLF, CR or LF as separators.
	/// </summary>
	public static string[] SplitLines(this string? source)
	{
		if (source == null)
			throw new ArgumentNullException(nameof(source));

		return Regex.Split(source, @"\r\n|\r|\n");
	}
}
