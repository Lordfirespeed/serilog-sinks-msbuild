/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Themes/ConsoleThemeStyle.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */
using System;
using System.ComponentModel;

namespace Serilog.Sinks.MSBuild.Themes
{
    /// <summary>
    /// Elements styled by a console theme.
    /// </summary>
    public enum MSBuildConsoleThemeStyle
    {
        /// <summary>
        /// Prominent text, generally content within an event's message.
        /// </summary>
        Text,

        /// <summary>
        /// Boilerplate text, for example items specified in an output template.
        /// </summary>
        SecondaryText,

        /// <summary>
        /// De-emphasized text, for example literal text in output templates and
        /// punctuation used when writing structured data.
        /// </summary>
        TertiaryText,

        /// <summary>
        /// Output demonstrating some kind of configuration issue, e.g. an invalid
        /// message template token.
        /// </summary>
        Invalid,

        /// <summary>
        /// The built-in <see langword="null"/> value.
        /// </summary>
        Null,

        /// <summary>
        /// Property and type names.
        /// </summary>
        Name,

        /// <summary>
        /// Strings.
        /// </summary>
        String,

        /// <summary>
        /// Numbers.
        /// </summary>
        Number,

        /// <summary>
        /// <see cref="bool"/> values.
        /// </summary>
        Boolean,

        /// <summary>
        /// All other scalar values, e.g. <see cref="System.Guid"/> instances.
        /// </summary>
        Scalar,

        /// <summary>
        /// Unrecognized literal values, e.g. <see cref="System.Guid"/> instances.
        /// </summary>
        [Obsolete("Use MSBuildConsoleThemeStyle.Scalar instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        Object = Scalar,

        /// <summary>
        /// Level indicator.
        /// </summary>
        LevelVerbose,

        /// <summary>
        /// Level indicator.
        /// </summary>
        LevelDebug,

        /// <summary>
        /// Level indicator.
        /// </summary>
        LevelInformation,

        /// <summary>
        /// Level indicator.
        /// </summary>
        LevelWarning,

        /// <summary>
        /// Level indicator.
        /// </summary>
        LevelError,

        /// <summary>
        /// Level indicator.
        /// </summary>
        LevelFatal,
    }
}
