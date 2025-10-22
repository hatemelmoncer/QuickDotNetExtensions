namespace QuickDotNetExtensions.UnitTests;

public class DecimalExtensionsTests
{
    [Fact]
    public void DecimalExtensions_RoundKeepOneDigit_Should_ReturnDecimalAndKeepOneDigit_RoundingBig()
    {
        decimal input = (decimal)10.788;
        Assert.Equal((decimal)10.8, input.RoundKeepOneDigit());
    }

    [Fact]
    public void DecimalExtensions_RoundKeepOneDigit_Should_ReturnDecimalAndKeepOneDigit_RoundingSmall()
    {
        decimal input = (decimal)10.722;
        Assert.Equal((decimal)10.7, input.RoundKeepOneDigit());
    }

    [Fact]
    public void DecimalExtensions_RoundKeepTwoDigits_Should_ReturnDecimalAndKeepTwoDigits_RoundingBig()
    {
        decimal input = (decimal)10.7788;
        Assert.Equal((decimal)10.78, input.RoundKeepTwoDigits());
    }

    [Fact]
    public void DecimalExtensions_RoundKeepTwoDigits_Should_ReturnDecimalAndKeepTwoDigits_RoundingSmall()
    {
        decimal input = (decimal)10.7722;
        Assert.Equal((decimal)10.77, input.RoundKeepTwoDigits());
    }

    [Fact]
    public void DecimalExtensions_RoundKeepThreeDigits_Should_ReturnDecimalAndKeepThreeDigits_RoundingBig()
    {
        decimal input = (decimal)10.7788;
        Assert.Equal((decimal)10.779, input.RoundKeepThreeDigits());
    }

    [Fact]
    public void DecimalExtensions_RoundKeepThreeDigits_Should_ReturnDecimalAndKeepThreeDigits_RoundingSmall()
    {
        decimal input = (decimal)10.7722;
        Assert.Equal((decimal)10.772, input.RoundKeepThreeDigits());
    }

    [Fact]
    public void DecimalExtensions_RoundKeepDigits_Should_ReturnDecimalAndKeepDigits_RoundingBig()
    {
        decimal input = (decimal)10.7788;
        Assert.Equal((decimal)10.779, input.RoundKeepDigits(3));
    }

    [Fact]
    public void DecimalExtensions_RoundKeepDigits_Should_ReturnDecimalAndKeepDigits_RoundingSmall()
    {
        decimal input = (decimal)10.7722;
        Assert.Equal((decimal)10.772, input.RoundKeepDigits(3));
    }
}
