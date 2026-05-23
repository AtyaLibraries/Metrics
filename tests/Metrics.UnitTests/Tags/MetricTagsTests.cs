using Atya.Diagnostics.Metrics.Tags;

namespace Metrics.UnitTests.Tags;

public sealed class MetricTagsTests
{
    [Fact]
    public void Operation_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.Operation("CreateOrder");

        _ = tag.Key.Should().Be(MetricTagNames.Operation);
        _ = tag.Value.Should().Be("CreateOrder");
    }

    [Fact]
    public void Outcome_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.Outcome("custom");

        _ = tag.Key.Should().Be(MetricTagNames.Outcome);
        _ = tag.Value.Should().Be("custom");
    }

    [Fact]
    public void OutcomeStarted_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.OutcomeStarted();

        _ = tag.Key.Should().Be(MetricTagNames.Outcome);
        _ = tag.Value.Should().Be(MetricTagValues.Started);
    }

    [Fact]
    public void OutcomeSuccess_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.OutcomeSuccess();

        _ = tag.Key.Should().Be(MetricTagNames.Outcome);
        _ = tag.Value.Should().Be(MetricTagValues.Success);
    }

    [Fact]
    public void OutcomeFailure_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.OutcomeFailure();

        _ = tag.Key.Should().Be(MetricTagNames.Outcome);
        _ = tag.Value.Should().Be(MetricTagValues.Failure);
    }

    [Fact]
    public void UserId_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.UserId("user-001");

        _ = tag.Key.Should().Be(MetricTagNames.UserId);
        _ = tag.Value.Should().Be("user-001");
    }

    [Fact]
    public void EntityType_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.EntityType("Order");

        _ = tag.Key.Should().Be(MetricTagNames.EntityType);
        _ = tag.Value.Should().Be("Order");
    }

    [Fact]
    public void EntityId_Should_Use_Stable_Tag_Name()
    {
        var tag = MetricTags.EntityId(42);

        _ = tag.Key.Should().Be(MetricTagNames.EntityId);
        _ = tag.Value.Should().Be(42);
    }

    [Fact]
    public void DependencySystem_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.DependencySystem("postgresql");

        _ = tag.Key.Should().Be(MetricTagNames.DependencySystem);
        _ = tag.Value.Should().Be("postgresql");
    }

    [Fact]
    public void ErrorType_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.ErrorType("TimeoutException");

        _ = tag.Key.Should().Be(MetricTagNames.ErrorType);
        _ = tag.Value.Should().Be("TimeoutException");
    }

    [Fact]
    public void Status_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.Status("completed");

        _ = tag.Key.Should().Be(MetricTagNames.Status);
        _ = tag.Value.Should().Be("completed");
    }

    [Fact]
    public void MessageType_Should_Return_Expected_Tag()
    {
        var tag = MetricTags.MessageType("OrderSubmitted");

        _ = tag.Key.Should().Be(MetricTagNames.MessageType);
        _ = tag.Value.Should().Be("OrderSubmitted");
    }

    [Fact]
    public void TenantId_Should_Throw_When_Value_Is_Whitespace()
    {
        var action = () => MetricTags.TenantId(" ");

        _ = action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void EntityId_Should_Throw_When_Value_Is_Null()
    {
        var action = () => MetricTags.EntityId(null!);

        _ = action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_Should_Allow_Custom_Tag_Name()
    {
        var tag = MetricTags.Create("custom.dimension", "value");

        _ = tag.Key.Should().Be("custom.dimension");
        _ = tag.Value.Should().Be("value");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Should_Throw_When_Name_Is_Invalid(string? name)
    {
        var action = () => MetricTags.Create(name!, "value");

        _ = action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Required_String_Tag_Helpers_Should_Throw_When_Value_Is_Invalid(string? value)
    {
        var actions = new Action[]
        {
            () => MetricTags.Operation(value!),
            () => MetricTags.Outcome(value!),
            () => MetricTags.TenantId(value!),
            () => MetricTags.UserId(value!),
            () => MetricTags.EntityType(value!),
            () => MetricTags.DependencySystem(value!),
            () => MetricTags.ErrorType(value!),
            () => MetricTags.Status(value!),
            () => MetricTags.MessageType(value!),
        };

        foreach (var action in actions)
        {
            _ = action.Should().Throw<ArgumentException>();
        }
    }
}
