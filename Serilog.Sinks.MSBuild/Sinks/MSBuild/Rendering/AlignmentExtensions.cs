/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Rendering/AlignmentExtensions.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using Serilog.Parsing;

namespace Serilog.Sinks.MSBuild.Rendering;

static class AlignmentExtensions
{
    public static Alignment Widen(this Alignment alignment, int amount)
    {
        return new Alignment(alignment.Direction, alignment.Width + amount);
    }
}
