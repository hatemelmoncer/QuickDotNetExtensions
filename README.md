# QuickDotNetExtensions

## Overview
Extension methods for .Net Core </br>
<b>QuickDotNetExtensions</b> is a collection of C# extension methods targeting .NET 6 and later, designed to simplify common operations on strings, dates, enumerables, and enums. The extensions are implemented as static classes and methods, making them easy to use and integrate into any .NET project.

---

## StringExtensions

Namespace: `QuickDotNetExtensions`

### Methods

#### `string Between(this string? source, string firstSequence, string secondSequence)`
Returns the substring between the first occurrence of `firstSequence` and `secondSequence` in `source`.  
- Returns `string.Empty` if either sequence is not found.
- Throws `ArgumentNullException` if any argument is null.

#### `string BetweenOrThrow(this string? source, string firstSequence, string secondSequence)`
Returns the substring between `firstSequence` and `secondSequence`.  
- Throws `StringNotFoundException` if either sequence is not found.
- Throws `ArgumentNullException` if any argument is null.

#### `bool EqualsIgnoreCase(this string? source, string secondString)`
Compares two strings for equality, ignoring case using `InvariantCulture`.  
- Throws `ArgumentNullException` if any argument is null.

#### `string Left(this string? source, int length)`
Returns the leftmost `length` characters of `source`.  
- Returns the whole string if `length` exceeds string length.
- Throws `ArgumentNullException` if `source` is null.

#### `string LeftOrThrow(this string? source, int length)`
Returns the leftmost `length` characters of `source`.  
- Throws `ArgumentException` if `length` exceeds string length.
- Throws `ArgumentNullException` if `source` is null.

#### `string Right(this string? source, int length)`
Returns the rightmost `length` characters of `source`.  
- Returns the whole string if `length` exceeds string length.
- Throws `ArgumentNullException` if `source` is null.

#### `string RightOrThrow(this string? source, int length)`
Returns the rightmost `length` characters of `source`.  
- Throws `ArgumentException` if `length` exceeds string length.
- Throws `ArgumentNullException` if `source` is null.

#### `T? ToEnumOrNull<T>(this string? source) where T : struct`
Converts the string to the corresponding enum value of type `T`, or returns `null` if conversion fails.  
- Throws `ArgumentNullException` if `source` is null.

---

## DateOnlyExtensions

Namespace: `QuickDotNetExtensions`

### Methods

#### `DateTime ToDateTime(this DateOnly dateOnly)`
Converts a `DateOnly` value to a `DateTime` with the default time.

#### `string ToYYYYMMDD(this DateOnly dateOnly)`
Formats the date as a string in `yyyyMMdd` format.

#### `DateOnly Yesterday(this DateOnly dateOnly)`
Returns the previous day.

#### `DateOnly YesterdayAndSkipWeekend(this DateOnly dateOnly)`
Returns the previous weekday, skipping Saturday and Sunday.

#### `DateOnly Tomorrow(this DateOnly dateOnly)`
Returns the next day.

#### `DateOnly TomorrowAndSkipWeekend(this DateOnly dateOnly)`
Returns the next weekday, skipping Saturday and Sunday.

---

## DateTimeExtensions

Namespace: `QuickDotNetExtensions`

### Methods

#### `DateOnly ToDateOnly(this DateTime dateTime)`
Converts a `DateTime` value to a `DateOnly`.

#### `string ToYYYYMMDD(this DateTime dateTime)`
Formats the date as a string in `yyyyMMdd` format.

#### `DateTime Yesterday(this DateTime dateTime)`
Returns the previous day.

#### `DateTime YesterdayAndSkipWeekend(this DateTime dateTime)`
Returns the previous weekday, skipping Saturday and Sunday.

#### `DateTime Tomorrow(this DateTime dateTime)`
Returns the next day.

#### `DateTime TomorrowAndSkipWeekend(this DateTime dateTime)`
Returns the next weekday, skipping Saturday and Sunday.

-- Basic day boundaries --

#### `DateTime StartOfDay(this DateTime dt)`
Returns the start of the day (00:00:00) for the DateTime preserving Kind.

#### `DateTime EndOfDay(this DateTime dt)`
Returns the end of the day (last tick before next day) for the DateTime preserving Kind.

#### `DateTimeOffset StartOfDay(this DateTimeOffset dto)`
Start of day for DateTimeOffset (time component zeroed, offset preserved).

#### `DateTimeOffset EndOfDay(this DateTimeOffset dto)`
End of day for DateTimeOffset (last tick before next day, offset preserved).

-- Month helpers --

