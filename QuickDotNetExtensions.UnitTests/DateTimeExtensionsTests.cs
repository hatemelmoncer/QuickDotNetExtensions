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
}
