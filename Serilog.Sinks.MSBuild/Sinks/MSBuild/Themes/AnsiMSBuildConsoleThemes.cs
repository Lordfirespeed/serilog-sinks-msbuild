/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/AnsiConsoleThemes.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.Collections.Generic;

namespace Serilog.Sinks.MSBuild.Themes;

static class AnsiMSBuildConsoleThemes
{
    public static AnsiMSBuildConsoleTheme Literate { get; } = new AnsiMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, string>
        {
            [MSBuildConsoleThemeStyle.Text] = "\x1b[38;5;0015m",
            [MSBuildConsoleThemeStyle.SecondaryText] = "\x1b[38;5;0007m",
            [MSBuildConsoleThemeStyle.TertiaryText] = "\x1b[38;5;0008m",
            [MSBuildConsoleThemeStyle.SuccessText] = "\x1b[38;5;0113m",
            [MSBuildConsoleThemeStyle.DangerText] = "\x1b[38;5;0208m",
            [MSBuildConsoleThemeStyle.WarningText] = "\x1b[38;5;0011m",
            [MSBuildConsoleThemeStyle.ExceptionText] = "\x1b[38;5;0009m",
            [MSBuildConsoleThemeStyle.Invalid] = "\x1b[38;5;0011m",
            [MSBuildConsoleThemeStyle.Null] = "\x1b[38;5;0027m",
            [MSBuildConsoleThemeStyle.Name] = "\x1b[38;5;0007m",
            [MSBuildConsoleThemeStyle.String] = "\x1b[38;5;0045m",
            [MSBuildConsoleThemeStyle.Number] = "\x1b[38;5;0200m",
            [MSBuildConsoleThemeStyle.Boolean] = "\x1b[38;5;0027m",
            [MSBuildConsoleThemeStyle.Scalar] = "\x1b[38;5;0085m",
            [MSBuildConsoleThemeStyle.LevelVerbose] = "\x1b[38;5;0007m",
            [MSBuildConsoleThemeStyle.LevelDebug] = "\x1b[38;5;0007m",
            [MSBuildConsoleThemeStyle.LevelInformation] = "\x1b[38;5;0015m",
            [MSBuildConsoleThemeStyle.LevelWarning] = "\x1b[38;5;0011m",
            [MSBuildConsoleThemeStyle.LevelError] = "\x1b[38;5;0015m\x1b[48;5;0196m",
            [MSBuildConsoleThemeStyle.LevelFatal] = "\x1b[38;5;0015m\x1b[48;5;0196m",
        });

    public static AnsiMSBuildConsoleTheme Grayscale { get; } = new AnsiMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, string>
        {
            [MSBuildConsoleThemeStyle.Text] = "\x1b[37;1m",
            [MSBuildConsoleThemeStyle.SecondaryText] = "\x1b[37m",
            [MSBuildConsoleThemeStyle.TertiaryText] = "\x1b[30;1m",
            [MSBuildConsoleThemeStyle.Invalid] = "\x1b[37;1m\x1b[47m",
            [MSBuildConsoleThemeStyle.Null] = "\x1b[1m\x1b[37;1m",
            [MSBuildConsoleThemeStyle.Name] = "\x1b[37m",
            [MSBuildConsoleThemeStyle.String] = "\x1b[1m\x1b[37;1m",
            [MSBuildConsoleThemeStyle.Number] = "\x1b[1m\x1b[37;1m",
            [MSBuildConsoleThemeStyle.Boolean] = "\x1b[1m\x1b[37;1m",
            [MSBuildConsoleThemeStyle.Scalar] = "\x1b[1m\x1b[37;1m",
            [MSBuildConsoleThemeStyle.LevelVerbose] = "\x1b[30;1m",
            [MSBuildConsoleThemeStyle.LevelDebug] = "\x1b[30;1m",
            [MSBuildConsoleThemeStyle.LevelInformation] = "\x1b[37;1m",
            [MSBuildConsoleThemeStyle.LevelWarning] = "\x1b[37;1m\x1b[47m",
            [MSBuildConsoleThemeStyle.LevelError] = "\x1b[30m\x1b[47m",
            [MSBuildConsoleThemeStyle.LevelFatal] = "\x1b[30m\x1b[47m",
        });

    public static AnsiMSBuildConsoleTheme Code { get; } = new AnsiMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, string>
        {
            [MSBuildConsoleThemeStyle.Text] = "\x1b[38;5;0253m",
            [MSBuildConsoleThemeStyle.SecondaryText] = "\x1b[38;5;0246m",
            [MSBuildConsoleThemeStyle.TertiaryText] = "\x1b[38;5;0242m",
            [MSBuildConsoleThemeStyle.SuccessText] = "\x1b[38;5;0113m",
            [MSBuildConsoleThemeStyle.DangerText] = "\x1b[38;5;0208m",
            [MSBuildConsoleThemeStyle.WarningText] = "\x1b[38;5;0011m",
            [MSBuildConsoleThemeStyle.ExceptionText] = "\x1b[38;5;0009m",
            [MSBuildConsoleThemeStyle.Invalid] = "\x1b[33;1m",
            [MSBuildConsoleThemeStyle.Null] = "\x1b[38;5;0038m",
            [MSBuildConsoleThemeStyle.Name] = "\x1b[38;5;0081m",
            [MSBuildConsoleThemeStyle.String] = "\x1b[38;5;0216m",
            [MSBuildConsoleThemeStyle.Number] = "\x1b[38;5;151m",
            [MSBuildConsoleThemeStyle.Boolean] = "\x1b[38;5;0038m",
            [MSBuildConsoleThemeStyle.Scalar] = "\x1b[38;5;0079m",
            [MSBuildConsoleThemeStyle.LevelVerbose] = "\x1b[37m",
            [MSBuildConsoleThemeStyle.LevelDebug] = "\x1b[37m",
            [MSBuildConsoleThemeStyle.LevelInformation] = "\x1b[37;1m",
            [MSBuildConsoleThemeStyle.LevelWarning] = "\x1b[38;5;0229m",
            [MSBuildConsoleThemeStyle.LevelError] = "\x1b[38;5;0197m\x1b[48;5;0238m",
            [MSBuildConsoleThemeStyle.LevelFatal] = "\x1b[38;5;0197m\x1b[48;5;0238m",
        });

    public static AnsiMSBuildConsoleTheme Sixteen { get; } = new AnsiMSBuildConsoleTheme(
        new Dictionary<MSBuildConsoleThemeStyle, string>
        {
            [MSBuildConsoleThemeStyle.Text] = AnsiEscapeSequence.Unthemed,
            [MSBuildConsoleThemeStyle.SecondaryText] = AnsiEscapeSequence.Unthemed,
            [MSBuildConsoleThemeStyle.TertiaryText] = AnsiEscapeSequence.Unthemed,
            [MSBuildConsoleThemeStyle.SuccessText] = AnsiEscapeSequence.BrightGreen,
            [MSBuildConsoleThemeStyle.DangerText] = AnsiEscapeSequence.Red,
            [MSBuildConsoleThemeStyle.WarningText] = AnsiEscapeSequence.BrightYellow,
            [MSBuildConsoleThemeStyle.ExceptionText] = AnsiEscapeSequence.BrightRed,
            [MSBuildConsoleThemeStyle.Invalid] = AnsiEscapeSequence.Yellow,
            [MSBuildConsoleThemeStyle.Null] = AnsiEscapeSequence.Blue,
            [MSBuildConsoleThemeStyle.Name] = AnsiEscapeSequence.Unthemed,
            [MSBuildConsoleThemeStyle.String] = AnsiEscapeSequence.Cyan,
            [MSBuildConsoleThemeStyle.Number] = AnsiEscapeSequence.Magenta,
            [MSBuildConsoleThemeStyle.Boolean] = AnsiEscapeSequence.Blue,
            [MSBuildConsoleThemeStyle.Scalar] = AnsiEscapeSequence.Green,
            [MSBuildConsoleThemeStyle.LevelVerbose] = AnsiEscapeSequence.Unthemed,
            [MSBuildConsoleThemeStyle.LevelDebug] = AnsiEscapeSequence.Bold,
            [MSBuildConsoleThemeStyle.LevelInformation] = AnsiEscapeSequence.BrightCyan,
            [MSBuildConsoleThemeStyle.LevelWarning] = AnsiEscapeSequence.BrightYellow,
            [MSBuildConsoleThemeStyle.LevelError] = AnsiEscapeSequence.BrightRed,
            [MSBuildConsoleThemeStyle.LevelFatal] = AnsiEscapeSequence.BrightRed,
        });
}
