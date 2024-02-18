/*
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.MSBuild.Themes;

/// <summary>
/// Describes the message classes available for applying <see cref="MSBuildConsoleThemeStyle"/> to text content
/// in log messages.
/// </summary>
public sealed class MessageClass : ILogEventEnricher
{
    /// <summary>
    /// The <see cref="string"/> key used as the name for the <see cref="LogEventProperty"/> that determines which
    /// <see cref="MSBuildConsoleThemeStyle"/> to apply.
    /// </summary>
    /// <seealso cref="Enrich"/>
    public const string PropertyName = nameof(MessageClass);

    /// <summary>
    /// Apply the standard text theme.
    /// </summary>
    /// <seealso cref="MSBuildConsoleThemeStyle.Text"/>
    public static MessageClass Default = new MessageClass("Default", MSBuildConsoleThemeStyle.Text);

    /// <summary>
    /// Apply a prominently positive 'success' text theme.
    /// </summary>
    /// <seealso cref="MSBuildConsoleThemeStyle.SuccessText"/>
    public static MessageClass Success = new MessageClass("Success", MSBuildConsoleThemeStyle.SuccessText);

    /// <summary>
    /// Apply a prominently negative 'danger' text theme.
    /// </summary>
    /// <seealso cref="MSBuildConsoleThemeStyle.DangerText"/>
    public static MessageClass Danger = new MessageClass("Danger", MSBuildConsoleThemeStyle.DangerText);

    /// <summary>
    /// Apply a neutral 'warning' text theme.
    /// </summary>
    /// <seealso cref="MSBuildConsoleThemeStyle.WarningText"/>
    public static MessageClass Warning = new MessageClass("Warning", MSBuildConsoleThemeStyle.WarningText);

    public string Name { get; }
    
    public MSBuildConsoleThemeStyle Style { get; }

    private MessageClass(string name, MSBuildConsoleThemeStyle style)
    {
        Name = name;
        Style = style;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent is null) throw new ArgumentNullException(nameof(logEvent));
        if (propertyFactory is null) throw new ArgumentNullException(nameof(propertyFactory));

        logEvent.AddOrUpdateProperty(new LogEventProperty(PropertyName, new ScalarValue(this)));
    }
}
