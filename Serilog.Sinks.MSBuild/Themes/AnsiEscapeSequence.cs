/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/AnsiEscapeSequence.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

namespace Serilog.Sinks.MSBuild.Themes;

static class AnsiEscapeSequence
{
    public const string Unthemed = "";
    public const string Reset = "\x1b[0m";
    public const string Bold = "\x1b[1m";

    public const string Black = "\x1b[30m";
    public const string Red = "\x1b[31m";
    public const string Green = "\x1b[32m";
    public const string Yellow = "\x1b[33m";
    public const string Blue = "\x1b[34m";
    public const string Magenta = "\x1b[35m";
    public const string Cyan = "\x1b[36m";
    public const string White = "\x1b[37m";

    public const string BrightBlack = "\x1b[30;1m";
    public const string BrightRed = "\x1b[31;1m";
    public const string BrightGreen = "\x1b[32;1m";
    public const string BrightYellow = "\x1b[33;1m";
    public const string BrightBlue = "\x1b[34;1m";
    public const string BrightMagenta = "\x1b[35;1m";
    public const string BrightCyan = "\x1b[36;1m";
    public const string BrightWhite = "\x1b[37;1m";
}