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

Releases are published by the central publisher, not by a workflow in this repository. After promoting `development` to `master` and tagging the `master` commit, a maintainer runs `gh workflow run publish.yml --repo AtyaLibraries/publisher -f repository=AtyaLibraries/Metrics -f ref=<tag>`; see the publisher RUNBOOK (https://github.com/AtyaLibraries/publisher/blob/main/docs/RUNBOOK.md).
