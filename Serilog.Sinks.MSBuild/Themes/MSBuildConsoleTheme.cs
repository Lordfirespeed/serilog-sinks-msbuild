﻿/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/ConsoleTheme.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;

namespace Serilog.Sinks.MSBuild.Themes
{
    /// <summary>
    /// The base class for styled terminal output.
    /// </summary>
    public abstract class MSBuildConsoleTheme
    {
        /// <summary>
        /// No styling applied.
        /// </summary>
        public static MSBuildConsoleTheme None { get; } = new EmptyMsBuildConsoleTheme();

        /// <summary>
        /// True if styling applied by the theme is written into the output, and can thus be
        /// buffered and measured.
        /// </summary>
        public abstract bool CanBuffer { get; }

        /// <summary>
        /// Begin a span of text in the specified <paramref name="style"/>.
        /// </summary>
        /// <param name="output">Output destination.</param>
        /// <param name="style">Style to apply.</param>
        /// <returns> The number of characters written to <paramref name="output"/>. </returns>
        public abstract int Set(TextWriter output, MSBuildConsoleThemeStyle style);

        /// <summary>
        /// Reset the output to un-styled colors.
        /// </summary>
        /// <param name="output">Output destination.</param>
        public abstract void Reset(TextWriter output);

        /// <summary>
        /// The number of characters written by the <see cref="Reset(TextWriter)"/> method.
        /// </summary>
        protected abstract int ResetCharCount { get; }

        internal StyleReset Apply(TextWriter output, MSBuildConsoleThemeStyle style, ref int invisibleCharacterCount)
        {
            invisibleCharacterCount += Set(output, style);
            invisibleCharacterCount += ResetCharCount;

            return new StyleReset(this, output);
        }
    }
}
