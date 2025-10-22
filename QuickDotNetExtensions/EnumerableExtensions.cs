namespace QuickDotNetExtensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }

    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (pageNumber < 0)
            throw new ArgumentException("Page size cannot be negative.", nameof(pageNumber));

        if (pageSize <= 0)
            throw new ArgumentException("Page size cannot be less than or equal to zero.", nameof(pageSize));

        int skipCount = (pageNumber - 1) * pageSize;
        return source.Skip(skipCount).Take(pageSize);
    }

    /// <summary>
    /// Batches the source sequence into lists of the specified size.
    /// The last batch may be smaller.
    /// Example: {1,2,3,4,5}.Batch(2) => [ {1,2}, {3,4}, {5} ]
    /// </summary>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (batchSize <= 0)
            throw new ArgumentException("Batch size cannot be less than or equal to zero.", nameof(batchSize));

        return source.Select((item, inx) => new { item, inx })
                     .GroupBy(x => x.inx / batchSize)
                     .Select(g => g.Select(x => x.item));
    }

    /// <summary>
    /// Performs the specified action on each element in the sequence.
    /// Returns the original sequence to allow fluent chaining when desired.
    /// </summary>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (action == null) throw new ArgumentNullException(nameof(action));
        
        foreach (var item in source)
        {
            action(item);
            yield return item;
        }
    }

    /// <summary>
    /// Flattens a sequence of sequences into a single sequence.
    /// Equivalent to SelectMany(x => x) but useful as a named operation.
    /// </summary>
    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.SelectMany(x => x ?? Enumerable.Empty<T>());
    }

    /// <summary>
    /// Enumerates the sequence together with zero-based indices (as KeyValuePair{int, T}).
    /// </summary>
    public static IEnumerable<KeyValuePair<int, T>> WithIndex<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        int i = 0;
        foreach (var item in source)
        {
            yield return new KeyValuePair<int, T>(i++, item);
        }
    }
}
