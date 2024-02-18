/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/test/Serilog.Sinks.Console.Tests/Support/DelegatingSink.cs
 * Copyright 2019 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.MSBuild.Tests.Support;

public class DelegatingSink : ILogEventSink
{
    readonly Action<LogEvent> _write;

    public DelegatingSink(Action<LogEvent> write)
    {
        _write = write ?? throw new ArgumentNullException(nameof(write));
    }

    public void Emit(LogEvent logEvent)
    {
        _write(logEvent);
    }

    public static LogEvent GetLogEvent(Action<ILogger> writeAction)
    {
        LogEvent? result = null;
        var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Sink(new DelegatingSink(le => result = le))
            .CreateLogger();

        writeAction(logger);
        return result!;
    }
}
