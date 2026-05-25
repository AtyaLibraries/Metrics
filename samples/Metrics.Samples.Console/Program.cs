using System.Diagnostics;
using System.Diagnostics.Metrics;
using Atya.Diagnostics.Metrics.Abstractions;
using Atya.Diagnostics.Metrics.Extensions;
using Atya.Diagnostics.Metrics.Tags;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddAtyaMetrics(options =>
{
    options.MeterName = "Samples.OrderProcessor";
    options.MeterVersion = "1.0.0";
});

services.AddTransient<OrderProcessor>();

using var provider = services.BuildServiceProvider();

var processor = provider.GetRequiredService<OrderProcessor>();
await processor.ProcessAsync("tenant-001", "ORD-2026-001");

Console.WriteLine("Metrics were recorded by Atya.Diagnostics.Metrics.");
Console.WriteLine("Attach a MeterListener or OpenTelemetry later to export them.");

internal sealed class OrderProcessor(IMeterAccessor meterAccessor)
{
    private readonly Counter<long> _requests = meterAccessor.CreateCounter<long>(
            "requests.total",
            description: "Total number of processed requests.");
    private readonly Histogram<double> _duration = meterAccessor.CreateHistogram<double>(
            "operation.duration",
            unit: "ms",
            description: "Duration of operation execution.");

    public async Task ProcessAsync(string tenantId, string orderId)
    {
        _requests.Increment(
            MetricTags.Operation("ProcessOrder"),
            MetricTags.OutcomeStarted(),
            MetricTags.TenantId(tenantId),
            MetricTags.EntityType("Order"),
            MetricTags.EntityId(orderId));

        var stopwatch = Stopwatch.StartNew();
        var outcome = MetricTags.OutcomeSuccess();

        try
        {
            await Task.Delay(75);

            _requests.IncrementSuccess(
                MetricTags.Operation("ProcessOrder"),
                MetricTags.TenantId(tenantId),
                MetricTags.EntityType("Order"),
                MetricTags.EntityId(orderId));
        }
        catch
        {
            outcome = MetricTags.OutcomeFailure();

            _requests.IncrementFailure(
                MetricTags.Operation("ProcessOrder"),
                MetricTags.TenantId(tenantId),
                MetricTags.EntityType("Order"),
                MetricTags.EntityId(orderId));

            throw;
        }
        finally
        {
            stopwatch.Stop();

            _duration.RecordDuration(
                stopwatch.Elapsed,
                MetricTags.Operation("ProcessOrder"),
                outcome,
                MetricTags.TenantId(tenantId),
                MetricTags.EntityType("Order"));
        }
    }
}
