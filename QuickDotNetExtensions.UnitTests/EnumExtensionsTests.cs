using System.ComponentModel;

namespace QuickDotNetExtensions.UnitTests;

public class EnumExtensionsTests
{
    [Fact]
    public void EnumExtensions_GetDescription_Should_Throw_When_SourceIsNull()
    {
        TestEnum? source = null!;
        Assert.Throws<ArgumentNullException>(() => source.GetDescription());
    }

    [Fact]
    public void EnumExtensions_GetDescription_Should_ReturnDescription_When_DescriptionIsIndicated()
    {
        TestEnum source = TestEnum.First;
        Assert.Equal("First Description", source.GetDescription());
    }

    [Fact]
    public void EnumExtensions_GetDescription_Should_ReturnEnumToString_When_DescriptionIsNotIndicated()
    {
        TestEnum source = TestEnum.Second;
        Assert.Equal("Second", source.GetDescription());
    }
}

public enum TestEnum
{
    [Description("First Description")]
    First,
    Second
}
