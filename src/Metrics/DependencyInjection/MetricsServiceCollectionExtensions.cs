// <copyright file="MetricsServiceCollectionExtensions.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
using Atya.Diagnostics.Metrics.Abstractions;
using Atya.Diagnostics.Metrics.Instruments;
using Atya.Diagnostics.Metrics.Internal;
using Atya.Diagnostics.Metrics.Options;
using Atya.Foundation.Guards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Atya.Diagnostics.Metrics.DependencyInjection;

/// <summary>
/// Registers Atya metrics services.
/// </summary>
public static class MetricsServiceCollectionExtensions
{
    /// <summary>
    /// Registers the shared metrics services used by Atya metrics helpers.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <param name="configure">An optional metrics options callback.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddAtyaMetrics(
        this IServiceCollection services,
        Action<MetricsOptions>? configure = null)
    {
        _ = Guard.AgainstNull(services);

        _ = services.AddOptions<MetricsOptions>();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<MetricsOptions>, MetricsOptionsValidator>());

        if (configure is not null)
        {
            _ = services.Configure(configure);
        }

        services.TryAddSingleton<IMeterAccessor, MeterAccessor>();

        return services;
    }
}
