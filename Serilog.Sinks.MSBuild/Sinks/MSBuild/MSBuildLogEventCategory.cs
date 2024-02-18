/*
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Serilog.Sinks.MSBuild;

/// <summary>
/// Specifies the meaning and relative importance of an MSBuild <see cref="TaskLoggingHelper"/> log event.
/// </summary>
/// <seealso cref="BuildMessageEventArgs"/>
/// <seealso cref="BuildWarningEventArgs"/>
/// <seealso cref="BuildErrorEventArgs"/>
public enum MSBuildLogEventCategory
{
    /// <summary>
    ///
    /// </summary>
    Message,

    /// <summary>
    ///
    /// </summary>
    Warning,

    /// <summary>
    ///
    /// </summary>
    Error,
}
