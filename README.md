# Philiprehberger.FileSizeHumanizer

Convert raw byte counts into readable file size strings like "1.5 KB" or "3.2 MB".

## Install

```bash
dotnet add package Philiprehberger.FileSizeHumanizer
```

## Usage

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

## API

### `FileSize`

| Method | Description |
|--------|-------------|
| `Humanize(long bytes, int decimals = 1)` | Convert a byte count to a human-readable size string using binary (1024-based) units |

**Supported units:** B, KB, MB, GB, TB, PB, EB

## License

MIT
