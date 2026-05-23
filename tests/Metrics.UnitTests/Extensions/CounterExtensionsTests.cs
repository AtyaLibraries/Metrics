using System.Diagnostics.Metrics;
using Atya.Diagnostics.Metrics.Extensions;
using Atya.Diagnostics.Metrics.Instruments;
using Atya.Diagnostics.Metrics.Options;
using Atya.Diagnostics.Metrics.Tags;
using Metrics.UnitTests.TestUtilities;

namespace Metrics.UnitTests.Extensions;

public sealed class CounterExtensionsTests
{
    [Fact]
    public void Increment_Should_Record_Long_Counter_With_Tags()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<long>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<long>("requests.total");

        counter.Increment(MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(1L);
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Operation && Equals(x.Value, "CreateOrder"));
    }

    [Fact]
    public void Increment_Should_Record_Int_Counter_With_Tags()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<int>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<int>("requests.total");

        counter.Increment(MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(1);
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Operation && Equals(x.Value, "CreateOrder"));
    }

    [Fact]
    public void Increment_Should_Throw_When_Counter_Is_Null()
    {
        Counter<long> counter = null!;

        var action = () => counter.Increment();

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Increment_Should_Throw_When_Tags_Are_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        var counter = accessor.CreateCounter<long>("requests.total");

        var action = () => counter.Increment(null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void IncrementBy_Should_Record_Long_Counter_With_Tags()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<long>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<long>("requests.total");

        counter.IncrementBy(5, MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(5L);
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Operation && Equals(x.Value, "CreateOrder"));
    }

    [Fact]
    public void IncrementBy_Should_Record_Int_Counter_With_Tags()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<int>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<int>("requests.total");

        counter.IncrementBy(5, MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(5);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IncrementBy_Should_Throw_When_Long_Value_Is_Not_Positive(long value)
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        var counter = accessor.CreateCounter<long>("requests.total");

        var action = () => counter.IncrementBy(value);

        _ = action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void IncrementBy_Should_Throw_When_Int_Value_Is_Not_Positive(int value)
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        var counter = accessor.CreateCounter<int>("requests.total");

        var action = () => counter.IncrementBy(value);

        _ = action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void IncrementSuccess_Should_Add_Success_Outcome_Tag()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<long>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<long>("requests.total", description: "Processed requests.");

        counter.IncrementSuccess(MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(1L);
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Operation && Equals(x.Value, "CreateOrder"));
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Outcome && Equals(x.Value, "success"));
    }

    [Fact]
    public void IncrementSuccess_Should_Add_Success_Outcome_Tag_For_Int_Counter()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<int>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<int>("requests.total", description: "Processed requests.");

        counter.IncrementSuccess();

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Value.Should().Be(1);
        _ = collector.Measurements[0].Tags.Should().ContainSingle(x => x.Key == MetricTagNames.Outcome && Equals(x.Value, MetricTagValues.Success));
    }

    [Fact]
    public void IncrementFailure_Should_Add_Failure_Outcome_Tag()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<long>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<long>("requests.total");

        counter.IncrementFailure(MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Outcome && Equals(x.Value, "failure"));
    }

    [Fact]
    public void IncrementFailure_Should_Add_Failure_Outcome_Tag_For_Int_Counter()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        using var collector = new MetricCollector<int>(accessor.Meter, "requests.total");

        var counter = accessor.CreateCounter<int>("requests.total");

        counter.IncrementFailure(MetricTags.Operation("CreateOrder"));

        _ = collector.Measurements.Should().ContainSingle();
        _ = collector.Measurements[0].Tags.Should().Contain(x => x.Key == MetricTagNames.Outcome && Equals(x.Value, MetricTagValues.Failure));
    }

    [Fact]
    public void IncrementSuccess_Should_Throw_When_Tags_Are_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);
        var counter = accessor.CreateCounter<long>("requests.total");

        var action = () => counter.IncrementSuccess(null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }
}
