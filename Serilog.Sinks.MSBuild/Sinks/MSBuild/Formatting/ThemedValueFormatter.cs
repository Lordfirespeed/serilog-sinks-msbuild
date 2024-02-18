/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Formatting/ThemedValueFormatter.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.IO;
using Serilog.Data;
using Serilog.Events;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Formatting;

abstract class ThemedValueFormatter : LogEventPropertyValueVisitor<ThemedValueFormatterState, int>
{
    readonly MSBuildConsoleTheme _theme;

    protected ThemedValueFormatter(MSBuildConsoleTheme theme)
    {
        _theme = theme ?? throw new ArgumentNullException(nameof(theme));
    }

    protected MSBuildStyleReset ApplyStyle(TextWriter output, MSBuildConsoleThemeStyle style, ref int invisibleCharacterCount)
    {
        return _theme.Apply(output, style, ref invisibleCharacterCount);
    }

    public int Format(LogEventPropertyValue value, TextWriter output, string? format, bool literalTopLevel = false)
    {
        return Visit(new ThemedValueFormatterState { Output = output, Format = format, IsTopLevel = literalTopLevel }, value);
    }

    public abstract ThemedValueFormatter SwitchTheme(MSBuildConsoleTheme theme);
}
