/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Rendering/Padding.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;
using Serilog.Parsing;

namespace Serilog.Sinks.MSBuild.Rendering;

static class Padding
{
    static readonly char[] PaddingChars = new string(' ', 80).ToCharArray();

    /// <summary>
    /// Writes the provided value to the output, applying direction-based padding when <paramref name="alignment"/> is provided.
    /// </summary>
    /// <param name="output">Output object to write result.</param>
    /// <param name="value">Provided value.</param>
    /// <param name="alignment">The alignment settings to apply when rendering <paramref name="value"/>.</param>
    public static void Apply(TextWriter output, string value, Alignment? alignment)
    {
        if (alignment is null || value.Length >= alignment.Value.Width)
        {
            output.Write(value);
            return;
        }

        var pad = alignment.Value.Width - value.Length;

        if (alignment.Value.Direction == AlignmentDirection.Left)
            output.Write(value);

        if (pad <= PaddingChars.Length)
        {
            output.Write(PaddingChars, 0, pad);
        }
        else
        {
            output.Write(new string(' ', pad));
        }

        if (alignment.Value.Direction == AlignmentDirection.Right)
            output.Write(value);
    }
}
