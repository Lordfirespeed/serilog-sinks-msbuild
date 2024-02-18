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
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.MSBuild.Formatting;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild;

// ReSharper disable once InconsistentNaming
/// <summary>
/// A <see cref="Serilog"/> sink that redirects <see cref="LogEvent"/>s to an MSBuild <see cref="TaskLoggingHelper"/>
/// instance.
/// </summary>
public class MSBuildTaskLogSink : ILogEventSink
{
    const int DefaultWriteBufferCapacity = 256;

    private readonly IMSBuildTextFormatter _formatter;
    private readonly MSBuildConsoleTheme _theme;
    private readonly TaskLoggingHelper _taskLoggingHelper;

    /// <summary>
    /// Creates a <see cref="MSBuildTaskLogSink"/> from a <see cref="TaskLoggingHelper"/>.
    /// </summary>
    /// <param name="taskLoggingHelper">The <see cref="TaskLoggingHelper"/> to which log events will be sent.</param>
    /// <param name="theme">The theme to apply to the styled output.</param>
    /// <param name="formatter">Supplies culture-specific
    /// formatting information. Can be <see langword="null"/>.</param>
    public MSBuildTaskLogSink(TaskLoggingHelper taskLoggingHelper, MSBuildConsoleTheme theme, IMSBuildTextFormatter formatter)
    {
        _taskLoggingHelper = taskLoggingHelper ?? throw new ArgumentNullException(nameof(taskLoggingHelper));
        _theme = theme ?? throw new ArgumentNullException(nameof(theme));
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }

    /// <inheritdoc cref="ILogEventSink.Emit"/>
    public void Emit(LogEvent logEvent)
    {
        if (!_theme.CanBuffer)
            throw new NotSupportedException("MSBuild log themes must support buffering.");

        var context = MSBuildContext.FromLogEvent(logEvent);

        var buffer = new StringWriter(new StringBuilder(DefaultWriteBufferCapacity));
        _formatter.Format(logEvent, context, buffer);
        var message = buffer.ToString().TrimEnd();

        switch (context.Category) {
            case MSBuildLogEventCategory.Message:
                LogMessageWithContext(message, context);
                break;
            case MSBuildLogEventCategory.Warning:
                LogWarningWithContext(message, context);
                break;
            case MSBuildLogEventCategory.Error:
                LogErrorWithContext(message, context);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(context.Category), context.Category, $"Unrecognised {nameof(MSBuildLogEventCategory)}");
        }
    }

    private void LogMessageWithContext(string message, MSBuildContext context)
        => _taskLoggingHelper.LogMessage(
            context.Subcategory,
            context.MessageCode,
            context.HelpKeyword,
            context.File,
            context.LineNumber,
            context.ColumnNumber,
            context.EndLineNumber,
            context.EndColumnNumber,
            context.Importance,
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
