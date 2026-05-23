// <copyright file="MetricTagValues.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
namespace Atya.Diagnostics.Metrics.Tags;

/// <summary>
/// Well-known metric tag values used by the package.
/// </summary>
public static class MetricTagValues
{
    /// <summary>
    /// Indicates that an operation has started.
    /// </summary>
    public const string Started = "started";

    /// <summary>
    /// Indicates that an operation completed successfully.
    /// </summary>
    public const string Success = "success";

    /// <summary>
    /// Indicates that an operation failed.
    /// </summary>
    public const string Failure = "failure";
}
