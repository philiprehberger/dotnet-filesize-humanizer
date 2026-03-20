namespace Philiprehberger.FileSizeHumanizer;

/// <summary>
/// Specifies the unit standard for file size formatting.
/// </summary>
public enum SizeStandard
{
    /// <summary>
    /// IEC binary standard using base 1024 (KiB, MiB, GiB, etc.).
    /// </summary>
    Binary,

    /// <summary>
    /// SI decimal standard using base 1000 (KB, MB, GB, etc.).
    /// </summary>
    SI
}

/// <summary>
/// Provides methods to convert byte counts to human-readable strings and parse them back.
/// </summary>
public static class FileSize
{
    private static readonly string[] LegacyUnits = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
    private static readonly string[] BinaryUnits = ["B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB"];
    private static readonly string[] SIUnits = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];

    private static readonly Dictionary<string, (double Factor, bool IsSI)> UnitMap = BuildUnitMap();

    private static Dictionary<string, (double Factor, bool IsSI)> BuildUnitMap()
    {
        var map = new Dictionary<string, (double, bool)>(StringComparer.OrdinalIgnoreCase)
        {
            ["B"] = (1, false),
            ["KIB"] = (1024, false),
            ["MIB"] = (Math.Pow(1024, 2), false),
            ["GIB"] = (Math.Pow(1024, 3), false),
            ["TIB"] = (Math.Pow(1024, 4), false),
            ["PIB"] = (Math.Pow(1024, 5), false),
            ["EIB"] = (Math.Pow(1024, 6), false),
            ["KB"] = (1000, true),
            ["MB"] = (Math.Pow(1000, 2), true),
            ["GB"] = (Math.Pow(1000, 3), true),
            ["TB"] = (Math.Pow(1000, 4), true),
            ["PB"] = (Math.Pow(1000, 5), true),
            ["EB"] = (Math.Pow(1000, 6), true),
        };
        return map;
    }

    /// <summary>
    /// Convert a byte count to a human-readable size string using binary (1024-based) units
    /// with legacy unit names (KB, MB, etc.).
    /// </summary>
    public static string Humanize(long bytes, int decimals = 1)
    {
        if (bytes < 0)
            throw new ArgumentOutOfRangeException(nameof(bytes), "Value must be non-negative.");

        if (bytes == 0)
            return "0 B";

        var index = 0;
        var value = (double)bytes;

        while (value >= 1024 && index < LegacyUnits.Length - 1)
        {
            value /= 1024;
            index++;
        }

        if (index == 0)
            return $"{bytes} B";

        return $"{value.ToString($"F{decimals}")} {LegacyUnits[index]}";
    }

    /// <summary>
    /// Convert a byte count to a human-readable size string using the specified standard.
    /// Binary uses IEC names (KiB, MiB, etc.) with base 1024.
    /// SI uses metric names (KB, MB, etc.) with base 1000.
    /// </summary>
    public static string Humanize(long bytes, int decimals = 1, SizeStandard standard = SizeStandard.Binary)
    {
        if (bytes < 0)
            throw new ArgumentOutOfRangeException(nameof(bytes), "Value must be non-negative.");

        if (bytes == 0)
            return "0 B";

        var units = standard == SizeStandard.Binary ? BinaryUnits : SIUnits;
        var divisor = standard == SizeStandard.Binary ? 1024.0 : 1000.0;

        var index = 0;
        var value = (double)bytes;

        while (value >= divisor && index < units.Length - 1)
        {
            value /= divisor;
            index++;
        }

        if (index == 0)
            return $"{bytes} B";

        return $"{value.ToString($"F{decimals}")} {units[index]}";
    }

    /// <summary>
    /// Parse a human-readable size string back to bytes.
    /// Supports both IEC (KiB, MiB) and SI (KB, MB) unit names.
    /// </summary>
    public static long Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new FormatException("Input string is null or empty.");

        input = input.Trim();

        // Find where the numeric part ends and the unit begins
        var i = 0;
        while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.' || input[i] == '-' || input[i] == '+'))
            i++;

        if (i == 0)
            throw new FormatException($"Cannot parse '{input}' as a file size.");

        var numberPart = input[..i].Trim();
        var unitPart = input[i..].Trim();

        if (!double.TryParse(numberPart, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out var number))
            throw new FormatException($"Cannot parse '{input}' as a file size.");

        if (string.IsNullOrEmpty(unitPart))
            throw new FormatException($"Cannot parse '{input}' as a file size. No unit specified.");

        if (!UnitMap.TryGetValue(unitPart, out var info))
            throw new FormatException($"Unknown unit '{unitPart}' in '{input}'.");

        var result = number * info.Factor;
        return (long)Math.Round(result);
    }

    /// <summary>
    /// Try to parse a human-readable size string back to bytes.
    /// Returns true if successful, false otherwise.
    /// </summary>
    public static bool TryParse(string input, out long bytes)
    {
        try
        {
            bytes = Parse(input);
            return true;
        }
        catch (FormatException)
        {
            bytes = 0;
            return false;
        }
    }
}
