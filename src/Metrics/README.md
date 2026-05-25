# Atya.Diagnostics.Metrics

Provider-agnostic metrics helpers for .NET applications built on `System.Diagnostics.Metrics`.

## What this package provides

- `IMeterAccessor` / `MeterAccessor` — managed `Meter` wrapper with DI support
- `MetricsOptions` — configuration model for service name, version, and `Meter` name
- `AddAtyaMetrics(...)` — one-call DI registration
- `CounterExtensions` / `HistogramExtensions` — convenience extensions for instrument creation
- `MetricTagNames` / `MetricTags` — well-known tag name constants and tag helpers

## What this package does not provide

This package intentionally does **not** configure:
- OpenTelemetry SDK or exporters
- Prometheus / OTLP / Graphite exporters
- Serilog or any logging provider
- ASP.NET Core middleware
- Vendor-specific integrations

## Installation

```bash
dotnet add package Atya.Diagnostics.Metrics
```

## Basic usage

```csharp
using System.Diagnostics.Metrics;
using Atya.Diagnostics.Metrics.Abstractions;
using Atya.Diagnostics.Metrics.Extensions;
using Atya.Diagnostics.Metrics.Tags;
using Microsoft.Extensions.DependencyInjection;

services.AddAtyaMetrics(options =>
{
    options.MeterName = "Orders.Service";
    options.MeterVersion = "1.0.0";
});
```

```csharp
public class OrderProcessor
{
    private readonly Counter<long> _ordersProcessed;

    public OrderProcessor(IMeterAccessor meterAccessor)
    {
        _ordersProcessed = meterAccessor.CreateCounter<long>("orders.processed");
    }

    public void Process(int orderId)
    {
        _ordersProcessed.IncrementSuccess(MetricTags.Operation("ProcessOrder"));
        // ...
    }
}
```

## Common tags

Use `MetricTags` and `MetricTagValues` to keep tag names and outcome values stable:

```csharp
counter.IncrementFailure(
    MetricTags.Operation("ProcessOrder"),
    MetricTags.TenantId("tenant-001"));
```

## Validation

`AddAtyaMetrics` validates that `MeterName` is not null or whitespace, and `MeterAccessor`
validates instrument names and observable callbacks before creating instruments.
