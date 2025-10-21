namespace QuickDotNetExtensions.UnitTests;

public partial class StringExtensionsTests
{
    /********************************************************************************/
    /******************************* EnsureStartsWith *******************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_EnsureStartsWith_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.EnsureStartsWith("any string"));
    }

    [Fact]
    public void StringExtensions_EnsureStartsWith_AddsPrefix_WhenMissing()
    {
        string src = "world";
        string res = src.EnsureStartsWith("hello ");
        Assert.Equal("hello world", res);
    }

    [Fact]
    public void StringExtensions_EnsureStartsWith_NoChange_WhenPresent()
    {
        string src = "HelloWorld";
        string res = src.EnsureStartsWith("hello", StringComparison.OrdinalIgnoreCase);
        Assert.Equal("HelloWorld", res);
    }

    /********************************************************************************/
    /******************************** EnsureEndsWith ********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_EnsureEndsWith_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.EnsureEndsWith("any string"));
    }

    [Fact]
    public void StringExtensions_EnsureEndsWith_AddsSuffix_WhenMissing()
    {
        string src = "file";
        string res = src.EnsureEndsWith(".txt");
        Assert.Equal("file.txt", res);
    }

    [Fact]
    public void StringExtensions_EnsureEndsWith_NoChange_WhenPresent()
    {
        string src = "file.txt";
        string res = src.EnsureEndsWith(".txt");
        Assert.Equal("file.txt", res);
    }

    /********************************************************************************/
    /********************************* RemovePrefix *********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_RemovePrefix_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.RemovePrefix("any string"));
    }

    [Fact]
    public void StringExtensions_RemovePrefix_Removes_WhenPresent_OtherwiseReturnsOriginal()
    {
        string src = "unittest";
        Assert.Equal("test", src.RemovePrefix("unit"));
        Assert.Equal("unittest", src.RemovePrefix("nope"));
    }

    /********************************************************************************/
    /********************************* RemoveSuffix *********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_RemoveSuffix_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.RemoveSuffix("any string"));
    }

    [Fact]
    public void StringExtensions_RemoveSuffix_Removes_WhenPresent_OtherwiseReturnsOriginal()
    {
        string src = "filename.txt";
        Assert.Equal("filename", src.RemoveSuffix(".txt"));
        Assert.Equal("filename.txt", src.RemoveSuffix(".md"));
    }

    /********************************************************************************/
    /***************************** NormalizeLineEndings *****************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_NormalizeLineEndings_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.NormalizeLineEndings("any string"));
    }

    [Fact]
    public void StringExtensions_NormalizeLineEndings_ReplacesVariousLineEndings()
    {
        string src = "line1\r\nline2\nline3\rline4";
        string normalized = src.NormalizeLineEndings("\n");
        Assert.Equal("line1\nline2\nline3\nline4", normalized);
    }

    /********************************************************************************/
    /********************************** SplitLines **********************************/
    /********************************************************************************/
    [Fact]
    public void StringExtensions_SplitLines_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.SplitLines());
    }

    [Fact]
    public void StringExtensions_SplitLines_SplitsOnAnyLineEnding()
    {
        string src = "a\r\nb\nc\rd";
        var lines = src.SplitLines();
        Assert.Equal(new[] { "a", "b", "c", "d" }, lines);
    }
}
