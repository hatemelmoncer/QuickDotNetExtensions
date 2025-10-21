namespace QuickDotNetExtensions.UnitTests;

public partial class StringExtensionsTests
{
    /********************************************************************************/
    /********************************* ToEnumOrNull *********************************/
    /********************************************************************************/
    public enum EnumOfTest
    {
        First, 
        Second,
    }

    [Fact]
    public void StringExtensions_ToEnumOrNull_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.EqualsIgnoreCase("any string"));
    }

    [Fact]
    public void StringExtensions_ToEnumOrNull_Should_ReturnNull_When_StringIsEmpty()
    {
        string source = "";
        Assert.Null(source.ToEnumOrNull<EnumOfTest>());
    }

    [Fact]
    public void StringExtensions_ToEnumOrNull_Should_ReturnNull_When_StringNotEqualsNotAnyEnumField()
    {
        string source = "Not Equals";
        Assert.Null(source.ToEnumOrNull<EnumOfTest>());
    }

    [Fact]
    public void StringExtensions_ToEnumOrNull_Should_ReturnEnumField_When_StringEqualsToEnumField()
    {
        string source = "First";
        Assert.Equal(EnumOfTest.First, source.ToEnumOrNull<EnumOfTest>());
    }


    /********************************************************************************/
    /********************************** FormatWith **********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_FormatWith_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.FormatWith("any string"));
    }

    [Fact]
    public void StringExtensions_FormatWith_FormatsUsingCurrentCulture()
    {
        string fmt = "Hello {0}";
        Assert.Equal("Hello world", fmt.FormatWith("world"));

        // Null format should throw
        Assert.Throws<ArgumentNullException>(() => ((string)null).FormatWith("x"));
    }
}