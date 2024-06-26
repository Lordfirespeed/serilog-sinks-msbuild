/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/LevelOutputFormat.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using Serilog.Events;
using Serilog.Sinks.MSBuild.Rendering;

namespace Serilog.Sinks.MSBuild.Output;

/// <summary>
/// Implements the {Level} element.
/// can now have a fixed width applied to it, as well as casing rules.
/// Width is set through formats like "u3" (uppercase three chars),
/// "w1" (one lowercase char), or "t4" (title case four chars).
/// </summary>
static class LevelOutputFormat
{
    static readonly string[][] TitleCaseLevelMap = {
        new []{ "V", "Vb", "Vrb", "Verb", "Verbo", "Verbos", "Verbose" },
        new []{ "D", "De", "Dbg", "Dbug", "Debug" },
        new []{ "I", "In", "Inf", "Info", "Infor", "Inform", "Informa", "Informat", "Informati", "Informatio", "Information" },
        new []{ "W", "Wn", "Wrn", "Warn", "Warni", "Warnin", "Warning" },
        new []{ "E", "Er", "Err", "Eror", "Error" },
        new []{ "F", "Fa", "Ftl", "Fatl", "Fatal" }
    };

    static readonly string[][] LowerCaseLevelMap = {
        new []{ "v", "vb", "vrb", "verb", "verbo", "verbos", "verbose" },
        new []{ "d", "de", "dbg", "dbug", "debug" },
        new []{ "i", "in", "inf", "info", "infor", "inform", "informa", "informat", "informati", "informatio", "information" },
        new []{ "w", "wn", "wrn", "warn", "warni", "warnin", "warning" },
        new []{ "e", "er", "err", "eror", "error" },
        new []{ "f", "fa", "ftl", "fatl", "fatal" }
    };

    static readonly string[][] UpperCaseLevelMap = {
        new []{ "V", "VB", "VRB", "VERB", "VERBO", "VERBOS", "VERBOSE" },
        new []{ "D", "DE", "DBG", "DBUG", "DEBUG" },
        new []{ "I", "IN", "INF", "INFO", "INFOR", "INFORM", "INFORMA", "INFORMAT", "INFORMATI", "INFORMATIO", "INFORMATION" },
        new []{ "W", "WN", "WRN", "WARN", "WARNI", "WARNIN", "WARNING" },
        new []{ "E", "ER", "ERR", "EROR", "ERROR" },
        new []{ "F", "FA", "FTL", "FATL", "FATAL" }
    };

    public static string GetLevelMoniker(LogEventLevel value, string? format = null)
    {
        var index = (int)value;
        if (index is < 0 or > (int)LogEventLevel.Fatal)
            return Casing.Format(value.ToString(), format);

        if (format == null || format.Length != 2 && format.Length != 3)
            return Casing.Format(GetLevelMoniker(TitleCaseLevelMap, index), format);

        // Using int.Parse() here requires allocating a string to exclude the first character prefix.
        // Junk like "wxy" will be accepted but produce benign results.
        var width = format[1] - '0';
        if (format.Length == 3)
        {
            width *= 10;
            width += format[2] - '0';
        }

        if (width < 1)
            return string.Empty;

        switch (format[0])
        {
            case 'w':
                return GetLevelMoniker(LowerCaseLevelMap, index, width);
            case 'u':
                return GetLevelMoniker(UpperCaseLevelMap, index, width);
            case 't':
                return GetLevelMoniker(TitleCaseLevelMap, index, width);
            default:
                return Casing.Format(GetLevelMoniker(TitleCaseLevelMap, index), format);
        }
    }

    static string GetLevelMoniker(string[][] caseLevelMap, int index, int width)
    {
        var caseLevel = caseLevelMap[index];
        return caseLevel[Math.Min(width, caseLevel.Length) - 1];
    }

    static string GetLevelMoniker(string[][] caseLevelMap, int index)
    {
        var caseLevel = caseLevelMap[index];
        return caseLevel[caseLevel.Length - 1];
    }
}
