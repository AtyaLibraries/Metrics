// <copyright file="CounterExtensions.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
using Atya.Diagnostics.Metrics.Tags;
using Atya.Foundation.Guards;

namespace Atya.Diagnostics.Metrics.Extensions;

/// <summary>
/// Helpful extensions for counter recording.
/// </summary>
public static class CounterExtensions
{
    /// <summary>
    /// Adds one to a long counter.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void Increment(this Counter<long> counter, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        counter.Add(1, Guard.AgainstNull(tags));
    }

    /// <summary>
    /// Adds one to an integer counter.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void Increment(this Counter<int> counter, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        counter.Add(1, Guard.AgainstNull(tags));
    }

    /// <summary>
    /// Adds a positive value to a long counter.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="value">The positive value to add.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void IncrementBy(this Counter<long> counter, long value, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        _ = Guard.AgainstZeroOrNegative(value);
        counter.Add(value, Guard.AgainstNull(tags));
    }

    /// <summary>
    /// Adds a positive value to an integer counter.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="value">The positive value to add.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void IncrementBy(this Counter<int> counter, int value, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        _ = Guard.AgainstZeroOrNegative(value);
        counter.Add(value, Guard.AgainstNull(tags));
    }

    /// <summary>
    /// Adds one to a long counter and appends an outcome tag with the value <see cref="MetricTagValues.Success"/>.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void IncrementSuccess(this Counter<long> counter, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        counter.Add(1, Append(tags, MetricTags.OutcomeSuccess()));
    }

    /// <summary>
    /// Adds one to an integer counter and appends an outcome tag with the value <see cref="MetricTagValues.Success"/>.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void IncrementSuccess(this Counter<int> counter, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        counter.Add(1, Append(tags, MetricTags.OutcomeSuccess()));
    }

    /// <summary>
    /// Adds one to a long counter and appends an outcome tag with the value <see cref="MetricTagValues.Failure"/>.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void IncrementFailure(this Counter<long> counter, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        counter.Add(1, Append(tags, MetricTags.OutcomeFailure()));
    }

    /// <summary>
    /// Adds one to an integer counter and appends an outcome tag with the value <see cref="MetricTagValues.Failure"/>.
    /// </summary>
    /// <param name="counter">The counter to update.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void IncrementFailure(this Counter<int> counter, params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(counter);
        counter.Add(1, Append(tags, MetricTags.OutcomeFailure()));
    }

    private static KeyValuePair<string, object?>[] Append(
        KeyValuePair<string, object?>[] tags,
        KeyValuePair<string, object?> tag)
    {
        _ = Guard.AgainstNull(tags);

        if (tags.Length == 0)
        {
            return new[] { tag };
        }

        var combined = new KeyValuePair<string, object?>[tags.Length + 1];
        Array.Copy(tags, combined, tags.Length);
        combined[^1] = tag;
        return combined;
    }
}
