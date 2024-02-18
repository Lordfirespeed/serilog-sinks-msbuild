/*
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using Microsoft.Build.Framework;
using Serilog.Events;

namespace Serilog.Sinks.MSBuild.Extensions;

/// <summary>
/// Contains extension methods that add functionality to <see cref="LogEventLevel"/>.
/// </summary>
public static class LogEventLevelExtensions
{
    /// <summary>
    /// Get the corresponding <see cref="MSBuildLogEventCategory"/> and <see cref="MessageImportance"/> for this
    /// <see cref="LogEventLevel"/>.
    /// </summary>
    /// <param name="level">The <see cref="LogEventLevel"/> to map.</param>
    /// <returns>The appropriate <see cref="MSBuildLogEventCategory"/> and <see cref="MessageImportance"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="level"/> is not a recognised <see cref="LogEventLevel"/>.</exception>
    public static (MSBuildLogEventCategory Category, MessageImportance? Importance) ToMSBuildCategory(this LogEventLevel level) => level switch {
        LogEventLevel.Verbose => (MSBuildLogEventCategory.Message, MessageImportance.Low),
        LogEventLevel.Debug => (MSBuildLogEventCategory.Message, MessageImportance.Normal),
        LogEventLevel.Information => (MSBuildLogEventCategory.Message, MessageImportance.High),
        LogEventLevel.Warning => (MSBuildLogEventCategory.Warning, null),
        LogEventLevel.Error => (MSBuildLogEventCategory.Error, null),
        LogEventLevel.Fatal => (MSBuildLogEventCategory.Error, null),
        _ => throw new ArgumentOutOfRangeException(nameof(level), level, $"Unrecognised {nameof(LogEventLevel)}"),
    };
}
