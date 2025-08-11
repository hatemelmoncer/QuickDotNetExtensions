namespace QuickDotNetExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns sequence of string between the firstSequence and the secondSequence
        /// </summary>
        /// <param name="source"></param>
        /// <param name="firstSequence"></param>
        /// <param name="secondSequence"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Between(this string? source, string firstSequence, string secondSequence)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (firstSequence == null)
                throw new ArgumentNullException(nameof(firstSequence));
            if (secondSequence == null)
                throw new ArgumentNullException(nameof(secondSequence));

            int firstSequencePosition = source.IndexOf(firstSequence);
            int secondSequencePosition = source.IndexOf(secondSequence);
            if (firstSequencePosition == -1 || secondSequencePosition == -1)
                return string.Empty;

            int firstPosition = firstSequencePosition + firstSequence.Length;
            int secondPosition = secondSequencePosition;
            
            string finalString = source.Substring(firstPosition, secondPosition - firstPosition);
            return finalString;
        }

        /// <summary>
        /// Returns sequence of string between the firstSequence and the secondSequence
        /// Throw StringNotFoundException if one of the sequence is not found
        /// </summary>
        /// <param name="source"></param>
        /// <param name="firstSequence"></param>
        /// <param name="secondSequence"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="StringNotFoundException"></exception>
        public static string BetweenOrThrow(this string? source, string firstSequence, string secondSequence)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (firstSequence == null)
                throw new ArgumentNullException(nameof(firstSequence));
            if (secondSequence == null)
                throw new ArgumentNullException(nameof(secondSequence));

            int firstSequencePosition = source.IndexOf(firstSequence);
            int secondSequencePosition = source.IndexOf(secondSequence);
            if (firstSequencePosition == -1)
                throw new StringNotFoundException($"The sequence '{firstSequence}' is not found is the source string.");

            if (secondSequencePosition == -1)
                throw new StringNotFoundException($"The sequence '{secondSequence}' is not found is the source string.");

            return Between(source, firstSequence, secondSequence);
        }

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
        /// Returns the sequence in the left side of the string before reaching the indicated length
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Left(this string? source, int length)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (length <= source.Length)
                return source.Substring(0, length);

            return source;
        }

        /// <summary>
        /// Returns the sequence in the left side of the string before reaching the indicated length
        /// Throws exception if the indicated lenght is greater than the string length
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string LeftOrThrow(this string? source, int length)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (length > source.Length)
                throw new ArgumentException("Length is greater than the string length.");

            return Left(source, length);
        }

        /// <summary>
        /// Returns the sequence in the right side of the string starting from the indicated length.
        /// Returns the same string if the indicated lenght is greater than the string length
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Right(this string? source, int length)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (length > source.Length)
                return source;

            return source.Substring(source.Length - length);
        }

        /// <summary>
        /// Returns the sequence in the right side of the string starting from the indicated length.
        /// Throws exception if the indicated lenght is greater than the string length
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string RightOrThrow(this string? source, int length)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (length > source.Length)
                throw new ArgumentException("Length is greater than the string length.");

            return source.Substring(source.Length - length);
        }

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
    }
}