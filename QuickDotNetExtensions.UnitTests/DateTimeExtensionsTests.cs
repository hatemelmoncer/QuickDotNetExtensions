namespace QuickDotNetExtensions.UnitTests;

public class DateTimeExtensionsTests
{
    [Fact]
    public void DateTimeExtensions_ToDateOnly_ReturnsCorrectDateOnly()
    {
        var dt = new DateTime(2024, 6, 1, 15, 30, 0);
        var dateOnly = dt.ToDateOnly();
        Assert.Equal(new DateOnly(2024, 6, 1), dateOnly);
    }

    [Fact]
    public void DateTimeExtensions_ToYYYYMMDD_ReturnsCorrectFormat()
    {
        var dt = new DateTime(2024, 6, 5, 10, 0, 0);
        Assert.Equal("20240605", dt.ToYYYYMMDD());
    }

    [Fact]
    public void DateTimeExtensions_Yesterday_ReturnsPreviousDay()
    {
        var dt = new DateTime(2024, 6, 2);
        Assert.Equal(new DateTime(2024, 6, 1), dt.Yesterday());
    }

    [Theory]
    [InlineData(2024, 6, 3, 2024, 5, 31)] // Monday -> Friday
    [InlineData(2024, 6, 2, 2024, 5, 31)] // Sunday -> Friday
    [InlineData(2024, 6, 1, 2024, 5, 31)] // Saturday -> Friday
    [InlineData(2024, 6, 4, 2024, 6, 3)]  // Tuesday -> Monday
    public void DateTimeExtensions_YesterdayAndSkipWeekend_SkipsWeekendCorrectly(int y, int m, int d, int ey, int em, int ed)
    {
        var dt = new DateTime(y, m, d);
        var expected = new DateTime(ey, em, ed);
        Assert.Equal(expected, dt.YesterdayAndSkipWeekend());
    }

    [Fact]
    public void DateTimeExtensions_Tomorrow_ReturnsNextDay()
    {
        var dt = new DateTime(2024, 6, 1);
        Assert.Equal(new DateTime(2024, 6, 2), dt.Tomorrow());
    }

    [Theory]
    [InlineData(2024, 5, 31, 2024, 6, 3)] // Friday -> Monday
    [InlineData(2024, 6, 1, 2024, 6, 3)]  // Saturday -> Monday
    [InlineData(2024, 6, 2, 2024, 6, 3)]  // Sunday -> Monday
    [InlineData(2024, 6, 3, 2024, 6, 4)]  // Monday -> Tuesday
    public void DateTimeExtensions_TomorrowAndSkipWeekend_SkipsWeekendCorrectly(int y, int m, int d, int ey, int em, int ed)
    {
        var dt = new DateTime(y, m, d);
        var expected = new DateTime(ey, em, ed);
        Assert.Equal(expected, dt.TomorrowAndSkipWeekend());
    }

    [Fact]
    public void StartOfDay_And_EndOfDay_Behavior()
    {
        var dt = new DateTime(2020, 1, 2, 15, 30, 45, DateTimeKind.Utc);
        Assert.Equal(new DateTime(2020, 1, 2, 0, 0, 0, DateTimeKind.Utc), dt.StartOfDay());
        Assert.Equal(dt.Date.AddDays(1).AddTicks(-1), dt.EndOfDay());
    }

    [Fact]
    public void FirstDayOfMonth_And_LastDayOfMonth_Behavior()
    {
        var dt = new DateTime(2021, 2, 15, 5, 6, 7, DateTimeKind.Local);
        Assert.Equal(new DateTime(2021, 2, 1, 0, 0, 0, DateTimeKind.Local), dt.FirstDayOfMonth());
        Assert.Equal(new DateTime(2021, 2, DateTime.DaysInMonth(2021, 2), 0, 0, 0, DateTimeKind.Local), dt.LastDayOfMonth());
    }

    [Fact]
    public void StartOfWeek_And_EndOfWeek_DefaultMonday()
    {
        var dt = new DateTime(2021, 9, 15, 13, 45, 0, DateTimeKind.Unspecified); // Wednesday
        var start = dt.StartOfWeek(); // Monday the 13th with same TimeOfDay
        Assert.Equal(new DateTime(2021, 9, 13, 13, 45, 0, DateTimeKind.Unspecified), start);
        var expectedEnd = start.Date.AddDays(7).AddTicks(-1);
        Assert.Equal(expectedEnd, dt.EndOfWeek());
    }

    [Fact]
    public void IsWeekend_IsWeekday()
    {
        var sat = new DateTime(2021, 9, 18); // Saturday
        var mon = new DateTime(2021, 9, 20); // Monday
        Assert.True(sat.IsWeekend());
        Assert.False(sat.IsWeekday());
        Assert.False(mon.IsWeekend());
        Assert.True(mon.IsWeekday());
    }

