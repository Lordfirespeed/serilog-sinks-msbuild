/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/ExceptionTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class ExceptionTokenRenderer : OutputTemplateTokenRenderer
{
    const string StackFrameLinePrefix = "   ";

    private readonly MSBuildConsoleTheme _theme;

    public ExceptionTokenRenderer(MSBuildConsoleTheme theme, PropertyToken pt)
    {
        _theme = theme;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        // Padding is never applied by this renderer.

        if (logEvent.Exception is null)
            return;

        var lines = new StringReader(logEvent.Exception.ToString());
        string? nextLine;
        while ((nextLine = lines.ReadLine()) != null)
        {
            var style = nextLine.StartsWith(StackFrameLinePrefix) ? MSBuildConsoleThemeStyle.SecondaryText : MSBuildConsoleThemeStyle.Text;
            var _ = 0;
            using (_theme.Apply(output, style, ref _))
                output.Write(nextLine);
            output.WriteLine();
        }
    }
}
