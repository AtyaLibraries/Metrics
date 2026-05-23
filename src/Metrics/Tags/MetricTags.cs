// <copyright file="MetricTags.cs" company="Atya">
// Copyright (c) Atya. All rights reserved.
// </copyright>
using Atya.Foundation.Guards;

namespace Atya.Diagnostics.Metrics.Tags;

/// <summary>
/// Creates common metric tags using stable tag names.
/// </summary>
public static class MetricTags
{
    /// <summary>
    /// Creates an operation tag.
    /// </summary>
    /// <param name="operation">The operation name.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> Operation(string operation)
    {
        return CreateRequiredStringTag(MetricTagNames.Operation, operation, nameof(operation));
    }

    /// <summary>
    /// Creates an outcome tag.
    /// </summary>
    /// <param name="outcome">The outcome value.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> Outcome(string outcome)
    {
        return CreateRequiredStringTag(MetricTagNames.Outcome, outcome, nameof(outcome));
    }

    /// <summary>
    /// Creates an outcome tag with the value <see cref="MetricTagValues.Started"/>.
    /// </summary>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> OutcomeStarted()
    {
        return Outcome(MetricTagValues.Started);
    }

    /// <summary>
    /// Creates an outcome tag with the value <see cref="MetricTagValues.Success"/>.
    /// </summary>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> OutcomeSuccess()
    {
        return Outcome(MetricTagValues.Success);
    }

    /// <summary>
    /// Creates an outcome tag with the value <see cref="MetricTagValues.Failure"/>.
    /// </summary>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> OutcomeFailure()
    {
        return Outcome(MetricTagValues.Failure);
    }

    /// <summary>
    /// Creates a tenant id tag.
    /// </summary>
    /// <param name="tenantId">The tenant id.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> TenantId(string tenantId)
    {
        return CreateRequiredStringTag(MetricTagNames.TenantId, tenantId, nameof(tenantId));
    }

    /// <summary>
    /// Creates a user id tag.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> UserId(string userId)
    {
        return CreateRequiredStringTag(MetricTagNames.UserId, userId, nameof(userId));
    }

    /// <summary>
    /// Creates an entity type tag.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> EntityType(string entityType)
    {
        return CreateRequiredStringTag(MetricTagNames.EntityType, entityType, nameof(entityType));
    }

    /// <summary>
    /// Creates an entity id tag.
    /// </summary>
    /// <param name="entityId">The entity id.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> EntityId(object entityId)
    {
        _ = Guard.AgainstNull(entityId);
        return new KeyValuePair<string, object?>(MetricTagNames.EntityId, entityId);
    }

    /// <summary>
    /// Creates a dependency system tag.
    /// </summary>
    /// <param name="dependencySystem">The dependency system name.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> DependencySystem(string dependencySystem)
    {
        return CreateRequiredStringTag(MetricTagNames.DependencySystem, dependencySystem, nameof(dependencySystem));
    }

    /// <summary>
    /// Creates an error type tag.
    /// </summary>
    /// <param name="errorType">The error type.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> ErrorType(string errorType)
    {
        return CreateRequiredStringTag(MetricTagNames.ErrorType, errorType, nameof(errorType));
    }

    /// <summary>
    /// Creates a status tag.
    /// </summary>
    /// <param name="status">The status value.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> Status(string status)
    {
        return CreateRequiredStringTag(MetricTagNames.Status, status, nameof(status));
    }

    /// <summary>
    /// Creates a message type tag.
    /// </summary>
    /// <param name="messageType">The message type.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> MessageType(string messageType)
    {
        return CreateRequiredStringTag(MetricTagNames.MessageType, messageType, nameof(messageType));
    }

    /// <summary>
    /// Creates a custom metric tag.
    /// </summary>
    /// <param name="name">The tag name.</param>
    /// <param name="value">The tag value.</param>
    /// <returns>The created tag.</returns>
    public static KeyValuePair<string, object?> Create(string name, object? value)
    {
        var tagName = Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        return new KeyValuePair<string, object?>(tagName, value);
    }

    private static KeyValuePair<string, object?> CreateRequiredStringTag(string tagName, string value, string paramName)
    {
        var tagValue = Guard.AgainstNullOrWhiteSpace(value, paramName);
        return new KeyValuePair<string, object?>(tagName, tagValue);
    }
}
