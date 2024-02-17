/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/StyleReset.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.IO;

namespace Serilog.Sinks.MSBuild.Themes;

struct StyleReset : IDisposable
{
    readonly MSBuildConsoleTheme _theme;
    readonly TextWriter _output;

    public StyleReset(MSBuildConsoleTheme theme, TextWriter output)
    {
        _theme = theme;
        _output = output;
    }

    public void Dispose()
    {
        _theme.Reset(_output);
    }
}
