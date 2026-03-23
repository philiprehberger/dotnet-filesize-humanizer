# Changelog

## 0.2.5 (2026-03-22)

- Add dates to changelog entries
- Normalize changelog format

## 0.2.4 (2026-03-21)

- Align README and csproj descriptions

## 0.2.3 (2026-03-16)

- Add Development section to README
- Add GenerateDocumentationFile and RepositoryType to .csproj

## 0.2.0 (2026-03-13)

- Add `SizeStandard` enum for choosing between Binary (1024/IEC) and SI (1000) unit scales
- Add `Humanize` overload accepting `SizeStandard` parameter
- Add `Parse` method for converting human-readable size strings back to bytes
- Add `TryParse` method as non-throwing variant of Parse

## 0.1.1 (2026-03-10)

- Fix README path in csproj so README displays on nuget.org

## 0.1.0 (2026-03-10)

- Initial release
- `FileSize.Humanize` — convert byte counts to human-readable strings
- Binary (1024-based) units: B, KB, MB, GB, TB, PB, EB
- Configurable decimal places
