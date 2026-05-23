# Metrics

Metrics is the repository for the Atya.Diagnostics.Metrics NuGet package.

| | |
| --- | --- |
| Repository | [https://github.com/aasulyan/Diagnostics](https://github.com/aasulyan/Diagnostics) |
| NuGet | Atya.Diagnostics.Metrics |
| License | MIT |

Provider-agnostic metrics helpers for .NET applications built on System.Diagnostics.Metrics.

## Layout

```text
.
|-- src/Metrics/
|-- tests/Metrics.UnitTests/
|-- samples/Metrics.Samples.Console/
|-- benchmarks/Metrics.Benchmarks/
|-- build/
\-- .github/
```

## Build and test

```bash
./build/build.ps1 -Configuration Release
./build/pack.ps1 -Configuration Release
```

Artifacts land in artifacts/packages/.

## Consumer guidance

Package-specific usage guidance lives in src/Metrics/README.md.

## Public API surface

Public API changes must be tracked in src/Metrics/PublicAPI.Unshipped.txt.

## Release

Publishing is handled by `.github/workflows/publish-nuget.yml`. The workflow restores, audits
dependencies, verifies formatting, builds, tests, packs with package validation, verifies stable
`.nupkg` and `.snupkg` artifacts, publishes to NuGet.org, tags the release, and creates a GitHub
release.
