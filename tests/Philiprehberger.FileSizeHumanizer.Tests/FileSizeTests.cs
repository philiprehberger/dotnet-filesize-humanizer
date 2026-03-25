using Xunit;
namespace Philiprehberger.FileSizeHumanizer.Tests;

public class FileSizeTests
{
    [Fact]
    public void Humanize_ZeroBytes_ReturnsZeroB()
    {
        var result = FileSize.Humanize(0, 2);

        Assert.Equal("0 B", result);
    }

    [Theory]
    [InlineData(1, "1 B")]
    [InlineData(512, "512 B")]
    [InlineData(1023, "1023 B")]
    public void Humanize_BytesUnderOneKB_ReturnsBytesWithUnit(long bytes, string expected)
    {
        var result = FileSize.Humanize(bytes, 1);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1024, "1.0 KB")]
    [InlineData(1048576, "1.0 MB")]
    [InlineData(1073741824, "1.0 GB")]
    public void Humanize_LargerValues_ReturnsLegacyUnits(long bytes, string expected)
    {
        var result = FileSize.Humanize(bytes, 1);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1536, 0, "2 KB")]
    [InlineData(1536, 2, "1.50 KB")]
    public void Humanize_CustomDecimals_FormatsCorrectly(long bytes, int decimals, string expected)
    {
        var result = FileSize.Humanize(bytes, decimals);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Humanize_NegativeBytes_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FileSize.Humanize(-1, 1));
    }

    [Theory]
    [InlineData(1024, SizeStandard.Binary, "1.0 KiB")]
    [InlineData(1048576, SizeStandard.Binary, "1.0 MiB")]
    public void Humanize_BinaryStandard_ReturnsIECUnits(long bytes, SizeStandard standard, string expected)
    {
        var result = FileSize.Humanize(bytes, 1, standard);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1000, SizeStandard.SI, "1.0 KB")]
    [InlineData(1000000, SizeStandard.SI, "1.0 MB")]
    public void Humanize_SIStandard_ReturnsMetricUnits(long bytes, SizeStandard standard, string expected)
    {
        var result = FileSize.Humanize(bytes, 1, standard);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Humanize_WithStandard_NegativeBytes_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FileSize.Humanize(-1, 1, SizeStandard.Binary));
    }

    [Fact]
    public void Humanize_WithStandard_ZeroBytes_ReturnsZeroB()
    {
        var result = FileSize.Humanize(0, 1, SizeStandard.SI);

        Assert.Equal("0 B", result);
    }

    [Theory]
    [InlineData("1 KiB", 1024)]
    [InlineData("1 MiB", 1048576)]
    [InlineData("1 KB", 1000)]
    [InlineData("1 MB", 1000000)]
    [InlineData("512 B", 512)]
    public void Parse_ValidInput_ReturnsCorrectBytes(string input, long expected)
    {
        var result = FileSize.Parse(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Parse_EmptyOrWhitespace_ThrowsFormatException(string input)
    {
        Assert.Throws<FormatException>(() => FileSize.Parse(input));
    }

    [Fact]
    public void Parse_NullInput_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => FileSize.Parse(null!));
    }

    [Fact]
    public void Parse_UnknownUnit_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => FileSize.Parse("100 XYZ"));
    }

    [Fact]
    public void Parse_NoUnit_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => FileSize.Parse("100"));
    }

    [Fact]
    public void TryParse_ValidInput_ReturnsTrueAndSetsBytes()
    {
        var success = FileSize.TryParse("1 KiB", out var bytes);

        Assert.True(success);
        Assert.Equal(1024, bytes);
    }

    [Fact]
    public void TryParse_InvalidInput_ReturnsFalseAndZero()
    {
        var success = FileSize.TryParse("garbage", out var bytes);

        Assert.False(success);
        Assert.Equal(0, bytes);
    }

    [Fact]
    public void TryParse_EmptyInput_ReturnsFalse()
    {
        var success = FileSize.TryParse("", out var bytes);

        Assert.False(success);
        Assert.Equal(0, bytes);
    }
}
