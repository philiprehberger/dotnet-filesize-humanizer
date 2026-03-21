# Philiprehberger.FileSizeHumanizer

[![CI](https://github.com/philiprehberger/dotnet-filesize-humanizer/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-filesize-humanizer/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.FileSizeHumanizer.svg)](https://www.nuget.org/packages/Philiprehberger.FileSizeHumanizer)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-filesize-humanizer)](LICENSE)

Convert byte counts into human-readable file size strings and parse them back to bytes.

## Installation

```bash
dotnet add package Philiprehberger.FileSizeHumanizer
```

## Usage

### Humanize (legacy units)

```csharp
using Philiprehberger.FileSizeHumanizer;

FileSize.Humanize(0);              // "0 B"
FileSize.Humanize(512);            // "512 B"
FileSize.Humanize(1024);           // "1.0 KB"
FileSize.Humanize(1_536);          // "1.5 KB"
FileSize.Humanize(3_355_443);      // "3.2 MB"
FileSize.Humanize(1_073_741_824);  // "1.0 GB"
FileSize.Humanize(2_199_023_255_552L); // "2.0 TB"

// Control decimal places
FileSize.Humanize(1_500_000, decimals: 2); // "1.43 MB"
FileSize.Humanize(1_500_000, decimals: 0); // "1 MB"
```

### SI and Binary (IEC) standards

```csharp
// Binary (IEC) — base 1024, units: B, KiB, MiB, GiB, TiB, PiB, EiB
FileSize.Humanize(1024, standard: SizeStandard.Binary);        // "1.0 KiB"
FileSize.Humanize(1_048_576, standard: SizeStandard.Binary);   // "1.0 MiB"

// SI — base 1000, units: B, KB, MB, GB, TB, PB, EB
FileSize.Humanize(1000, standard: SizeStandard.SI);            // "1.0 KB"
FileSize.Humanize(1_000_000, standard: SizeStandard.SI);       // "1.0 MB"

// With decimal control
FileSize.Humanize(1_500_000, decimals: 2, standard: SizeStandard.SI); // "1.50 MB"
```

### Parse and TryParse

```csharp
// Parse strings back to bytes (supports both SI and IEC units)
FileSize.Parse("3.2 MB");   // 3200000
FileSize.Parse("1.5 GiB");  // 1610612736
FileSize.Parse("1024 B");   // 1024

// Whitespace between number and unit is handled
FileSize.Parse("3.2MB");    // 3200000

// TryParse — non-throwing variant
if (FileSize.TryParse("2.5 GB", out var bytes))
{
    Console.WriteLine(bytes); // 2500000000
}

// Returns false for invalid input
FileSize.TryParse("not a size", out _); // false
```

## API

### `SizeStandard` enum

| Value | Base | Units |
|-------|------|-------|
| `Binary` | 1024 | B, KiB, MiB, GiB, TiB, PiB, EiB |
| `SI` | 1000 | B, KB, MB, GB, TB, PB, EB |

### `FileSize`

| Method | Description |
|--------|-------------|
| `Humanize(long bytes, int decimals = 1)` | Convert a byte count to a human-readable string using binary (1024) scale with legacy unit names (KB, MB, etc.) |
| `Humanize(long bytes, int decimals = 1, SizeStandard standard = SizeStandard.Binary)` | Convert a byte count using the specified standard with proper IEC or SI unit names |
| `Parse(string input)` | Parse a human-readable size string back to bytes. Throws `FormatException` on invalid input. |
| `TryParse(string input, out long bytes)` | Non-throwing variant of `Parse`. Returns `true` on success. |

## Development

```bash
dotnet build src/Philiprehberger.FileSizeHumanizer.csproj --configuration Release
```

## License

MIT
