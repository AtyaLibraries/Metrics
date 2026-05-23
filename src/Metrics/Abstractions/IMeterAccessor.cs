// <copyright file="IMeterAccessor.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
namespace Atya.Diagnostics.Metrics.Abstractions;

/// <summary>
/// Provides access to a configured <see cref="Meter"/> instance and creates instruments consistently.
/// </summary>
public interface IMeterAccessor
{
    /// <summary>
    /// Gets the configured <see cref="Meter"/> instance.
    /// </summary>
    public Meter Meter { get; }

    /// <summary>
    /// Creates a <see cref="Counter{T}"/> instrument.
    /// </summary>
    /// <typeparam name="T">The counter measurement type.</typeparam>
    /// <param name="name">The instrument name.</param>
    /// <param name="unit">The optional instrument unit.</param>
    /// <param name="description">The optional instrument description.</param>
    /// <returns>The created counter.</returns>
    public Counter<T> CreateCounter<T>(string name, string? unit = null, string? description = null)
        where T : struct;

    /// <summary>
    /// Creates an <see cref="UpDownCounter{T}"/> instrument.
    /// </summary>
    /// <typeparam name="T">The up/down counter measurement type.</typeparam>
    /// <param name="name">The instrument name.</param>
    /// <param name="unit">The optional instrument unit.</param>
    /// <param name="description">The optional instrument description.</param>
    /// <returns>The created up/down counter.</returns>
    public UpDownCounter<T> CreateUpDownCounter<T>(string name, string? unit = null, string? description = null)
        where T : struct;

    /// <summary>
    /// Creates a <see cref="Histogram{T}"/> instrument.
    /// </summary>
    /// <typeparam name="T">The histogram measurement type.</typeparam>
    /// <param name="name">The instrument name.</param>
    /// <param name="unit">The optional instrument unit.</param>
    /// <param name="description">The optional instrument description.</param>
    /// <returns>The created histogram.</returns>
    public Histogram<T> CreateHistogram<T>(string name, string? unit = null, string? description = null)
        where T : struct;

    /// <summary>
    /// Creates an <see cref="ObservableGauge{T}"/> instrument.
    /// </summary>
    /// <typeparam name="T">The observable gauge measurement type.</typeparam>
    /// <param name="name">The instrument name.</param>
    /// <param name="observeValue">The callback that observes the current value.</param>
    /// <param name="unit">The optional instrument unit.</param>
    /// <param name="description">The optional instrument description.</param>
    /// <returns>The created observable gauge.</returns>
    public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<T> observeValue, string? unit = null, string? description = null)
        where T : struct;

    /// <summary>
    /// Creates an <see cref="ObservableCounter{T}"/> instrument.
    /// </summary>
    /// <typeparam name="T">The observable counter measurement type.</typeparam>
    /// <param name="name">The instrument name.</param>
    /// <param name="observeValue">The callback that observes the current value.</param>
    /// <param name="unit">The optional instrument unit.</param>
    /// <param name="description">The optional instrument description.</param>
    /// <returns>The created observable counter.</returns>
    public ObservableCounter<T> CreateObservableCounter<T>(string name, Func<T> observeValue, string? unit = null, string? description = null)
        where T : struct;

    /// <summary>
    /// Creates an <see cref="ObservableUpDownCounter{T}"/> instrument.
    /// </summary>
    /// <typeparam name="T">The observable up/down counter measurement type.</typeparam>
    /// <param name="name">The instrument name.</param>
    /// <param name="observeValue">The callback that observes the current value.</param>
    /// <param name="unit">The optional instrument unit.</param>
    /// <param name="description">The optional instrument description.</param>
    /// <returns>The created observable up/down counter.</returns>
    public ObservableUpDownCounter<T> CreateObservableUpDownCounter<T>(string name, Func<T> observeValue, string? unit = null, string? description = null)
        where T : struct;
}
