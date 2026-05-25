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
        if (options is null)
        {
            return ValidateOptionsResult.Fail("MetricsOptions cannot be null.");
        }

        var failures = new List<string>();

        if (string.IsNullOrWhiteSpace(options.MeterName))
        {
            failures.Add("MetricsOptions.MeterName cannot be null or whitespace.");
        }

        if (options.MeterVersion is not null && string.IsNullOrWhiteSpace(options.MeterVersion))
        {
            failures.Add("MetricsOptions.MeterVersion cannot be whitespace when set.");
        }

        return failures.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(failures);
    }
}
