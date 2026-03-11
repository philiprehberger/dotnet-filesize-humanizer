namespace Philiprehberger.FileSizeHumanizer;

public static class FileSize
{
    private static readonly string[] Units = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];

    public static string Humanize(long bytes, int decimals = 1)
    {
        if (bytes < 0)
            throw new ArgumentOutOfRangeException(nameof(bytes), "Value must be non-negative.");

        if (bytes == 0)
            return "0 B";

        var index = 0;
        var value = (double)bytes;

        while (value >= 1024 && index < Units.Length - 1)
        {
            value /= 1024;
            index++;
        }

        if (index == 0)
            return $"{bytes} B";

        return $"{value.ToString($"F{decimals}")} {Units[index]}";
    }
}
