namespace QuickDotNetExtensions;

public static class DateTimeExtensions
{
    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static string ToYYYYMMDD(this DateTime dateTime)
    {
        return dateTime.ToString("yyyyMMdd");
    }

    public static DateTime Yesterday(this DateTime dateTime)
    {
        return dateTime.AddDays(-1);
    }

    public static DateTime YesterdayAndSkipWeekend(this DateTime dateTime)
    {
        var yesterday = dateTime.AddDays(-1);

        if (yesterday.DayOfWeek == DayOfWeek.Saturday)
            return yesterday.Yesterday();

        if (yesterday.DayOfWeek == DayOfWeek.Sunday)
            return yesterday.Yesterday().Yesterday();

        return yesterday;
    }

    public static DateTime Tomorrow(this DateTime dateTime)
    {
        return dateTime.AddDays(1);
    }

    public static DateTime TomorrowAndSkipWeekend(this DateTime dateTime)
    {
        var tomorrow = dateTime.AddDays(1);

        if (tomorrow.DayOfWeek == DayOfWeek.Saturday)
            return tomorrow.Tomorrow().Tomorrow();

        if (tomorrow.DayOfWeek == DayOfWeek.Sunday)
            return tomorrow.Tomorrow();

        return tomorrow;
    }
}
