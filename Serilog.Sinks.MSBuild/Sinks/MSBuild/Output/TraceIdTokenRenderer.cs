﻿/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/TraceIdTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Rendering;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class TraceIdTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly Alignment? _alignment;

    public TraceIdTokenRenderer(MSBuildConsoleTheme theme, PropertyToken traceIdToken)
    {
        _theme = theme;
        _alignment = traceIdToken.Alignment;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        if (logEvent.TraceId is not { } traceId)
            return;

        var _ = 0;
        using (_theme.Apply(output, MSBuildConsoleThemeStyle.Text, ref _))
        {
            if (_alignment is {} alignment)
                Padding.Apply(output, traceId.ToString(), alignment);
            else
                output.Write(traceId);
        }
    }
}