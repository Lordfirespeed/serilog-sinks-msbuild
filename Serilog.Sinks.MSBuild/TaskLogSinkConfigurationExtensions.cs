/*
 * The contents of this file have been sourced from various projects and modified by Joe Clack.
 *
 * https://github.com/serilog-contrib/serilog-sinks-msbuild/blob/08bfd97b4ac0523a2b19407ba2325e75b22d6d84/Serilog.Sinks.MSBuild/MSBuildSink.cs
 * Copyright 2019 Theodore Tsirpanis
 * Theodore Tsirpanis licenses the referenced file to Joe Clack under the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Serilog.Events;
using Serilog.Sinks.MSBuild;
using Serilog.Configuration;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog;

/// <summary>
/// Adds extension methods to <see cref="LoggerConfiguration"/> related to configuring <see cref="LogEvent"/>
/// redirection to MSBuild via <see cref="MSBuildTaskLogSink"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class TaskLogSinkConfigurationExtensions
{
    /// <summary>
    /// Redirects log events to MSBuild via <paramref name="task"/>.
    /// </summary>
    /// <param name="sinkConfiguration">Logger sink configuration.</param>
    /// <param name="task">The MSBuild <see cref="ITask"/> to log events for.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or <see langword="null"/>.</param>
    /// <param name="theme">The theme to apply to the styled output. If not specified,
    /// uses <see cref="SystemMSBuildConsoleTheme.Literate"/>.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <remarks>Because this sink redirects messages to another logging system,
    /// it is recommended to allow all event levels to pass through.</remarks>
    public static LoggerConfiguration MSBuildTask(
        this LoggerSinkConfiguration sinkConfiguration,
        ITask task,
        IFormatProvider? formatProvider = null,
        MSBuildConsoleTheme? theme = null)
    {
        if (sinkConfiguration is null) throw new ArgumentNullException(nameof(sinkConfiguration));
        if (task is null) throw new ArgumentNullException(nameof(task));

        TaskLoggingHelper taskLoggingHelper = task is Task implementedTask ? implementedTask.Log : new TaskLoggingHelper(task);

        theme ??= SystemMSBuildConsoleTheme.Literate;

        return sinkConfiguration.Sink(new MSBuildTaskLogSink(taskLoggingHelper, theme, formatProvider));
    }

    /// <summary>
    /// Redirects log events to an MSBuild <see cref="TaskLoggingHelper"/>.
    /// </summary>
    /// <param name="sinkConfiguration">Logger sink configuration.</param>
    /// <param name="taskLoggingHelper">The MSBuild <see cref="TaskLoggingHelper"/> to log events to.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information. Can be <see langword="null"/>.</param>
    /// <param name="theme">The theme to apply to the styled output. If not specified,
    /// uses <see cref="SystemMSBuildConsoleTheme.Literate"/>.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <remarks>Because this sink redirects messages to another logging system,
    /// it is recommended to allow all event levels to pass through.</remarks>
    public static LoggerConfiguration MSBuildTaskLoggingHelper(
        this LoggerSinkConfiguration sinkConfiguration,
        TaskLoggingHelper taskLoggingHelper,
        IFormatProvider? formatProvider = null,
        MSBuildConsoleTheme? theme = null)
    {
        if (sinkConfiguration is null) throw new ArgumentNullException(nameof(sinkConfiguration));
        if (taskLoggingHelper is null) throw new ArgumentNullException(nameof(taskLoggingHelper));

        theme ??= SystemMSBuildConsoleTheme.Literate;

        return sinkConfiguration.Sink(new MSBuildTaskLogSink(taskLoggingHelper, theme, formatProvider));
    }
}
