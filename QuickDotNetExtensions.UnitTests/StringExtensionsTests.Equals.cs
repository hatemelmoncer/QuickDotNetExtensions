namespace QuickDotNetExtensions.UnitTests;

public partial class StringExtensionsTests
{
    /********************************************************************************/
    /******************************* EqualsIgnoreCase *******************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_EqualsIgnoreCase_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.EqualsIgnoreCase("any string"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCase_Should_ReturnTrue_When_StringsAreStrictEquals()
    {
        string source = "source";
        Assert.True(source.EqualsIgnoreCase("source"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCase_Should_ReturnTrue_When_StringsAreEqualsIfIgnoringCase()
    {
        string source = "source";
        Assert.True(source.EqualsIgnoreCase("SouRCE"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCase_Should_ReturnFalse_When_StringsAreDifferents()
    {
        string source = "source";
        Assert.False(source.EqualsIgnoreCase("I am different"));
    }

    /********************************************************************************/
    /******************************* EqualsIgnoreCaseInvariant *******************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_EqualsIgnoreCaseInvariant_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.EqualsIgnoreCaseInvariant("any string"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCaseInvariant_Should_ReturnTrue_When_StringsAreStrictEquals()
    {
        string source = "source";
        Assert.True(source.EqualsIgnoreCaseInvariant("source"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCaseInvariant_Should_ReturnTrue_When_StringsAreEqualsIfIgnoringCase()
    {
        string source = "source";
        Assert.True(source.EqualsIgnoreCaseInvariant("SouRCE"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCaseInvariant_Should_ReturnFalse_When_StringsAreDifferents()
    {
        string source = "source";
        Assert.False(source.EqualsIgnoreCaseInvariant("I am different"));
    }

    /********************************************************************************/
    /******************************* EqualsIgnoreCaseOrdinal *******************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_EqualsIgnoreCaseOrdinal_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.EqualsIgnoreCaseOrdinal("any string"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCaseOrdinal_Should_ReturnTrue_When_StringsAreStrictEquals()
    {
        string source = "source";
        Assert.True(source.EqualsIgnoreCaseOrdinal("source"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCaseOrdinal_Should_ReturnTrue_When_StringsAreEqualsIfIgnoringCase()
    {
        string source = "source";
        Assert.True(source.EqualsIgnoreCaseOrdinal("SouRCE"));
    }

    [Fact]
    public void StringExtensions_EqualsIgnoreCaseOrdinal_Should_ReturnFalse_When_StringsAreDifferents()
    {
        string source = "source";
        string.Format("test {0}", "value");
        Assert.False(source.EqualsIgnoreCaseOrdinal("I am different"));
    }
}
