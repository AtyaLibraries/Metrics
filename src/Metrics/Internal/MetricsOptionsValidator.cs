// <copyright file="MetricsOptionsValidator.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
using Atya.Diagnostics.Metrics.Options;
using Microsoft.Extensions.Options;

namespace Atya.Diagnostics.Metrics.Internal;

internal sealed class MetricsOptionsValidator : IValidateOptions<MetricsOptions>
{
    public ValidateOptionsResult Validate(string? name, MetricsOptions options)
    {
        return options is null
            ? ValidateOptionsResult.Fail("MetricsOptions cannot be null.")
            : string.IsNullOrWhiteSpace(options.MeterName)
            ? ValidateOptionsResult.Fail("MetricsOptions.MeterName cannot be null or whitespace.")
            : options.MeterVersion is not null && string.IsNullOrWhiteSpace(options.MeterVersion)
            ? ValidateOptionsResult.Fail("MetricsOptions.MeterVersion cannot be whitespace when set.")
            : ValidateOptionsResult.Success;
    }
}
