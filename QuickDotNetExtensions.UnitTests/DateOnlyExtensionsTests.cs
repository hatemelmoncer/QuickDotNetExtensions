namespace QuickDotNetExtensions.UnitTests;

public class DateOnlyExtensionsTests
{
    [Fact]
    public void DateOnlyExtensions_ToDateTime_ReturnsDateTimeWithDefaultTime()
    {
        var date = new DateOnly(2024, 6, 1);
        var dt = date.ToDateTime();
        Assert.Equal(new DateTime(2024, 6, 1), dt);
    }

    [Fact]
    public void DateOnlyExtensions_ToYYYYMMDD_ReturnsCorrectFormat()
    {
        var date = new DateOnly(2024, 6, 5);
        Assert.Equal("20240605", date.ToYYYYMMDD());
    }

    [Fact]
    public void DateOnlyExtensions_Yesterday_ReturnsPreviousDay()
    {
        var date = new DateOnly(2024, 6, 2);
        Assert.Equal(new DateOnly(2024, 6, 1), date.Yesterday());
    }

    [Theory]
    [InlineData(2024, 6, 3, 2024, 5, 31)] // Monday -> Friday
    [InlineData(2024, 6, 2, 2024, 5, 31)] // Sunday -> Friday
    [InlineData(2024, 6, 1, 2024, 5, 31)] // Saturday -> Friday
    [InlineData(2024, 6, 4, 2024, 6, 3)]  // Tuesday -> Monday
    public void DateOnlyExtensions_YesterdayAndSkipWeekend_SkipsWeekendCorrectly(int y, int m, int d, int ey, int em, int ed)
    {
        var date = new DateOnly(y, m, d);
        var expected = new DateOnly(ey, em, ed);
        Assert.Equal(expected, date.YesterdayAndSkipWeekend());
    }

    [Fact]
    public void DateOnlyExtensions_Tomorrow_ReturnsNextDay()
    {
        var date = new DateOnly(2024, 6, 1);
        Assert.Equal(new DateOnly(2024, 6, 2), date.Tomorrow());
    }

    [Theory]
    [InlineData(2024, 5, 31, 2024, 6, 3)] // Friday -> Monday
    [InlineData(2024, 6, 1, 2024, 6, 3)]  // Saturday -> Monday
    [InlineData(2024, 6, 2, 2024, 6, 3)]  // Sunday -> Monday
    [InlineData(2024, 6, 3, 2024, 6, 4)]  // Monday -> Tuesday
    public void DateOnlyExtensions_TomorrowAndSkipWeekend_SkipsWeekendCorrectly(int y, int m, int d, int ey, int em, int ed)
    {
        var date = new DateOnly(y, m, d);
        var expected = new DateOnly(ey, em, ed);
        Assert.Equal(expected, date.TomorrowAndSkipWeekend());
    }

    [Fact]
    public void FirstAndLastDayOfMonth_Work()
    {
        var d = new DateOnly(2021, 5, 15);
        Assert.Equal(new DateOnly(2021, 5, 1), d.FirstDayOfMonth());

        var febLeap = new DateOnly(2020, 2, 10);
        Assert.Equal(new DateOnly(2020, 2, 29), febLeap.LastDayOfMonth());

        var apr = new DateOnly(2021, 4, 30);
        Assert.Equal(new DateOnly(2021, 4, 30), apr.LastDayOfMonth());
    }

    [Fact]
    public void StartAndEndOfWeek_DefaultMonday()
    {
        var d = new DateOnly(2021, 9, 15); // Wednesday
        var start = d.StartOfWeek(); // Monday 13th
        Assert.Equal(new DateOnly(2021, 9, 13), start);
        var end = d.EndOfWeek();
        Assert.Equal(start.AddDays(6), end);
    }

    [Fact]
    public void IsWeekend_IsWeekday()
    {
        var sat = new DateOnly(2021, 9, 18); // Saturday
        var mon = new DateOnly(2021, 9, 20); // Monday
        Assert.True(sat.IsWeekend());
        Assert.False(sat.IsWeekday());
        Assert.False(mon.IsWeekend());
        Assert.True(mon.IsWeekday());
    }

    [Fact]
    public void NextPreviousNextOrSamePreviousOrSame_Behavior()
    {
        var d = new DateOnly(2021, 9, 15); // Wednesday

        Assert.Equal(new DateOnly(2021, 9, 17), d.Next(DayOfWeek.Friday));
        // Next strictly returns next occurrence (same-day returns +7)
        Assert.Equal(new DateOnly(2021, 9, 22), d.Next(DayOfWeek.Wednesday));

        Assert.Equal(new DateOnly(2021, 9, 13), d.Previous(DayOfWeek.Monday));

        // NextOrSame and PreviousOrSame
        Assert.Equal(new DateOnly(2021, 9, 15), d.NextOrSame(DayOfWeek.Wednesday));
        Assert.Equal(new DateOnly(2021, 9, 15), d.PreviousOrSame(DayOfWeek.Wednesday));
    }

    [Fact]
    public void AddBusinessDays_SkipsWeekends()
    {
        var fri = new DateOnly(2021, 9, 17); // Friday
        var plusOne = fri.AddBusinessDays(1);
        Assert.Equal(new DateOnly(2021, 9, 20), plusOne); // Monday

        var mon = new DateOnly(2021, 9, 20);
        var minusOne = mon.AddBusinessDays(-1);
        Assert.Equal(new DateOnly(2021, 9, 17), minusOne); // Friday

        // Zero returns same date
        Assert.Equal(mon, mon.AddBusinessDays(0));
    }

    [Fact]
    public void BusinessDaysBetween_ComputesCorrectly()
    {
        var a = new DateOnly(2021, 9, 13); // Monday
        var b = new DateOnly(2021, 9, 17); // Friday
        Assert.Equal(5, a.BusinessDaysBetween(b, inclusive: true));
        Assert.Equal(4, a.BusinessDaysBetween(b, inclusive: false));

        // reversed order should be same
        Assert.Equal(5, b.BusinessDaysBetween(a, inclusive: true));
    }

    [Fact]
    public void ToIsoString_FormatsAsYYYYMMDD()
    {
        var d = new DateOnly(2021, 7, 9);
        Assert.Equal("2021-07-09", d.ToIsoString());

        var d2 = new DateOnly(5, 1, 2); // year 5 -> padded to 0005
        Assert.Equal("0005-01-02", d2.ToIsoString());
    }
}
