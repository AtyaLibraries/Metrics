using Atya.Diagnostics.Metrics.Instruments;
using Atya.Diagnostics.Metrics.Options;
using Microsoft.Extensions.Options;

namespace Metrics.UnitTests.Instruments;

public sealed class MeterAccessorTests
{
    [Fact]
    public void Constructor_Should_Throw_When_Options_Are_Null()
    {
        IOptions<MetricsOptions> options = null!;

        var action = () => new MeterAccessor(options);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_Should_Throw_When_Options_Value_Is_Null()
    {
        var options = new NullMetricsOptions();

        var action = () => new MeterAccessor(options);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_Should_Throw_When_MeterName_Is_Invalid()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = " " });

        var action = () => new MeterAccessor(options);

        _ = action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_Should_Create_Meter_With_Configured_Name_And_Version()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions
        {
            MeterName = "Samples.OrderProcessor",
            MeterVersion = "1.2.3"
        });

        using var accessor = new MeterAccessor(options);

        _ = accessor.Meter.Name.Should().Be("Samples.OrderProcessor");
        _ = accessor.Meter.Version.Should().Be("1.2.3");
    }

    [Fact]
    public void CreateCounter_Should_Throw_For_Invalid_Name()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var action = () => accessor.CreateCounter<long>(" ");

        _ = action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateCounter_Should_Return_Usable_Instrument()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var counter = accessor.CreateCounter<long>("requests.total", "requests", "Processed requests.");

        _ = counter.Should().NotBeNull();
        _ = counter.Name.Should().Be("requests.total");
        _ = counter.Unit.Should().Be("requests");
        _ = counter.Description.Should().Be("Processed requests.");
    }

    [Fact]
    public void CreateUpDownCounter_Should_Return_Usable_Instrument()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var counter = accessor.CreateUpDownCounter<int>("queue.depth", "items", "Queue depth.");

        _ = counter.Should().NotBeNull();
        _ = counter.Name.Should().Be("queue.depth");
        _ = counter.Unit.Should().Be("items");
        _ = counter.Description.Should().Be("Queue depth.");
    }

    [Fact]
    public void CreateHistogram_Should_Return_Usable_Instrument()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var histogram = accessor.CreateHistogram<double>("operation.duration", "ms", "Operation duration.");

        _ = histogram.Should().NotBeNull();
        _ = histogram.Name.Should().Be("operation.duration");
        _ = histogram.Unit.Should().Be("ms");
        _ = histogram.Description.Should().Be("Operation duration.");
    }

    [Fact]
    public void CreateObservableGauge_Should_Throw_When_Callback_Is_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var action = () => accessor.CreateObservableGauge<int>("queue.depth", null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreateObservableGauge_Should_Return_Usable_Instrument()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var gauge = accessor.CreateObservableGauge("queue.depth", () => 7, "items", "Queue depth.");

        _ = gauge.Should().NotBeNull();
        _ = gauge.Name.Should().Be("queue.depth");
        _ = gauge.Unit.Should().Be("items");
        _ = gauge.Description.Should().Be("Queue depth.");
    }

    [Fact]
    public void CreateObservableCounter_Should_Throw_When_Callback_Is_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var action = () => accessor.CreateObservableCounter<int>("jobs.completed", null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreateObservableCounter_Should_Return_Usable_Instrument()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var counter = accessor.CreateObservableCounter("jobs.completed", () => 7, "jobs", "Completed jobs.");

        _ = counter.Should().NotBeNull();
        _ = counter.Name.Should().Be("jobs.completed");
        _ = counter.Unit.Should().Be("jobs");
        _ = counter.Description.Should().Be("Completed jobs.");
    }

    [Fact]
    public void CreateObservableUpDownCounter_Should_Throw_When_Callback_Is_Null()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var action = () => accessor.CreateObservableUpDownCounter<int>("queue.depth", null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CreateObservableUpDownCounter_Should_Return_Usable_Instrument()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        using var accessor = new MeterAccessor(options);

        var counter = accessor.CreateObservableUpDownCounter("queue.depth", () => 7, "items", "Queue depth.");

        _ = counter.Should().NotBeNull();
        _ = counter.Name.Should().Be("queue.depth");
        _ = counter.Unit.Should().Be("items");
        _ = counter.Description.Should().Be("Queue depth.");
    }

    [Fact]
    public void Members_Should_Throw_After_Dispose()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        var accessor = new MeterAccessor(options);

        accessor.Dispose();

        var meterAction = () => accessor.Meter;
        var counterAction = () => accessor.CreateCounter<long>("requests.total");

        _ = meterAction.Should().Throw<ObjectDisposedException>();
        _ = counterAction.Should().Throw<ObjectDisposedException>();
    }

    [Fact]
    public void Dispose_Should_Be_Idempotent()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MetricsOptions { MeterName = "Samples.OrderProcessor" });
        var accessor = new MeterAccessor(options);

        accessor.Dispose();
        var action = () => accessor.Dispose();

        _ = action.Should().NotThrow();
    }

    private sealed class NullMetricsOptions : IOptions<MetricsOptions>
    {
        public MetricsOptions Value => null!;
    }
}
