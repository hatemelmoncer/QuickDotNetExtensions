namespace QuickDotNetExtensions;

public static class DecimalExtensions
{
    public static decimal RoundKeepOneDigit(this decimal value)
    {
        return RoundKeepDigits(value, 1);
    }

    public static decimal RoundKeepTwoDigits(this decimal value)
    {
        return RoundKeepDigits(value, 2);
    }

    public static decimal RoundKeepThreeDigits(this decimal value)
    {
        return RoundKeepDigits(value, 3);
    }

    public static decimal RoundKeepDigits(this decimal value, int digitsToKeep)
    {
        return decimal.Round(value, digitsToKeep, MidpointRounding.AwayFromZero);
    }
}
