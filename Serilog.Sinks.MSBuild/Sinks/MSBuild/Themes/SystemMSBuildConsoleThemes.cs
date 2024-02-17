/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/SystemConsoleThemes.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.Collections.Generic;

namespace Serilog.Sinks.MSBuild.Themes;

static class SystemMSBuildConsoleThemes
{
    public static SystemMSBuildConsoleTheme Literate { get; } = new SystemMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, SystemMSBuildConsoleThemeStyle>
        {
            [MSBuildConsoleThemeStyle.Text] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.SecondaryText] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.TertiaryText] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.Invalid] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Yellow },
            [MSBuildConsoleThemeStyle.Null] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Blue },
            [MSBuildConsoleThemeStyle.Name] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.String] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
            [MSBuildConsoleThemeStyle.Number] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Magenta },
            [MSBuildConsoleThemeStyle.Boolean] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Blue },
            [MSBuildConsoleThemeStyle.Scalar] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Green },
            [MSBuildConsoleThemeStyle.LevelVerbose] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.LevelDebug] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.LevelInformation] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.LevelWarning] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Yellow },
            [MSBuildConsoleThemeStyle.LevelError] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
            [MSBuildConsoleThemeStyle.LevelFatal] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
        });

    public static SystemMSBuildConsoleTheme Grayscale { get; } = new SystemMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, SystemMSBuildConsoleThemeStyle>
        {
            [MSBuildConsoleThemeStyle.Text] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.SecondaryText] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.TertiaryText] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.Invalid] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.Null] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Name] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.String] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Number] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Boolean] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Scalar] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.LevelVerbose] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.LevelDebug] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.LevelInformation] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.LevelWarning] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.LevelError] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Black, Background = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.LevelFatal] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Black, Background = ConsoleColor.White },
        });

    public static SystemMSBuildConsoleTheme Colored { get; } = new SystemMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, SystemMSBuildConsoleThemeStyle>
        {
            [MSBuildConsoleThemeStyle.Text] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray },
            [MSBuildConsoleThemeStyle.SecondaryText] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.TertiaryText] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.Invalid] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Yellow },
            [MSBuildConsoleThemeStyle.Null] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Name] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.String] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Number] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Boolean] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.Scalar] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White },
            [MSBuildConsoleThemeStyle.LevelVerbose] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.Gray, Background = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.LevelDebug] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkGray },
            [MSBuildConsoleThemeStyle.LevelInformation] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Blue },
            [MSBuildConsoleThemeStyle.LevelWarning] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.DarkGray, Background = ConsoleColor.Yellow },
            [MSBuildConsoleThemeStyle.LevelError] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
            [MSBuildConsoleThemeStyle.LevelFatal] = new SystemMSBuildConsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
        });
}
