// <copyright file="MeterAccessor.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
using Atya.Diagnostics.Metrics.Abstractions;
using Atya.Diagnostics.Metrics.Options;
using Atya.Foundation.Guards;
using Microsoft.Extensions.Options;

namespace Atya.Diagnostics.Metrics.Instruments;

/// <summary>
/// Default implementation of <see cref="IMeterAccessor"/>.
/// </summary>
public sealed class MeterAccessor : IMeterAccessor, IDisposable
{
    private readonly Meter _meter;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeterAccessor"/> class.
    /// </summary>
    /// <param name="options">The configured metrics options.</param>
    public MeterAccessor(IOptions<MetricsOptions> options)
    {
        _ = Guard.AgainstNull(options);

        var metricsOptions = Guard.AgainstNull(options.Value);
        var meterName = Guard.AgainstNullOrWhiteSpace(metricsOptions.MeterName, nameof(metricsOptions.MeterName));

        this._meter = new Meter(meterName, metricsOptions.MeterVersion);
    }

    /// <inheritdoc />
    public Meter Meter
    {
        get
        {
            this.ThrowIfDisposed();
            return this._meter;
        }
    }

    /// <inheritdoc />
    public Counter<T> CreateCounter<T>(string name, string? unit = null, string? description = null)
        where T : struct
    {
        this.ThrowIfDisposed();
        return this._meter.CreateCounter<T>(ValidateInstrumentName(name), unit, description);
    }

    /// <inheritdoc />
    public UpDownCounter<T> CreateUpDownCounter<T>(string name, string? unit = null, string? description = null)
        where T : struct
    {
        this.ThrowIfDisposed();
        return this._meter.CreateUpDownCounter<T>(ValidateInstrumentName(name), unit, description);
    }

    /// <inheritdoc />
    public Histogram<T> CreateHistogram<T>(string name, string? unit = null, string? description = null)
        where T : struct
    {
        this.ThrowIfDisposed();
        return this._meter.CreateHistogram<T>(ValidateInstrumentName(name), unit, description);
    }

    /// <inheritdoc />
    public ObservableGauge<T> CreateObservableGauge<T>(string name, Func<T> observeValue, string? unit = null, string? description = null)
        where T : struct
    {
        this.ThrowIfDisposed();
        _ = Guard.AgainstNull(observeValue);
        return this._meter.CreateObservableGauge(ValidateInstrumentName(name), observeValue, unit, description);
    }

    /// <inheritdoc />
    public ObservableCounter<T> CreateObservableCounter<T>(string name, Func<T> observeValue, string? unit = null, string? description = null)
        where T : struct
    {
        this.ThrowIfDisposed();
        _ = Guard.AgainstNull(observeValue);
        return this._meter.CreateObservableCounter(ValidateInstrumentName(name), observeValue, unit, description);
    }

    /// <inheritdoc />
    public ObservableUpDownCounter<T> CreateObservableUpDownCounter<T>(string name, Func<T> observeValue, string? unit = null, string? description = null)
        where T : struct
    {
        this.ThrowIfDisposed();
        _ = Guard.AgainstNull(observeValue);
        return this._meter.CreateObservableUpDownCounter(ValidateInstrumentName(name), observeValue, unit, description);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (this._disposed)
        {
            return;
        }

        this._meter.Dispose();
        this._disposed = true;
    }

    private static string ValidateInstrumentName(string name)
    {
        return Guard.AgainstNullOrWhiteSpace(name, nameof(name));
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(this._disposed, this);
    }
}
