/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/AnsiConsoleTheme.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Serilog.Sinks.MSBuild.Themes;

/// <summary>
/// A console theme using the ANSI terminal escape sequences. Recommended
/// for Linux and Windows 10+.
/// </summary>
public class AnsiMSBuildConsoleTheme : MSBuildConsoleTheme
{
    /// <summary>
    /// A 256-color theme along the lines of Visual Studio Code.
    /// </summary>
    public static AnsiMSBuildConsoleTheme Code { get; } = AnsiMSBuildConsoleThemes.Code;

    /// <summary>
    /// A theme using only gray, black and white.
    /// </summary>
    public static AnsiMSBuildConsoleTheme Grayscale { get; } = AnsiMSBuildConsoleThemes.Grayscale;

    /// <summary>
    /// A theme in the style of the original <i>Serilog.Sinks.Literate</i>.
    /// </summary>
    public static AnsiMSBuildConsoleTheme Literate { get; } = AnsiMSBuildConsoleThemes.Literate;

    /// <summary>
    /// A theme in the style of the original <i>Serilog.Sinks.Literate</i> using only standard 16 terminal colors that will work on light backgrounds.
    /// </summary>
    public static AnsiMSBuildConsoleTheme Sixteen { get; } = AnsiMSBuildConsoleThemes.Sixteen;

    readonly IReadOnlyDictionary<MSBuildConsoleThemeStyle, string> _styles;
    const string AnsiStyleReset = "\x1b[0m";
    const string MSBuildErrorStyle = "\x1b[38;5;9m";
    const string MSBuildWarningStyle = "\x1b[38;5;11m";

    /// <summary>
    /// Construct a theme given a set of styles.
    /// </summary>
    /// <param name="styles">Styles to apply within the theme.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="styles"/> is <code>null</code></exception>
    public AnsiMSBuildConsoleTheme(IReadOnlyDictionary<MSBuildConsoleThemeStyle, string> styles)
    {
        if (styles is null) throw new ArgumentNullException(nameof(styles));
        _styles = styles.ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    /// <inheritdoc/>
    public override bool CanBuffer => true;

    /// <param name="context">The <see cref="MSBuildContext"/> of the styled message.</param>
    /// <returns>The appropriate reset string for the <see cref="MSBuildContext"/>.</returns>
    protected virtual string GetReset(MSBuildContext context) => context.Category switch {
        MSBuildLogEventCategory.Message => AnsiStyleReset,
        MSBuildLogEventCategory.Warning => MSBuildWarningStyle,
        MSBuildLogEventCategory.Error => MSBuildErrorStyle,
        _ => throw new ArgumentOutOfRangeException(nameof(context), context, $"Unrecognised {nameof(MSBuildLogEventCategory)}")
    };

    /// <inheritdoc/>
    protected override int GetResetCharCount(MSBuildContext context) => AnsiStyleReset.Length;

    /// <inheritdoc/>
    public override int Set(MSBuildContext context, TextWriter output, MSBuildConsoleThemeStyle style)
    {
        if (_styles.TryGetValue(style, out var ansiStyle))
        {
            output.Write(ansiStyle);
            return ansiStyle.Length;
        }
        return 0;
    }

    /// <inheritdoc/>
    public override void Reset(MSBuildContext context, TextWriter output) => output.Write(AnsiStyleReset);
}
