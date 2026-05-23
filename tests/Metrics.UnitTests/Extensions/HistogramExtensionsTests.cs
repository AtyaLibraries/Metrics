using System.Diagnostics;
using System.Diagnostics.Metrics;
using Atya.Diagnostics.Metrics.Extensions;
using Atya.Diagnostics.Metrics.Instruments;
using Atya.Diagnostics.Metrics.Options;
using Atya.Diagnostics.Metrics.Tags;
using Metrics.UnitTests.TestUtilities;

namespace Metrics.UnitTests.Extensions;

public sealed class HistogramExtensionsTests
{
    [Fact]
    public void RecordDuration_Should_Record_TotalMilliseconds_And_Tags()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<double>(accessor.Meter, "operation.duration");

        var histogram = accessor.CreateHistogram<double>("operation.duration", "ms", "Operation duration.");

        histogram.RecordDuration(
            TimeSpan.FromMilliseconds(125.5),
            MetricTags.Operation("ProcessOrder"),
            MetricTags.Outcome("success"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(125.5d);
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Operation && Equals(x.Value, "ProcessOrder"));
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Outcome && Equals(x.Value, "success"));
    }

    [Fact]
    public void RecordDuration_Should_Throw_When_Histogram_Is_Null()
    {
        Histogram<double> histogram = null!;

        var action = () => histogram.RecordDuration(TimeSpan.FromMilliseconds(10));

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RecordDuration_Should_Record_Rounded_TotalMilliseconds_For_Long_Histogram()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<long>(accessor.Meter, "operation.duration");

        var histogram = accessor.CreateHistogram<long>("operation.duration", "ms", "Operation duration.");

        histogram.RecordDuration(
            TimeSpan.FromMilliseconds(125.5),
            MetricTags.Operation("ProcessOrder"),
            MetricTags.OutcomeSuccess());

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(126L);
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Outcome && Equals(x.Value, MetricTagValues.Success));
    }

    [Fact]
    public void RecordDuration_Should_Record_Stopwatch_For_Double_Histogram()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<double>(accessor.Meter, "operation.duration");

        var histogram = accessor.CreateHistogram<double>("operation.duration", "ms", "Operation duration.");
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Stop();

        histogram.RecordDuration(stopwatch, MetricTags.Operation("ProcessOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void RecordDuration_Should_Record_Stopwatch_For_Long_Histogram()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<long>(accessor.Meter, "operation.duration");

        var histogram = accessor.CreateHistogram<long>("operation.duration", "ms", "Operation duration.");
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Stop();

        histogram.RecordDuration(stopwatch, MetricTags.Operation("ProcessOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void RecordDuration_Should_Throw_When_Long_Histogram_Is_Null()
    {
        Histogram<long> histogram = null!;

        var action = () => histogram.RecordDuration(TimeSpan.FromMilliseconds(10));

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RecordDuration_Should_Throw_When_Stopwatch_Is_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        var histogram = accessor.CreateHistogram<double>("operation.duration");
        Stopwatch stopwatch = null!;

        var action = () => histogram.RecordDuration(stopwatch);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RecordDuration_Should_Throw_When_Tags_Are_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        var histogram = accessor.CreateHistogram<double>("operation.duration");

        var action = () => histogram.RecordDuration(TimeSpan.FromMilliseconds(10), null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }
}
