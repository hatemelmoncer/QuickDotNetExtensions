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
}