#### `DateTime FirstDayOfMonth(this DateTime dt)`
Returns a DateTime set to the first day of the month for the provided date (time-of-day preserved where applicable).

#### `DateTime LastDayOfMonth(this DateTime dt)`
Returns a DateTime set to the last day of the month for the provided date (time-of-day zeroed, Kind preserved).

-- Week helpers --

#### `DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)`
Returns the date that corresponds to the start of the week containing this date. Defaults to Monday as start of week. Preserves Kind and time-of-day.

#### `DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)`
Returns the date that corresponds to the end of the week containing this date (computed from the startOfWeek).

-- Weekday helpers --

#### `bool IsWeekend(this DateTime dt)`
True if the date is Saturday or Sunday.

#### `bool IsWeekday(this DateTime dt)`
True if the date is not Saturday or Sunday.

#### `DateTime Next(this DateTime dt, DayOfWeek desired)`
Returns the next occurrence of the requested DayOfWeek strictly after the given date.

#### `DateTime Previous(this DateTime dt, DayOfWeek desired)`
Returns the previous occurrence of the requested DayOfWeek strictly before the given date.

#### `DateTime NextOrSame(this DateTime dt, DayOfWeek desired)`
Returns the next or same occurrence of desired DayOfWeek (returns same day if dt already matches).

#### `DateTime PreviousOrSame(this DateTime dt, DayOfWeek desired)`
Returns the previous or same occurrence of desired DayOfWeek.

-- Truncate / Round --

#### `DateTime Truncate(this DateTime dt, TimeSpan to)`
Truncates the DateTime to the specified TimeSpan (e.g. minutes, seconds). Preserves Kind.  
- Throws `ArgumentOutOfRangeException` if `to` is not > TimeSpan.Zero.

#### `DateTime Round(this DateTime dt, TimeSpan to, MidpointRounding rounding = MidpointRounding.ToEven)`
Rounds the DateTime to the nearest TimeSpan interval using midpoint rounding.  
- Throws `ArgumentOutOfRangeException` if `to` is not > TimeSpan.Zero.

-- Business day helpers --

#### `DateTime AddBusinessDays(this DateTime dt, int businessDays)`
Adds business days to the given date (skips Saturday and Sunday). Accepts negative values to subtract business days.

#### `int BusinessDaysBetween(this DateTime a, DateTime b, bool inclusive = false)`
Returns the number of business days between two dates. If `inclusive` is true the end date is included. Weekends are excluded. Order of dates does not matter; result is non-negative.

-- Unix time --

#### `long ToUnixTimeSeconds(this DateTime dt)`
Returns Unix time seconds for a DateTime (interpreted via its Kind).

#### `long ToUnixTimeMilliseconds(this DateTime dt)`
Returns Unix time milliseconds for a DateTime.

-- ISO / formatting --

#### `string ToIsoString(this DateTime dt)`
Returns ISO-8601 round-trip string (o format).

#### `string ToIsoString(this DateTimeOffset dto)`
Returns ISO-8601 round-trip string (o format) for DateTimeOffset.

-- Time zone helpers --

#### `DateTime ConvertTimeZone(this DateTime dt, TimeZoneInfo sourceZone, TimeZoneInfo destZone)`
Converts a DateTime from one time zone to another using TimeZoneInfo ids.  
- Throws `ArgumentNullException` if `sourceZone` or `destZone` is null.

#### `DateTime ConvertToTimeZone(this DateTime dt, string timeZoneId)`
Converts a DateTime to the specified TimeZone by id (throws if id not found).  
- Throws `ArgumentNullException` if `timeZoneId` is null.

---

## EnumerableExtensions

Namespace: `QuickDotNetExtensions`

### Methods

#### `bool IsNullOrEmpty<T>(this IEnumerable<T> source)`
Returns `true` when `source` is `null` or contains no elements. Safe helper to avoid null checks before enumeration.

#### `IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageNumber, int pageSize)`
Returns a page (slice) of the sequence using 1-based `pageNumber` and `pageSize`.
- Throws `ArgumentNullException` if `source` is null.
- Throws `ArgumentException` if `pageNumber` is negative or `pageSize` is less than or equal to zero.
- Example: `items.Page(2, 10)` skips first 10 and takes next 10.

#### `IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)`
Batches the source sequence into subsequences of `batchSize`. The last batch may be smaller.
- Throws `ArgumentNullException` if `source` is null.
- Throws `ArgumentException` if `batchSize` is less than or equal to zero.
- Example: `{1,2,3,4,5}.Batch(2) => [ {1,2}, {3,4}, {5} ]`

