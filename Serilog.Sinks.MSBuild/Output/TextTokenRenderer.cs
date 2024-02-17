/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/TextTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;
using Serilog.Events;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class TextTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly string _text;

    public TextTokenRenderer(MSBuildConsoleTheme theme, string text)
    {
        _theme = theme;
        _text = text;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        var _ = 0;
        using (_theme.Apply(output, MSBuildConsoleThemeStyle.TertiaryText, ref _))
            output.Write(_text);
    }
}
