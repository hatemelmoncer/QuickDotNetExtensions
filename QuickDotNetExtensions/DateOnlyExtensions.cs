namespace QuickDotNetExtensions;

public static class DateOnlyExtensions
{
    public static DateTime ToDateTime(this DateOnly dateOnly) => dateOnly.ToDateTime(default);

    public static TimeOnly ToTimeOnly(this DateOnly dt) => TimeOnly.FromDateTime(dt.ToDateTime());

    public static string ToYYYYMMDD(this DateOnly dateOnly) => dateOnly.ToString("yyyyMMdd");

    // Days helpers -------------------------------------------------------

    public static DateOnly Yesterday(this DateOnly dateOnly)
    {
        return dateOnly.AddDays(-1);
    }

    public static DateOnly YesterdayAndSkipWeekend(this DateOnly dateOnly)
    {
        var yesterday = dateOnly.AddDays(-1);

        if (yesterday.DayOfWeek == DayOfWeek.Saturday)
            return yesterday.Yesterday();

        if (yesterday.DayOfWeek == DayOfWeek.Sunday)
            return yesterday.Yesterday().Yesterday();

        return yesterday;
    }

    public static DateOnly Tomorrow(this DateOnly dateOnly)
    {
        return dateOnly.AddDays(1);
    }

    public static DateOnly TomorrowAndSkipWeekend(this DateOnly dateOnly)
    {
        var tomorrow = dateOnly.AddDays(1);

        if (tomorrow.DayOfWeek == DayOfWeek.Saturday)
            return tomorrow.Tomorrow().Tomorrow();

        if (tomorrow.DayOfWeek == DayOfWeek.Sunday)
            return tomorrow.Tomorrow();

        return tomorrow;
    }

    // Month helpers -------------------------------------------------------

    /// <summary>
    /// Returns the first day of the month for the provided date.
    /// </summary>
    public static DateOnly FirstDayOfMonth(this DateOnly d) => new DateOnly(d.Year, d.Month, 1);

    /// <summary>
    /// Returns the last day of the month for the provided date.
    /// </summary>
    public static DateOnly LastDayOfMonth(this DateOnly d)
    {
        int days = DateTime.DaysInMonth(d.Year, d.Month);
        return new DateOnly(d.Year, d.Month, days);
    }

    // Week helpers --------------------------------------------------------

    /// <summary>
    /// Returns the start of the week containing this date.
    /// Defaults to Monday as the first day of week.
    /// </summary>
    public static DateOnly StartOfWeek(this DateOnly d, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        int diff = (7 + ((int)d.DayOfWeek - (int)startOfWeek)) % 7;
        return d.AddDays(-diff);
    }

    /// <summary>
    /// Returns the end of the week containing this date.
    /// Defaults to Monday as the first day of week; end is the last day of that week.
    /// </summary>
    public static DateOnly EndOfWeek(this DateOnly d, DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        var start = d.StartOfWeek(startOfWeek);
        return start.AddDays(6);
    }

    // Weekday helpers -----------------------------------------------------

    /// <summary>
    /// True if the date is Saturday or Sunday.
    /// </summary>
    public static bool IsWeekend(this DateOnly d) =>
        d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// True if the date is not Saturday or Sunday.
    /// </summary>
    public static bool IsWeekday(this DateOnly d) => !d.IsWeekend();

    /// <summary>
    /// Returns the next occurrence of the requested DayOfWeek strictly after the given date.
    /// </summary>
    public static DateOnly Next(this DateOnly d, DayOfWeek desired)
    {
        int start = (int)d.DayOfWeek;
        int target = (int)desired;
        int delta = (target - start + 7) % 7;
        if (delta == 0) delta = 7;
        return d.AddDays(delta);
    }

    /// <summary>
    /// Returns the previous occurrence of the requested DayOfWeek strictly before the given date.
    /// </summary>
    public static DateOnly Previous(this DateOnly d, DayOfWeek desired)
    {
        int start = (int)d.DayOfWeek;
        int target = (int)desired;
        int delta = (start - target + 7) % 7;
        if (delta == 0) delta = 7;
        return d.AddDays(-delta);
    }

    /// <summary>
    /// Returns the next or same occurrence of desired DayOfWeek (if d is already that day it returns d).
    /// </summary>
    public static DateOnly NextOrSame(this DateOnly d, DayOfWeek desired)
    {
        int start = (int)d.DayOfWeek;
        int target = (int)desired;
        int delta = (target - start + 7) % 7;
        return d.AddDays(delta);
    }

    /// <summary>
    /// Returns the previous or same occurrence of desired DayOfWeek.
    /// </summary>
    public static DateOnly PreviousOrSame(this DateOnly d, DayOfWeek desired)
    {
        int start = (int)d.DayOfWeek;
        int target = (int)desired;
        int delta = (start - target + 7) % 7;
        return d.AddDays(-delta);
    }

    // Business day helpers -----------------------------------------------

    /// <summary>
    /// Adds business days to the given date (skips Saturday and Sunday).
    /// Accepts negative values to subtract business days.
    /// </summary>
    public static DateOnly AddBusinessDays(this DateOnly d, int businessDays)
    {
        if (businessDays == 0) return d;
        int direction = businessDays > 0 ? 1 : -1;
        int remaining = Math.Abs(businessDays);
        DateOnly cursor = d;
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
    public static int BusinessDaysBetween(this DateOnly a, DateOnly b, bool inclusive = false)
    {
        var start = a;
        var end = b;
        if (start > end)
        {
            var tmp = start;
            start = end;
            end = tmp;
        }

        int days = (end.ToDateTime(TimeOnly.MinValue) - start.ToDateTime(TimeOnly.MinValue)).Days + (inclusive ? 1 : 0);
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

    // ISO / formatting ---------------------------------------------------

    /// <summary>
    /// Returns ISO-8601 date string (YYYY-MM-DD).
    /// </summary>
    public static string ToIsoString(this DateOnly d) => d.ToString("yyyy-MM-dd");
}