#### `IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)`
Performs `action` on each element and yields the original element (supports fluent chaining).
- Throws `ArgumentNullException` if `source` or `action` is null.
- Example: `items.ForEach(x => Console.WriteLine(x)).ToList();`

#### `IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)`
Flattens a sequence of sequences into a single sequence. Null inner sequences are treated as empty.
- Throws `ArgumentNullException` if `source` is null.
- Equivalent to `.SelectMany(x => x)` but named for clarity.

#### `IEnumerable<KeyValuePair<int, T>> WithIndex<T>(this IEnumerable<T> source)`
Enumerates elements together with zero-based indices as `KeyValuePair<int, T>`.
- Throws `ArgumentNullException` if `source` is null.
- Example: `foreach(var kv in items.WithIndex()) Console.WriteLine(kv.Key + ": " + kv.Value);`

#### `IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)`
Performs `action` on each element and yields the original element (supports fluent chaining).
- Throws `ArgumentNullException` if `source` or `action` is null.
- Example: `items.ForEach(x => Console.WriteLine(x)).ToList();`

#### `IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)`
Flattens a sequence of sequences into a single sequence. Null inner sequences are treated as empty.
- Throws `ArgumentNullException` if `source` is null.
- Equivalent to `.SelectMany(x => x)` but named for clarity.

#### `IEnumerable<KeyValuePair<int, T>> WithIndex<T>(this IEnumerable<T> source)`
Enumerates elements together with zero-based indices as `KeyValuePair<int, T>`.
- Throws `ArgumentNullException` if `source` is null.
- Example: `foreach(var kv in items.WithIndex()) Console.WriteLine(kv.Key + ": " + kv.Value);`

---

## DecimalExtensions

Namespace: `QuickDotNetExtensions`

### Methods

#### `decimal RoundKeepOneDigit(this decimal value)`
Rounds `value` to 1 decimal digit using `MidpointRounding.AwayFromZero`.

#### `decimal RoundKeepTwoDigits(this decimal value)`
Rounds `value` to 2 decimal digits using `MidpointRounding.AwayFromZero`.

#### `decimal RoundKeepThreeDigits(this decimal value)`
Rounds `value` to 3 decimal digits using `MidpointRounding.AwayFromZero`.

#### `decimal RoundKeepDigits(this decimal value, int digitsToKeep)`
Rounds `value` to `digitsToKeep` digits using `MidpointRounding.AwayFromZero`.

---

## EnumExtensions

Namespace: `QuickDotNetExtensions`

### Methods

#### `string GetDescription(this Enum source)`
Returns the `DescriptionAttribute` value applied to an enum field when present; otherwise returns the enum field name via `ToString()`.
- Throws `ArgumentNullException` if `source` is null.
- Example:

---

## LoggerExtensions

The `LoggerExtensions` class provides a convenient way to automatically log the entry, exit, and elapsed time of a method using `ILogger`. This is especially useful for tracing and performance diagnostics.

### Features

- Logs when a method is entered and exited.
- Logs the elapsed time in milliseconds.
- Supports configurable log level (default is `Information`).
- Simple `using`-based scope.

### Usage

```csharp
using Microsoft.Extensions.Logging;
using QuickDotNetExtensions;

public void MyMethod(ILogger logger)
{
    using var scope = logger.LogMethodScope(nameof(MyMethod), LogLevel.Information);
    // Your method logic here
}
```

**Sample log output:**
```
info: Entering MyMethod
info: Exiting MyMethod (Elapsed: 15ms)
```

### Method

```csharp
IDisposable LogMethodScope(this ILogger logger, string methodName, LogLevel level = LogLevel.Information)
```
- `logger`: The `ILogger` instance.
- `methodName`: The name of the method to log.
- `level`: The log level (optional, default is `Information`).

---

**Note:**  
Requires a reference to `Microsoft.Extensions.Logging.Abstractions` version 7.0.0 or later.

---

## Usage

To use these extensions, simply add the `QuickDotNetExtensions` namespace to your file:

```csharp
using QuickDotNetExtensions;
```

Then call the extension methods on your objects:

```csharp
string result = "Hello [World]!".Between("[", "]");
DateOnly nextBusinessDay = DateOnly.FromDateTime(DateTime.Now).TomorrowAndSkipWeekend();
```

---

## Testing

Unit tests are provided for all extension methods. See the `QuickDotNetExtensions.UnitTests` project for examples and coverage.

---

## Requirements

- .NET 6
- C# 10.0

---

## License

*(Specify your license here if applicable.)*
