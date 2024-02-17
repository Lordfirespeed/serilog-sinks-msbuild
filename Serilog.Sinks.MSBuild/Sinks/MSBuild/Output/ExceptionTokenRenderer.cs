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
using System.Linq;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Extensions;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class ExceptionTokenRenderer : OutputTemplateTokenRenderer
{
    private readonly MSBuildConsoleTheme _theme;

    private const ExceptionRenderStyleFlags ExceptionRenderStyle = 0
        | ExceptionRenderStyleFlags.IncludeInner
        | ExceptionRenderStyleFlags.IncludeStackTrace
        | ExceptionRenderStyleFlags.IncludeType;

    public ExceptionTokenRenderer(MSBuildConsoleTheme theme, PropertyToken pt)
    {
        _theme = theme;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        // Padding is never applied by this renderer.

        if (logEvent.Exception is null)
            return;

        var lines = new StringReader(logEvent.RenderException(ExceptionRenderStyle));
        while (lines.ReadLine() is { } nextLine)
        {
            var style = LineIsStackTrace(nextLine) ? MSBuildConsoleThemeStyle.SecondaryText : MSBuildConsoleThemeStyle.Text;
            var _ = 0;
            using (_theme.Apply(output, style, ref _))
                output.Write(nextLine);
            output.WriteLine();
        }

        // https://stackoverflow.com/a/20411839/11045433
        int LeadingSpaceCount(string line) => line
            .TakeWhile(c => c is ' ')
            .Count();

        bool LineIsStackTrace(string line) => LeadingSpaceCount(line) % 4 == 3;
    }
}
