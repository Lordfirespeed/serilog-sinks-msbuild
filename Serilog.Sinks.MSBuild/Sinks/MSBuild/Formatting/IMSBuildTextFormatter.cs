/*
 * This file is based upon
 * https://github.com/serilog/serilog/blob/653260089620944516cbd874449f3c4bd02edc0e/src/Serilog/Formatting/ITextFormatter.cs
 * Copyright 2013-2015 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.IO;
using Serilog.Events;

namespace Serilog.Sinks.MSBuild.Formatting;

/// <summary>
/// Formats MSBuild log events in a textual representation.
/// </summary>
public interface IMSBuildTextFormatter
{
    /// <summary>
    /// Format the log event into the output.
    /// </summary>
    /// <param name="logEvent">The event to format.</param>
    /// <param name="context">Output <see cref="MSBuildContext"/>.</param>
    /// <param name="output">The output.</param>
    void Format(LogEvent logEvent, MSBuildContext context, TextWriter output);
}
