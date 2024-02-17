/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/NewLineTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Rendering;

namespace Serilog.Sinks.MSBuild.Output;

class NewLineTokenRenderer : OutputTemplateTokenRenderer
{
    readonly Alignment? _alignment;

    public NewLineTokenRenderer(Alignment? alignment)
    {
        _alignment = alignment;
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        if (_alignment.HasValue)
            Padding.Apply(output, Environment.NewLine, _alignment.Value.Widen(Environment.NewLine.Length));
        else
            output.WriteLine();
    }
}