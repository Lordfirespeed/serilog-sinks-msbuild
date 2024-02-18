/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/EventPropertyTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.IO;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Rendering;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class EventPropertyTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly PropertyToken _token;
    readonly IFormatProvider? _formatProvider;

    public EventPropertyTokenRenderer(MSBuildConsoleTheme theme, PropertyToken token, IFormatProvider? formatProvider)
    {
        _theme = theme;
        _token = token;
        _formatProvider = formatProvider;
    }

    public override void Render(LogEvent logEvent, MSBuildContext context, TextWriter output)
    {
        // If a property is missing, don't render anything (message templates render the raw token here).
        if (!logEvent.Properties.TryGetValue(_token.PropertyName, out var propertyValue))
        {
            Padding.Apply(output, string.Empty, _token.Alignment);
            return;
        }

        var _ = 0;
        using (_theme.Apply(context, output, MSBuildConsoleThemeStyle.SecondaryText, ref _))
        {
            var writer = _token.Alignment.HasValue ? new StringWriter() : output;

            // If the value is a scalar string, support some additional formats: 'u' for uppercase
            // and 'w' for lowercase.
            if (propertyValue is ScalarValue sv && sv.Value is string literalString)
            {
                var cased = Casing.Format(literalString, _token.Format);
                writer.Write(cased);
            }
            else
            {
                propertyValue.Render(writer, _token.Format, _formatProvider);
            }

            if (_token.Alignment.HasValue)
            {
                var str = writer.ToString()!;
                Padding.Apply(output, str, _token.Alignment);
            }
        }
    }
}
