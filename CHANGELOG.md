# Changelog

## [0.2.0] - 2026-03-13

### Added
- `SizeStandard` enum for choosing between Binary (1024/IEC) and SI (1000) unit scales
- `Humanize` overload accepting `SizeStandard` parameter
- `Parse` method for converting human-readable size strings back to bytes
- `TryParse` method as non-throwing variant of Parse

## 0.1.1 (2026-03-10)

- Fix README path in csproj so README displays on nuget.org

## 0.1.0 (2026-03-10)

- Initial release
- `FileSize.Humanize` — convert byte counts to human-readable strings
- Binary (1024-based) units: B, KB, MB, GB, TB, PB, EB
- Configurable decimal places
