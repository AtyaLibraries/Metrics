// <copyright file="MetricTagNames.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
namespace Atya.Diagnostics.Metrics.Tags;

/// <summary>
/// Well-known metric tag names used by the package.
/// </summary>
public static class MetricTagNames
{
    /// <summary>
    /// Identifies the operation being measured.
    /// </summary>
    public const string Operation = "operation";

    /// <summary>
    /// Identifies the operation result.
    /// </summary>
    public const string Outcome = "outcome";

    /// <summary>
    /// Identifies the tenant associated with the measurement.
    /// </summary>
    public const string TenantId = "tenant.id";

    /// <summary>
    /// Identifies the user associated with the measurement.
    /// </summary>
    public const string UserId = "user.id";

    /// <summary>
    /// Identifies the entity type associated with the measurement.
    /// </summary>
    public const string EntityType = "entity.type";

    /// <summary>
    /// Identifies the entity id associated with the measurement.
    /// </summary>
    public const string EntityId = "entity.id";

    /// <summary>
    /// Identifies the external dependency system.
    /// </summary>
    public const string DependencySystem = "dependency.system";

    /// <summary>
    /// Identifies the error type.
    /// </summary>
    public const string ErrorType = "error.type";

    /// <summary>
    /// Identifies a status value.
    /// </summary>
    public const string Status = "status";

    /// <summary>
    /// Identifies a message type.
    /// </summary>
    public const string MessageType = "message.type";
}
