/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/SpanIdTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.Globalization;
using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Rendering;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class TimestampTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly PropertyToken _token;
    readonly IFormatProvider? _formatProvider;

    public TimestampTokenRenderer(MSBuildConsoleTheme theme, PropertyToken token, IFormatProvider? formatProvider)
    {
        _theme = theme;
        _token = token;
        _formatProvider = formatProvider;
    }

    public override void Render(LogEvent logEvent, MSBuildContext context, TextWriter output)
    {
        var sv = new DateTimeOffsetValue(logEvent.Timestamp);

        var _ = 0;
        using (_theme.Apply(context, output, MSBuildConsoleThemeStyle.SecondaryText, ref _))
        {
            if (_token.Alignment is null)
            {
                sv.Render(output, _token.Format, _formatProvider);
            }
            else
            {
                var buffer = new StringWriter();
                sv.Render(buffer, _token.Format, _formatProvider);
                var str = buffer.ToString();
                Padding.Apply(output, str, _token.Alignment);
            }
        }
    }

    readonly struct DateTimeOffsetValue
    {
        public DateTimeOffsetValue(DateTimeOffset value)
        {
            Value = value;
        }

        public DateTimeOffset Value { get; }

        public void Render(TextWriter output, string? format = null, IFormatProvider? formatProvider = null)
        {
            var custom = (ICustomFormatter?)formatProvider?.GetFormat(typeof(ICustomFormatter));
            if (custom != null)
            {
                output.Write(custom.Format(format, Value, formatProvider));
                return;
            }

#if FEATURE_SPAN
                Span<char> buffer = stackalloc char[32];
                if (Value.TryFormat(buffer, out int written, format, formatProvider ?? CultureInfo.InvariantCulture))
                    output.Write(buffer.Slice(0, written));
                else
                    output.Write(Value.ToString(format, formatProvider ?? CultureInfo.InvariantCulture));
#else
            output.Write(Value.ToString(format, formatProvider ?? CultureInfo.InvariantCulture));
#endif
        }
    }
}
