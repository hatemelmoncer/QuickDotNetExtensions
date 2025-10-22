namespace QuickDotNetExtensions;

public static class DateTimeExtensions
{
    public static DateOnly ToDateOnly(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

    public static TimeOnly ToTimeOnly(this DateTime dt) => TimeOnly.FromDateTime(dt);

    public static string ToYYYYMMDD(this DateTime dateTime) => dateTime.ToString("yyyyMMdd");

    // Days helpers -------------------------------------------------------

    public static DateTime Yesterday(this DateTime dateTime) => dateTime.AddDays(-1);

    public static DateTime YesterdayAndSkipWeekend(this DateTime dateTime)
    {
        var yesterday = dateTime.AddDays(-1);

        if (yesterday.DayOfWeek == DayOfWeek.Saturday)
            return yesterday.Yesterday();

        if (yesterday.DayOfWeek == DayOfWeek.Sunday)
            return yesterday.Yesterday().Yesterday();

        return yesterday;
    }

    public static DateTime Tomorrow(this DateTime dateTime) => dateTime.AddDays(1);

    public static DateTime TomorrowAndSkipWeekend(this DateTime dateTime)
    {
        var tomorrow = dateTime.AddDays(1);

        if (tomorrow.DayOfWeek == DayOfWeek.Saturday)
            return tomorrow.Tomorrow().Tomorrow();

        if (tomorrow.DayOfWeek == DayOfWeek.Sunday)
            return tomorrow.Tomorrow();

        return tomorrow;
    }

    
    // Basic day boundaries ------------------------------------------------

    /// <summary>
    /// Returns the start of the day (00:00:00) for the DateTime preserving Kind.
    /// </summary>
    public static DateTime StartOfDay(this DateTime dt) => dt.Date;

    /// <summary>
    /// Returns the end of the day (last tick before next day) for the DateTime preserving Kind.
    /// </summary>
    public static DateTime EndOfDay(this DateTime dt) => dt.Date.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Start of day for DateTimeOffset (time component zeroed, offset preserved).
    /// </summary>
    public static DateTimeOffset StartOfDay(this DateTimeOffset dto) => dto.Date;

    /// <summary>
    /// End of day for DateTimeOffset (last tick before next day, offset preserved).
    /// </summary>
    public static DateTimeOffset EndOfDay(this DateTimeOffset dto) => dto.Date.AddDays(1).AddTicks(-1);


    // Month helpers -------------------------------------------------------

    /// <summary>
    /// Returns a DateTime set to the first day of the month for the provided date (same time-of-day and Kind).
    /// </summary>
    public static DateTime FirstDayOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.Kind).Date;

    /// <summary>
    /// Returns a DateTime set to the last day of the month for the provided date (time-of-day zeroed, Kind preserved).
    /// </summary>
    public static DateTime LastDayOfMonth(this DateTime dt)
    {
        int days = DateTime.DaysInMonth(dt.Year, dt.Month);
        return new DateTime(dt.Year, dt.Month, days, 0, 0, 0, dt.Kind);
    }

    // Week helpers --------------------------------------------------------

