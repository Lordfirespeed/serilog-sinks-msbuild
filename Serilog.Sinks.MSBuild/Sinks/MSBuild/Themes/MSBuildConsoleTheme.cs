/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/ConsoleTheme.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;

namespace Serilog.Sinks.MSBuild.Themes;

/// <summary>
/// The base class for styled terminal output.
/// </summary>
public abstract class MSBuildConsoleTheme
{
    /// <summary>
    /// No styling applied.
    /// </summary>
    public static MSBuildConsoleTheme None { get; } = new EmptyMSBuildConsoleTheme();

    /// <summary>
    /// True if styling applied by the theme is written into the output, and can thus be
    /// buffered and measured.
    /// </summary>
    public abstract bool CanBuffer { get; }

    /// <summary>
    /// Begin a span of text in the specified <paramref name="style"/>.
    /// </summary>
    /// /// <param name="context">Output <see cref="MSBuildContext"/>.</param>
    /// <param name="output">Output destination.</param>
    /// <param name="style">Style to apply.</param>
    /// <returns> The number of characters written to <paramref name="output"/>. </returns>
    public abstract int Set(MSBuildContext context, TextWriter output, MSBuildConsoleThemeStyle style);

    /// <summary>
    /// Reset the output to default MSBuild colours for the context.
    /// </summary>
    /// <param name="context">Output <see cref="MSBuildContext"/>.</param>
    /// <param name="output">Output destination.</param>
    public abstract void Reset(MSBuildContext context, TextWriter output);

    /// <param name="context">Output <see cref="MSBuildContext"/>.</param>
    /// <returns>The number of characters written by the <see cref="Reset(MSBuildContext,TextWriter)"/> method.</returns>
    protected abstract int GetResetCharCount(MSBuildContext context);

    internal MSBuildStyleReset Apply(MSBuildContext context, TextWriter output, MSBuildConsoleThemeStyle style, ref int invisibleCharacterCount)
    {
        invisibleCharacterCount += Set(context, output, style);
        invisibleCharacterCount += GetResetCharCount(context);

        return new MSBuildStyleReset(this, context, output);
    }
}
