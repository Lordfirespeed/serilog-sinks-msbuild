/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/LevelTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.Collections.Generic;
using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Rendering;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class LevelTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly PropertyToken _levelToken;

    static readonly Dictionary<LogEventLevel, MSBuildConsoleThemeStyle> Levels = new Dictionary<LogEventLevel, MSBuildConsoleThemeStyle>
    {
        { LogEventLevel.Verbose, MSBuildConsoleThemeStyle.LevelVerbose },
        { LogEventLevel.Debug, MSBuildConsoleThemeStyle.LevelDebug },
        { LogEventLevel.Information, MSBuildConsoleThemeStyle.LevelInformation },
        { LogEventLevel.Warning, MSBuildConsoleThemeStyle.LevelWarning },
        { LogEventLevel.Error, MSBuildConsoleThemeStyle.LevelError },
        { LogEventLevel.Fatal, MSBuildConsoleThemeStyle.LevelFatal },
    };

    public LevelTokenRenderer(MSBuildConsoleTheme theme, PropertyToken levelToken)
    {
        _theme = theme;
        _levelToken = levelToken;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        var moniker = LevelOutputFormat.GetLevelMoniker(logEvent.Level, _levelToken.Format);
        if (!Levels.TryGetValue(logEvent.Level, out var levelStyle))
            levelStyle = MSBuildConsoleThemeStyle.Invalid;

        var _ = 0;
        using (_theme.Apply(output, levelStyle, ref _))
            Padding.Apply(output, moniker, _levelToken.Alignment);
    }
}