    /// <summary>
    /// Returns the date that corresponds to the start of the week containing this date.
    /// Defaults to Monday as start of week but you can provide another DayOfWeek.
    /// Returned DateTime preserves Kind and has time component preserved from original.
    /// </summary>
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.Date.AddDays(-diff).Add(dt.TimeOfDay);
    }

    /// <summary>
    /// Returns the date that corresponds to the end of the week containing this date.
    /// Defaults to Monday start-of-week; end is computed accordingly.
    /// </summary>
    public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        var start = dt.StartOfWeek(startOfWeek).Date;
        return start.AddDays(7).AddTicks(-1).Add(dt.TimeOfDay) - dt.TimeOfDay;
    }


    // Weekday helpers -----------------------------------------------------

    /// <summary>
    /// True if the date is Saturday or Sunday.
    /// </summary>
    public static bool IsWeekend(this DateTime dt) =>
        dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// True if the date is not Saturday or Sunday.
    /// </summary>
    public static bool IsWeekday(this DateTime dt) => !dt.IsWeekend();

    /// <summary>
    /// Returns the next occurrence of the requested DayOfWeek strictly after the given date.
    /// </summary>
    public static DateTime Next(this DateTime dt, DayOfWeek desired)
    {
        int start = (int)dt.DayOfWeek;
        int target = (int)desired;
        int delta = (target - start + 7) % 7;
        if (delta == 0) delta = 7;
        return dt.Date.AddDays(delta).Add(dt.TimeOfDay);
    }

    /// <summary>
    /// Returns the previous occurrence of the requested DayOfWeek strictly before the given date.
    /// </summary>
    public static DateTime Previous(this DateTime dt, DayOfWeek desired)
    {
        int start = (int)dt.DayOfWeek;
        int target = (int)desired;
        int delta = (start - target + 7) % 7;
        if (delta == 0) delta = 7;
        return dt.Date.AddDays(-delta).Add(dt.TimeOfDay);
    }

    /// <summary>
    /// Returns the next or same occurrence of desired DayOfWeek (if dt is already that day it returns dt.Date + dt.TimeOfDay).
    /// </summary>
    public static DateTime NextOrSame(this DateTime dt, DayOfWeek desired)
    {
        int start = (int)dt.DayOfWeek;
        int target = (int)desired;
        int delta = (target - start + 7) % 7;
        return dt.Date.AddDays(delta).Add(dt.TimeOfDay);
    }

    /// <summary>
    /// Returns the previous or same occurrence of desired DayOfWeek.
    /// </summary>
    public static DateTime PreviousOrSame(this DateTime dt, DayOfWeek desired)
    {
        int start = (int)dt.DayOfWeek;
        int target = (int)desired;
        int delta = (start - target + 7) % 7;
        return dt.Date.AddDays(-delta).Add(dt.TimeOfDay);
    }

    // Truncate / Round ---------------------------------------------------

    /// <summary>
    /// Truncates the DateTime to the specified TimeSpan (e.g. minutes, seconds). Preserves Kind.
    /// </summary>
    public static DateTime Truncate(this DateTime dt, TimeSpan to)
    {
        if (to <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(to), "to must be > TimeSpan.Zero");
        long ticks = (dt.Ticks / to.Ticks) * to.Ticks;
        return new DateTime(ticks, dt.Kind);
    }

    /// <summary>
    /// Rounds the DateTime to the nearest TimeSpan interval using MidpointRounding.
    /// </summary>
    public static DateTime Round(this DateTime dt, TimeSpan to, MidpointRounding rounding = MidpointRounding.ToEven)
    {
        if (to <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(to), "to must be > TimeSpan.Zero");
        long rem = dt.Ticks % to.Ticks;
        long half = to.Ticks / 2;
        long roundedTicks = dt.Ticks - rem;
        if (Math.Abs(rem) >= half)
        {
            if (rem > 0)
                roundedTicks += to.Ticks;
            else
                roundedTicks -= to.Ticks;
        }
        return new DateTime(roundedTicks, dt.Kind);
    }

    // Business day helpers -----------------------------------------------

    /// <summary>
    /// Adds business days to the given date (skips Saturday and Sunday).
    /// Accepts negative values to subtract business days.
    /// </summary>
    public static DateTime AddBusinessDays(this DateTime dt, int businessDays)
    {
        if (businessDays == 0) return dt;
        int direction = businessDays > 0 ? 1 : -1;
        int remaining = Math.Abs(businessDays);
        DateTime cursor = dt;
        while (remaining > 0)
        {
            cursor = cursor.AddDays(direction);
            if (cursor.IsWeekday())
                remaining--;
        }
        return cursor;
    }

    /// <summary>
    /// Returns the number of business days between two dates. If inclusive is true the end date is included.
    /// Weekends are excluded. Order of dates does not matter (result is non-negative).
    /// </summary>
    public static int BusinessDaysBetween(this DateTime a, DateTime b, bool inclusive = false)
    {
        var start = a.Date;
        var end = b.Date;
        if (start > end)
        {
            var tmp = start;
            start = end;
            end = tmp;
        }

        int days = (end - start).Days + (inclusive ? 1 : 0);
        int fullWeeks = days / 7;
        int businessDays = fullWeeks * 5;
        int remainder = days % 7;

        for (int i = 0; i < remainder; i++)
        {
            var day = start.AddDays(fullWeeks * 7 + i).DayOfWeek;
            if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday) businessDays++;
        }

        return businessDays;
    }

    // Unix time ----------------------------------------------------------

    /// <summary>
    /// Returns Unix time seconds for a DateTime (interpreted via its Kind; best to use DateTime.SpecifyKind or DateTimeOffset).
    /// </summary>
    public static long ToUnixTimeSeconds(this DateTime dt)
    {
        var dto = new DateTimeOffset(dt);
        return dto.ToUnixTimeSeconds();
    }

    /// <summary>
    /// Returns Unix time milliseconds for a DateTime.
    /// </summary>
    public static long ToUnixTimeMilliseconds(this DateTime dt)
    {
        var dto = new DateTimeOffset(dt);
        return dto.ToUnixTimeMilliseconds();
    }

    // ISO / formatting ---------------------------------------------------

    /// <summary>
    /// Returns ISO-8601 round-trip string (o format).
    /// </summary>
    public static string ToIsoString(this DateTime dt) => dt.ToString("o");

    /// <summary>
    /// Returns ISO-8601 round-trip string (o format) for DateTimeOffset.
    /// </summary>
    public static string ToIsoString(this DateTimeOffset dto) => dto.ToString("o");

    // Time zone helpers -----------------------------------------------

    /// <summary>
    /// Converts a DateTime from one time zone to another using TimeZoneInfo ids.
    /// </summary>
    public static DateTime ConvertTimeZone(this DateTime dt, TimeZoneInfo sourceZone, TimeZoneInfo destZone)
    {
        if (sourceZone == null) throw new ArgumentNullException(nameof(sourceZone));
        if (destZone == null) throw new ArgumentNullException(nameof(destZone));

        var dto = new DateTimeOffset(dt, sourceZone.GetUtcOffset(dt));
        return TimeZoneInfo.ConvertTime(dto, destZone).DateTime;
    }

    /// <summary>
    /// Converts a DateTime to the specified TimeZone by id (throws if id not found).
    /// </summary>
    public static DateTime ConvertToTimeZone(this DateTime dt, string timeZoneId)
    {
        if (timeZoneId == null) throw new ArgumentNullException(nameof(timeZoneId));
        var dest = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return ConvertTimeZone(dt, TimeZoneInfo.Local, dest);
    }
}
