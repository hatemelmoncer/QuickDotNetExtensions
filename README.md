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

---

## EnumerableExtensions

Namespace: `QuickDotNetExtensions`

*(Please refer to the source for full method details. Typical methods include filtering, mapping, and utility operations for `IEnumerable<T>` collections.)*

---

## EnumExtensions

Namespace: `QuickDotNetExtensions`

*(Please refer to the source for full method details. Typical methods include parsing, description retrieval, and utility operations for enum types.)*

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
