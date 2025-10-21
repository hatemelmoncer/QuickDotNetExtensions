namespace QuickDotNetExtensions.UnitTests;

public partial class StringExtensionsTests
{
    /**********************************************************************************/
    /************************************* Between ************************************/
    /**********************************************************************************/
    [Fact]
    public void StringExtensions_Between_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.Between("first sequence", "second sequence"));
    }

    [Fact]
    public void StringExtensions_Between_Should_Throw_When_FirstSequenceIsNull()
    {
        string source = "this is a test";
        string firstSequence = null!;
        Assert.Throws<ArgumentNullException>(() => source.Between(firstSequence, "test"));
    }

    [Fact]
    public void StringExtensions_Between_Should_Throw_When_SecondSequenceIsNull()
    {
        string source = "this is a test";
        string secondSequence = null!;
        Assert.Throws<ArgumentNullException>(() => source.Between("this", secondSequence));
    }

    [Fact]
    public void StringExtensions_Between_Should_ReturnEmptyString_When_FirstSequenceIsNotFound()
    {
        string source = "this begin my source end";
        Assert.Equal(string.Empty, source.Between("i am not found", "end"));
    }

    [Fact]
    public void StringExtensions_Between_Should_ReturnEmptyString_When_SecondSequenceIsNotFound()
    {
        string source = "this begin my source end";
        Assert.Equal(string.Empty, source.Between("begin", "i am not found"));
    }

    [Fact]
    public void StringExtensions_Between_Should_ReturnStringBetweenTheTwoSequences_When_Ok()
    {
        string source = "this begin my source end";
        Assert.Equal(" my source ", source.Between("begin", "end"));
    }

    /**********************************************************************************/
    /******************************* BetweenOrThrow ***********************************/
    /**********************************************************************************/
    [Fact]
    public void StringExtensions_BetweenOrThrow_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.BetweenOrThrow("first", "second"));
    }

    [Fact]
    public void StringExtensions_BetweenOrThrow_Should_Throw_When_FirstSequenceIsNull()
    {
        string source = "this is a test";
        string firstSequence = null!;
        Assert.Throws<ArgumentNullException>(() => source.BetweenOrThrow(firstSequence, "test"));
    }

    [Fact]
    public void StringExtensions_BetweenOrThrow_Should_Throw_When_SecondSequenceIsNull()
    {
        string source = "this is a test";
        string secondSequence = null!;
        Assert.Throws<ArgumentNullException>(() => source.BetweenOrThrow("this", secondSequence));
    }

    [Fact]
    public void StringExtensions_BetweenOrThrow_Should_Throw_StringNotFoundException_When_FirstSequenceNotFound()
    {
        string source = "this is a test";
        Assert.Throws<StringNotFoundException>(() => source.BetweenOrThrow("notfound", "test"));
    }

    [Fact]
    public void StringExtensions_BetweenOrThrow_Should_Throw_StringNotFoundException_When_SecondSequenceNotFound()
    {
        string source = "this is a test";
        Assert.Throws<StringNotFoundException>(() => source.BetweenOrThrow("this", "notfound"));
    }

    [Fact]
    public void StringExtensions_BetweenOrThrow_Should_ReturnStringBetweenSequences_When_Ok()
    {
        string source = "start middle end";
        Assert.Equal(" middle ", source.BetweenOrThrow("start", "end"));
    }

    /********************************************************************************/
    /************************************* Left ************************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_Left_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.Left(7));
    }

    [Fact]
    public void StringExtensions_Left_Should_ReturnEmptyString_When_StringIsEmptyAndLengthIsZero()
    {
        string source = "";
        Assert.Equal("", source.Left(0));
    }

    [Fact]
    public void StringExtensions_Left_Should_ReturnStringInTheLeftBeforeReachingTheLength_When_Ok()
    {
        string source = "this my source";
        Assert.Equal("this my", source.Left(7));
    }

    /********************************************************************************/
    /********************************* LeftOrThrow **********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_LeftOrThrow_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.LeftOrThrow(5));
    }

    [Fact]
    public void StringExtensions_LeftOrThrow_Should_Throw_When_LengthIsGreaterThanStringLength()
    {
        string source = "abc";
        Assert.Throws<ArgumentException>(() => source.LeftOrThrow(10));
    }

    [Fact]
    public void StringExtensions_LeftOrThrow_Should_Return_WholeString_When_LengthEqualsStringLength()
    {
        string source = "abcdef";
        Assert.Equal("abcdef", source.LeftOrThrow(6));
    }

    [Fact]
    public void StringExtensions_LeftOrThrow_Should_Return_LeftSubstring_When_LengthIsLessThanStringLength()
    {
        string source = "abcdef";
        Assert.Equal("abc", source.LeftOrThrow(3));
    }

    /********************************************************************************/
    /************************************* Right ************************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_Right_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.Right(5));
    }

    [Fact]
    public void StringExtensions_Right_Should_Return_WholeString_When_LengthIsGreaterThanStringLength()
    {
        string source = "abc";
        Assert.Equal("abc", source.Right(10));
    }

    [Fact]
    public void StringExtensions_Right_Should_Return_EmptyString_When_StringIsEmptyAndLengthIsZero()
    {
        string source = "";
        Assert.Equal("", source.Right(0));
    }

    [Fact]
    public void StringExtensions_Right_Should_Return_RightSubstring_When_LengthIsLessThanStringLength()
    {
        string source = "abcdef";
        Assert.Equal("def", source.Right(3));
    }

    [Fact]
    public void StringExtensions_Right_Should_Return_WholeString_When_LengthEqualsStringLength()
    {
        string source = "abcdef";
        Assert.Equal("abcdef", source.Right(6));
    }

    /********************************************************************************/
    /********************************** RightOrThrow ********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_RightOrThrow_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.RightOrThrow(7));
    }

    [Fact]
    public void StringExtensions_RightOrThrow_Should_Throw_When_LengthIsGreaterThanTheStringLength()
    {
        string source = "this my source";
        Assert.Throws<ArgumentException>(() => source.RightOrThrow(777));
    }

    [Fact]
    public void StringExtensions_RightOrThrow_Should_ReturnEmptyString_When_StringIsEmptyAndLengthIsZero()
    {
        string source = "";
        Assert.Equal("", source.RightOrThrow(0));
    }

    [Fact]
    public void StringExtensions_RightOrThrow_Should_ReturnStringInTheRightStartingFromTheLenght_When_Ok()
    {
        string source = "this my source";
        Assert.Equal(" source", source.RightOrThrow(7));
    }
}
