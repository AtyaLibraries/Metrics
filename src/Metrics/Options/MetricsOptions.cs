// <copyright file="MetricsOptions.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
namespace Atya.Diagnostics.Metrics.Options;

/// <summary>
/// Configures the shared <see cref="Meter"/> used by the package.
/// </summary>
public sealed class MetricsOptions
{
    /// <summary>
    /// Gets or sets the meter name.
    /// </summary>
    public string MeterName { get; set; } = "Atya";

    /// <summary>
    /// Gets or sets the optional meter version.
    /// </summary>
    public string? MeterVersion { get; set; }
}
