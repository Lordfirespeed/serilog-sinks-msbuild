/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/SystemConsoleTheme.cs
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
/// A console theme using the styling facilities of the <see cref="System.Console"/> class. Recommended
/// for Windows versions prior to Windows 10.
/// </summary>
public class SystemMSBuildConsoleTheme : MSBuildConsoleTheme
{
    /// <summary>
    /// A theme using only gray, black and white.
    /// </summary>
    public static SystemMSBuildConsoleTheme Grayscale { get; } = SystemMSBuildConsoleThemes.Grayscale;

    /// <summary>
    /// A theme in the style of the original <i>Serilog.Sinks.Literate</i>.
    /// </summary>
    public static SystemMSBuildConsoleTheme Literate { get; } = SystemMSBuildConsoleThemes.Literate;

    /// <summary>
    /// A theme based on the original Serilog "colored console" sink.
    /// </summary>
    public static SystemMSBuildConsoleTheme Colored { get; } = SystemMSBuildConsoleThemes.Colored;

    /// <summary>
    /// Construct a theme given a set of styles.
    /// </summary>
    /// <param name="styles">Styles to apply within the theme.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="styles"/> is <code>null</code></exception>
    public SystemMSBuildConsoleTheme(IReadOnlyDictionary<MSBuildConsoleThemeStyle, SystemMSBuildConsoleThemeStyle> styles)
    {
        if (styles is null) throw new ArgumentNullException(nameof(styles));
        Styles = styles.ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    /// <inheritdoc/>
    public IReadOnlyDictionary<MSBuildConsoleThemeStyle, SystemMSBuildConsoleThemeStyle> Styles { get; }

    /// <inheritdoc/>
    public override bool CanBuffer => false;

    /// <inheritdoc/>
    protected override int ResetCharCount { get; }

    /// <inheritdoc/>
    public override int Set(TextWriter output, MSBuildConsoleThemeStyle style)
    {
        if (Styles.TryGetValue(style, out var wcts))
        {
            if (wcts.Foreground.HasValue)
                Console.ForegroundColor = wcts.Foreground.Value;
            if (wcts.Background.HasValue)
                Console.BackgroundColor = wcts.Background.Value;
        }

        return 0;
    }

    /// <inheritdoc/>
    public override void Reset(TextWriter output)
    {
        Console.ResetColor();
    }
}
