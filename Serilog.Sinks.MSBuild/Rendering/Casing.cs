/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Rendering/Casing.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

namespace Serilog.Sinks.MSBuild.Rendering;

static class Casing
{
    /// <summary>
    /// Apply upper or lower casing to <paramref name="value"/> when <paramref name="format"/> is provided.
    /// Returns <paramref name="value"/> when no or invalid format provided.
    /// </summary>
    /// <param name="value">Provided string for formatting.</param>
    /// <param name="format">Format string.</param>
    /// <returns>The provided <paramref name="value"/> with formatting applied.</returns>
    public static string Format(string value, string? format = null)
    {
        switch (format)
        {
            case "u":
                return value.ToUpperInvariant();
            case "w":
                return value.ToLowerInvariant();
            default:
                return value;
        }
    }
}
