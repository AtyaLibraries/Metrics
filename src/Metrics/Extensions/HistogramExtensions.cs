// <copyright file="HistogramExtensions.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
using System.Diagnostics;
using Atya.Foundation.Guards;

namespace Atya.Diagnostics.Metrics.Extensions;

/// <summary>
/// Helpful extensions for histogram recording.
/// </summary>
public static class HistogramExtensions
{
    /// <summary>
    /// Records elapsed milliseconds in a double histogram.
    /// </summary>
    /// <param name="histogram">The histogram to update.</param>
    /// <param name="elapsed">The elapsed duration.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void RecordDuration(
        this Histogram<double> histogram,
        TimeSpan elapsed,
        params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(histogram);
        histogram.Record(elapsed.TotalMilliseconds, Guard.AgainstNull(tags));
    }

    /// <summary>
    /// Records rounded elapsed milliseconds in a long histogram.
    /// </summary>
    /// <param name="histogram">The histogram to update.</param>
    /// <param name="elapsed">The elapsed duration.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void RecordDuration(
        this Histogram<long> histogram,
        TimeSpan elapsed,
        params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(histogram);
        histogram.Record((long)Math.Round(elapsed.TotalMilliseconds, MidpointRounding.AwayFromZero), Guard.AgainstNull(tags));
    }

    /// <summary>
    /// Records elapsed stopwatch time in a double histogram.
    /// </summary>
    /// <param name="histogram">The histogram to update.</param>
    /// <param name="stopwatch">The stopwatch containing the elapsed duration.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void RecordDuration(
        this Histogram<double> histogram,
        Stopwatch stopwatch,
        params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(stopwatch);
        histogram.RecordDuration(stopwatch.Elapsed, tags);
    }

    /// <summary>
    /// Records rounded elapsed stopwatch time in a long histogram.
    /// </summary>
    /// <param name="histogram">The histogram to update.</param>
    /// <param name="stopwatch">The stopwatch containing the elapsed duration.</param>
    /// <param name="tags">The measurement tags.</param>
    public static void RecordDuration(
        this Histogram<long> histogram,
        Stopwatch stopwatch,
        params KeyValuePair<string, object?>[] tags)
    {
        _ = Guard.AgainstNull(stopwatch);
        histogram.RecordDuration(stopwatch.Elapsed, tags);
    }
}
