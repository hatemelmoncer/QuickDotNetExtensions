namespace QuickDotNetExtensions;

public static class DateOnlyExtensions
{
    public static DateTime ToDateTime(this DateOnly dateOnly)
    {
        return dateOnly.ToDateTime(default);
    }

    public static string ToYYYYMMDD(this DateOnly dateOnly)
    {
        return dateOnly.ToString("yyyyMMdd");
    }

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
}