    [Fact]
    public void Next_Previous_NextOrSame_PreviousOrSame_Work()
    {
        var dt = new DateTime(2021, 9, 15, 8, 0, 0); // Wednesday
        var nextFri = dt.Next(DayOfWeek.Friday);
        Assert.Equal(new DateTime(2021, 9, 17, 8, 0, 0), nextFri);

        // Next on same day yields next week
        var nextWed = dt.Next(DayOfWeek.Wednesday);
        Assert.Equal(new DateTime(2021, 9, 22, 8, 0, 0), nextWed);

        var prevMon = dt.Previous(DayOfWeek.Monday);
        Assert.Equal(new DateTime(2021, 9, 13, 8, 0, 0), prevMon);

        // NextOrSame when same day returns same-date (time preserved)
        var nextOrSameWed = dt.NextOrSame(DayOfWeek.Wednesday);
        Assert.Equal(new DateTime(2021, 9, 15, 8, 0, 0), nextOrSameWed);

        // PreviousOrSame when same day returns same-date
        var prevOrSameWed = dt.PreviousOrSame(DayOfWeek.Wednesday);
        Assert.Equal(new DateTime(2021, 9, 15, 8, 0, 0), prevOrSameWed);
    }

    [Fact]
    public void Truncate_RoundsDown_ToTimespan()
    {
        var dt = new DateTime(2021, 1, 1, 1, 2, 3, 123, DateTimeKind.Utc);
        var truncatedToMinute = dt.Truncate(TimeSpan.FromMinutes(1));
        Assert.Equal(new DateTime(2021, 1, 1, 1, 2, 0, DateTimeKind.Utc), truncatedToMinute);

        Assert.Throws<ArgumentOutOfRangeException>(() => dt.Truncate(TimeSpan.Zero));
    }

    [Fact]
    public void Round_RoundsToNearestInterval()
    {
        var dt = new DateTime(2021, 1, 1, 1, 2, 30, 0, DateTimeKind.Utc);
        var rounded = dt.Round(TimeSpan.FromMinutes(1));
        Assert.Equal(new DateTime(2021, 1, 1, 1, 3, 0, DateTimeKind.Utc), rounded);

        Assert.Throws<ArgumentOutOfRangeException>(() => dt.Round(TimeSpan.Zero));
    }

    [Fact]
    public void AddBusinessDays_SkipsWeekends()
    {
        var fri = new DateTime(2021, 9, 17); // Friday
        var plusOne = fri.AddBusinessDays(1);
        Assert.Equal(new DateTime(2021, 9, 20), plusOne); // Monday

        var mon = new DateTime(2021, 9, 20);
        var minusOne = mon.AddBusinessDays(-1);
        Assert.Equal(new DateTime(2021, 9, 17), minusOne); // Friday
    }

    [Fact]
    public void BusinessDaysBetween_ComputesCorrectly()
    {
        var a = new DateTime(2021, 9, 13); // Monday
        var b = new DateTime(2021, 9, 17); // Friday
        Assert.Equal(5, a.BusinessDaysBetween(b, inclusive: true));
        Assert.Equal(4, a.BusinessDaysBetween(b, inclusive: false));
    }

    [Fact]
    public void DateOnly_And_TimeOnly_Conversions_And_Combine()
    {
        var dt = new DateTime(2021, 12, 31, 23, 45, 10, DateTimeKind.Unspecified);
        var dateOnly = dt.ToDateOnly();
        var timeOnly = dt.ToTimeOnly();
        Assert.Equal(DateOnly.FromDateTime(dt), dateOnly);
        Assert.Equal(TimeOnly.FromDateTime(dt), timeOnly);

        // combine back
        var combined = dateOnly.ToDateTime(timeOnly, DateTimeKind.Unspecified);
        Assert.Equal(dt, combined);
    }

    [Fact]
    public void UnixTime_Roundtrip_And_Iso_Formatting()
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(0L, epoch.ToUnixTimeSeconds());
        Assert.Equal(0L, epoch.ToUnixTimeMilliseconds());

        var dt = new DateTime(2021, 3, 4, 5, 6, 7, DateTimeKind.Utc);
        Assert.Equal(dt.ToString("o"), dt.ToIsoString());
    }

    [Fact]
    public void ConvertTimeZone_And_ConvertToTimeZone_ArgumentValidation_And_BasicConsistency()
    {
        var dt = new DateTime(2021, 9, 15, 12, 0, 0, DateTimeKind.Unspecified);

        // null zone arguments
        Assert.Throws<ArgumentNullException>(() => DateTimeExtensions.ConvertTimeZone(dt, null, TimeZoneInfo.Utc));
        Assert.Throws<ArgumentNullException>(() => DateTimeExtensions.ConvertTimeZone(dt, TimeZoneInfo.Utc, null));

        // ConvertToTimeZone null id
        Assert.Throws<ArgumentNullException>(() => dt.ConvertToTimeZone(null));

        // Basic consistency: converting local -> UTC via ConvertTimeZone should equal ConvertToTimeZone using "UTC" id
        var utcZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");
        var localZone = TimeZoneInfo.Local;
        var converted1 = DateTimeExtensions.ConvertTimeZone(dt, localZone, utcZone);
        var converted2 = dt.ConvertToTimeZone("UTC");
        Assert.Equal(converted1, converted2);

        // Roundtrip: local -> UTC -> local should yield a DateTime representing same instant (kind may vary)
        var back = DateTimeExtensions.ConvertTimeZone(converted1, utcZone, localZone);
        // We can't guarantee Kind, but the instant/time-of-day representation should match when considered as DateTime in local zone:
        // ConvertTimeZone used offsets; roundtrip should produce the same wall-clock when starting from local and converting back.
        // So we assert that back.Date == dt.Date and back.TimeOfDay == dt.TimeOfDay
        Assert.Equal(dt.Date, back.Date);
        Assert.Equal(dt.TimeOfDay, back.TimeOfDay);
    }
}
