using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDotNetExtensions.UnitTests;

public partial class StringExtensionsTests
{
    [Fact]
    public void StringExtensions_Base64_Should_Throw_When_SourceIsNull()
    {
        string source = null!;
        Assert.Throws<ArgumentNullException>(() => source.ToBase64());
    }

    [Fact]
	public void StringExtensions_Base64_Roundtrip_Works()
	{
		string src = "Hello, 世界!";
		string b64 = src.ToBase64();
		string decoded = b64.FromBase64();
		Assert.Equal(src, decoded);
	}
}
