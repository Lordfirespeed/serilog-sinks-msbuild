/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/MessageTemplateOutputTokenRenderer.cs
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
using Serilog.Sinks.MSBuild.Formatting;
using Serilog.Sinks.MSBuild.Rendering;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class MessageTemplateOutputTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly PropertyToken _token;
    readonly ThemedMessageTemplateRenderer _renderer;

    public MessageTemplateOutputTokenRenderer(MSBuildConsoleTheme theme, PropertyToken token, IFormatProvider? formatProvider)
    {
        _theme = theme ?? throw new ArgumentNullException(nameof(theme));
        _token = token ?? throw new ArgumentNullException(nameof(token));

        bool isLiteral = false, isJson = false;

        if (token.Format != null)
        {
            for (var i = 0; i < token.Format.Length; ++i)
            {
                if (token.Format[i] == 'l')
                    isLiteral = true;
                else if (token.Format[i] == 'j')
                    isJson = true;
            }
        }

        var valueFormatter = isJson
            ? (ThemedValueFormatter)new ThemedJsonValueFormatter(theme, formatProvider)
            : new ThemedDisplayValueFormatter(theme, formatProvider);

        _renderer = new ThemedMessageTemplateRenderer(theme, valueFormatter, isLiteral);
    }

    public override void Render(LogEvent logEvent, TextWriter output)
    {
        if (_token.Alignment is null || !_theme.CanBuffer)
        {
            _renderer.Render(logEvent.MessageTemplate, logEvent.Properties, output);
            return;
        }

        var buffer = new StringWriter();
        var invisible = _renderer.Render(logEvent.MessageTemplate, logEvent.Properties, buffer);
        var value = buffer.ToString();
        Padding.Apply(output, value, _token.Alignment.Value.Widen(invisible));
    }
}
