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

using Microsoft.Build.Framework;
using Serilog.Events;
using Serilog.Sinks.MSBuild.Extensions;

namespace Serilog.Sinks.MSBuild;

/// <summary>
/// <see cref="LogEvent"/> property names that are significant for <see cref="MSBuildTaskLogSink"/>
/// and would give MSBuild additional information if specified.
/// </summary>
/// <remarks>All are optional. A value of 0 is used in place of <see langword="null"/> for <see cref="int"/>s.</remarks>

// ReSharper disable once InconsistentNaming
public record MSBuildContext
{
    /// <summary>
    /// Populate an <see cref="MSBuildContext"/> using a SeriLog <see cref="LogEvent"/>.
    /// </summary>
    /// <param name="logEvent">The <see cref="LogEvent"/> used to lookup contextual information.</param>
    /// <returns><see cref="MSBuildContext"/> populated as much as possible.</returns>
    public static MSBuildContext FromLogEvent(LogEvent logEvent)
    {
        var (category, importance) = logEvent.Level.ToMSBuildCategory();

        return new MSBuildContext {
            Category = category,
            Importance = importance,
            Subcategory = GetString(nameof(Subcategory)),
            MessageCode = GetString(nameof(MessageCode)),
            HelpKeyword = GetString(nameof(HelpKeyword)),
            HelpLink = GetString(nameof(HelpLink)),
            File = GetString(nameof(File)),
            LineNumber = GetInt(nameof(LineNumber)),
            ColumnNumber = GetInt(nameof(ColumnNumber)),
            EndLineNumber = GetInt(nameof(EndLineNumber)),
            EndColumnNumber = GetInt(nameof(EndColumnNumber)),
        };

        object? GetScalarValue(string key)
        {
            if (!logEvent.Properties.TryGetValue(key, out var value)) return null;
            return value is ScalarValue scalarValue ? scalarValue.Value : null;
        }

        string? GetString(string key) => GetScalarValue(key)?.ToString();

        int GetInt(string key)
        {
            var scalarValue = GetScalarValue(key);

            if (scalarValue == null) return 0;
            if (scalarValue is int intValue) return intValue;
            if (int.TryParse(scalarValue.ToString(), out intValue)) return intValue;
            return 0;
        }
    }

    /// <summary>
    /// The message's category.
    /// </summary>
    public required MSBuildLogEventCategory Category { get; init; }

    /// <summary>
    /// The message's importance (used only when message <see cref="Category"/>
    /// is <see cref="MSBuildLogEventCategory.Message"/>).
    /// </summary>
    public MessageImportance? Importance { get; init; }

    /// <summary>
    /// The message's subcategory.
    /// </summary>
    public string? Subcategory { get; init; }

    /// <summary>
    /// The message's error code.
    /// </summary>
    public string? MessageCode { get; init; }

    /// <summary>
    /// The help keyword for the host IDE (can be null).
    /// </summary>
    public string? HelpKeyword { get; init; }

    /// <summary>
    /// A link pointing to more information about the error.
    /// </summary>
    /// <remarks>
    /// Supported only for warnings and errors and in MSBuild 16.8+.
    /// In all other cases this property will be ignored.
    /// </remarks>
    public string? HelpLink { get; init; }

    /// <summary>
    /// The path to the file containing the message's cause.
    /// </summary>
    public string? File { get; init; }

    /// <summary>
    /// The 'start' line number of the message's cause.
    /// </summary>
    public int LineNumber { get; init; }

    /// <summary>
    /// The 'start' column (character) number of the message's cause.
    /// </summary>
    public int ColumnNumber { get; init; }

    /// <summary>
    /// The 'end' line number of the message's cause.
    /// </summary>
    public int EndLineNumber { get; init; }

    /// <summary>
    /// The 'end' column (character) number of the message's cause.
    /// </summary>
    public int EndColumnNumber { get; init; }
}
