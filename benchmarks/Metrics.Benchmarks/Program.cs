using System.Diagnostics.Metrics;
using Atya.Diagnostics.Metrics.Extensions;
using Atya.Diagnostics.Metrics.Tags;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Metrics.Benchmarks;

/// <summary>
/// Runs metrics package benchmarks.
/// </summary>
public static class Program
{
    /// <summary>
    /// Runs the configured benchmark suite.
    /// </summary>
    /// <param name="args">BenchmarkDotNet command-line arguments.</param>
    public static void Main(string[] args)
    {
        _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}

/// <summary>
/// Measures the overhead of the package's convenience APIs against direct
/// <see cref="System.Diagnostics.Metrics"/> calls.
/// </summary>
[MemoryDiagnoser]
public class MetricsRecordingBenchmarks : IDisposable
{
    private static readonly TimeSpan Elapsed = TimeSpan.FromMilliseconds(12.75);

    private Meter? _meter;
    private Counter<long> _counter = null!;
    private Histogram<double> _histogram = null!;
    private string _operationName = null!;
    private KeyValuePair<string, object?>[] _tags = null!;

    /// <summary>
    /// Creates reusable instruments and tags for benchmark operations.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        this._meter = new Meter("Atya.Diagnostics.Metrics.Benchmarks", "1.0.0");
        this._counter = this._meter.CreateCounter<long>("requests.total");
        this._histogram = this._meter.CreateHistogram<double>("operation.duration", "ms");
        this._operationName = "ProcessOrder";
        this._tags =
        [
            MetricTags.Operation(this._operationName),
            MetricTags.TenantId("tenant-001"),
            MetricTags.EntityType("Order"),
        ];
    }

    /// <summary>
    /// Disposes benchmark instruments.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        this.Dispose();
    }

    /// <summary>
    /// Disposes benchmark resources.
    /// </summary>
    public void Dispose()
    {
        this._meter?.Dispose();
        this._meter = null;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Baseline direct counter recording.
    /// </summary>
    [Benchmark(Baseline = true)]
    public void CounterAdd()
    {
        this._counter.Add(1, this._tags);
    }

    /// <summary>
    /// Records via the package's increment convenience method.
    /// </summary>
    [Benchmark]
    public void CounterIncrement()
    {
        this._counter.Increment(this._tags);
    }

    /// <summary>
    /// Records a successful counter outcome.
    /// </summary>
    [Benchmark]
    public void CounterIncrementSuccess()
    {
        this._counter.IncrementSuccess(this._tags);
    }

    /// <summary>
    /// Records a duration through the package's histogram helper.
    /// </summary>
    [Benchmark]
    public void HistogramRecordDuration()
    {
        this._histogram.RecordDuration(Elapsed, this._tags);
    }

    /// <summary>
    /// Creates a common tag helper value.
    /// </summary>
    /// <returns>The created tag.</returns>
    [Benchmark]
    public KeyValuePair<string, object?> CreateOperationTag()
    {
        return MetricTags.Operation(this._operationName);
    }
}
