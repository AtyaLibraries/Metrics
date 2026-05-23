using Atya.Diagnostics.Metrics.Abstractions;
using Atya.Diagnostics.Metrics.DependencyInjection;
using Atya.Diagnostics.Metrics.Internal;
using Atya.Diagnostics.Metrics.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Metrics.UnitTests.DependencyInjection;

public sealed class MetricsServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAtyaMetrics_Should_Throw_When_Services_Is_Null()
    {
        ServiceCollection services = null!;

        var action = () => services.AddAtyaMetrics();

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddAtyaMetrics_Should_Register_Default_Options()
    {
        var services = new ServiceCollection();

        _ = services.AddAtyaMetrics();

        using var provider = services.BuildServiceProvider();

        var accessor = provider.GetRequiredService<IMeterAccessor>();
        var options = provider.GetRequiredService<IOptions<MetricsOptions>>();

        _ = accessor.Meter.Name.Should().Be("Atya");
        _ = accessor.Meter.Version.Should().BeNull();
        _ = options.Value.MeterName.Should().Be("Atya");
    }

    [Fact]
    public void AddAtyaMetrics_Should_Register_Services()
    {
        var services = new ServiceCollection();

        _ = services.AddAtyaMetrics(options =>
        {
            options.MeterName = "Samples.OrderProcessor";
            options.MeterVersion = "1.0.0";
        });

        using var provider = services.BuildServiceProvider();

        var accessor = provider.GetRequiredService<IMeterAccessor>();
        var options = provider.GetRequiredService<IOptions<MetricsOptions>>();

        _ = accessor.Meter.Name.Should().Be("Samples.OrderProcessor");
        _ = accessor.Meter.Version.Should().Be("1.0.0");
        _ = options.Value.MeterName.Should().Be("Samples.OrderProcessor");
    }

    [Fact]
    public void AddAtyaMetrics_Should_Be_Idempotent()
    {
        var services = new ServiceCollection();

        _ = services.AddAtyaMetrics(options => options.MeterName = "Samples.OrderProcessor");
        _ = services.AddAtyaMetrics();

        using var provider = services.BuildServiceProvider();
        var accessors = provider.GetServices<IMeterAccessor>();

        _ = accessors.Should().ContainSingle();
    }

    [Fact]
    public void AddAtyaMetrics_Should_Validate_Options_When_Resolved()
    {
        var services = new ServiceCollection();
        _ = services.AddAtyaMetrics(options => options.MeterName = " ");

        using var provider = services.BuildServiceProvider();

        var action = () => provider.GetRequiredService<IMeterAccessor>();

        _ = action.Should().Throw<OptionsValidationException>()
            .WithMessage("*MetricsOptions.MeterName cannot be null or whitespace.*");
    }

    [Fact]
    public void AddAtyaMetrics_Should_Validate_MeterVersion_When_Resolved()
    {
        var services = new ServiceCollection();
        _ = services.AddAtyaMetrics(options => options.MeterVersion = " ");

        using var provider = services.BuildServiceProvider();

        var action = () => provider.GetRequiredService<IMeterAccessor>();

        _ = action.Should().Throw<OptionsValidationException>()
            .WithMessage("*MetricsOptions.MeterVersion cannot be whitespace when set.*");
    }

    [Fact]
    public void MetricsOptionsValidator_Should_Fail_When_Options_Are_Null()
    {
        var validator = new MetricsOptionsValidator();

        var result = validator.Validate(null, null!);

        _ = result.Failed.Should().BeTrue();
        _ = result.Failures.Should().ContainSingle("MetricsOptions cannot be null.");
    }

    [Fact]
    public void MetricsOptionsValidator_Should_Succeed_For_Default_Options()
    {
        var validator = new MetricsOptionsValidator();

        var result = validator.Validate(null, new MetricsOptions());

        _ = result.Succeeded.Should().BeTrue();
    }
}
