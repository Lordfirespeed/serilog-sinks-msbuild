/*
 * The contents of this file have been sourced from various projects and modified by Joe Clack.
 *
 * https://github.com/serilog-contrib/serilog-sinks-msbuild/blob/08bfd97b4ac0523a2b19407ba2325e75b22d6d84/Serilog.Sinks.MSBuild/MSBuildSink.cs
 * Copyright 2019 Theodore Tsirpanis
 * Theodore Tsirpanis licenses the referenced file to Joe Clack under the Apache-2.0 license.
 *
 * https://github.com/EvaisaDev/UnityNetcodePatcher/blob/c64eb86e74e85e1badc442adc0bf270bab0df6b6/NetcodePatcher.MSBuild/MSBuildTaskLogSink.cs
 * UnityNetcodePatcher Copyright (c) 2023 EvaisaDev
 * EvaisaDev licenses the referenced file to Joe Clack under the MIT license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSBuild.Extensions;

namespace Serilog.Sinks.MSBuild;

// ReSharper disable once InconsistentNaming
/// <summary>
/// A <see cref="Serilog"/> sink that redirects <see cref="LogEvent"/>s to an MSBuild <see cref="TaskLoggingHelper"/>
/// instance.
/// </summary>
public class MSBuildTaskLogSink : ILogEventSink
{
    private const ExceptionRenderStyleFlags ExceptionRenderStyle = 0
        | ExceptionRenderStyleFlags.IncludeInner
        | ExceptionRenderStyleFlags.IncludeStackTrace
        | ExceptionRenderStyleFlags.IncludeType;

    private readonly IFormatProvider? _formatProvider;
    private readonly TaskLoggingHelper _taskLoggingHelper;

    /// <summary>
    /// Creates a <see cref="MSBuildTaskLogSink"/> from an <see cref="ITask"/>.
    /// </summary>
    /// <param name="task">The <see cref="ITask"/> to which log events will be sent.</param>
    /// <param name="formatProvider">Supplies culture-specific
    /// formatting information. Can be <see langword="null"/>.</param>
    public MSBuildTaskLogSink(ITask task, IFormatProvider? formatProvider)
    {
        if (task is null)
            throw new ArgumentNullException(nameof(task));

        _taskLoggingHelper = task is Task implementedTask ? implementedTask.Log : new TaskLoggingHelper(task);
        _formatProvider = formatProvider;
    }

    /// <summary>
    /// Creates a <see cref="MSBuildTaskLogSink"/> from a <see cref="TaskLoggingHelper"/>.
    /// </summary>
    /// <param name="taskLoggingHelper">The <see cref="TaskLoggingHelper"/> to which log events will be sent.</param>
    /// <param name="formatProvider">Supplies culture-specific
    /// formatting information. Can be <see langword="null"/>.</param>
    public MSBuildTaskLogSink(TaskLoggingHelper taskLoggingHelper, IFormatProvider? formatProvider)
    {
        if (taskLoggingHelper is null)
            throw new ArgumentNullException(nameof(taskLoggingHelper));

        _taskLoggingHelper = taskLoggingHelper;
        _formatProvider = formatProvider;
    }

    /// <inheritdoc cref="ILogEventSink.Emit"/>
    public void Emit(LogEvent logEvent)
    {
        var context = MSBuildContext.FromLogEvent(logEvent);
        var message = logEvent.RenderMessage(_formatProvider);

        if (logEvent.Exception is not null)
            message += Environment.NewLine + logEvent.RenderException(ExceptionRenderStyle);

        switch (logEvent.Level)
        {
            case LogEventLevel.Verbose:
                LogMessageWithContext(MessageImportance.Low, message, context);
                break;
            case LogEventLevel.Debug:
                LogMessageWithContext(MessageImportance.Normal, message, context);
                break;
            case LogEventLevel.Information:
                LogMessageWithContext(MessageImportance.High, message, context);
                break;
            case LogEventLevel.Warning:
                LogWarningWithContext(message, context);
                break;
            case LogEventLevel.Error:
            case LogEventLevel.Fatal:
                LogErrorWithContext(message, context);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logEvent), $"Unrecognised {nameof(LogEventLevel)}");
        }
    }

    private void LogMessageWithContext(MessageImportance importance, string message, MSBuildContext context)
        => _taskLoggingHelper.LogMessage(
            context.Subcategory,
            context.MessageCode,
            context.HelpKeyword,
            context.File,
            context.LineNumber,
            context.ColumnNumber,
            context.EndLineNumber,
            context.EndColumnNumber,
            importance,
            message
        );

    private void LogWarningWithContext(string message, MSBuildContext context)
        => _taskLoggingHelper.LogWarning(
            context.Subcategory,
            context.MessageCode,
            context.HelpKeyword,
            context.HelpLink,
            context.File,
            context.LineNumber,
            context.ColumnNumber,
            context.EndLineNumber,
            context.EndColumnNumber,
            message
        );

    private void LogErrorWithContext(string message, MSBuildContext context)
        => _taskLoggingHelper.LogError(
            context.Subcategory,
            context.MessageCode,
            context.HelpKeyword,
            context.HelpLink,
            context.File,
            context.LineNumber,
            context.ColumnNumber,
            context.EndLineNumber,
            context.EndColumnNumber,
            message
        );
}
