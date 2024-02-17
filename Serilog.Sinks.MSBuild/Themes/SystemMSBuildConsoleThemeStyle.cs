/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/SystemConsoleThemeStyle.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;

namespace Serilog.Sinks.MSBuild.Themes
{
    /// <summary>
    /// Styling applied using the <see cref="System.ConsoleColor"/> enumeration.
    /// </summary>
    public struct SystemMSBuildConsoleThemeStyle
    {
        /// <summary>
        /// The foreground color to apply.
        /// </summary>
        public ConsoleColor? Foreground;

        /// <summary>
        /// The background color to apply.
        /// </summary>
        public ConsoleColor? Background;
    }
}
