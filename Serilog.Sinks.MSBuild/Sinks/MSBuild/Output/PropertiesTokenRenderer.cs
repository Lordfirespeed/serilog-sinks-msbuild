/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/PropertiesTokenRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Formatting;
using Serilog.Sinks.MSBuild.Rendering;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class PropertiesTokenRenderer : OutputTemplateTokenRenderer
{
    readonly MessageTemplate _outputTemplate;
    readonly MSBuildConsoleTheme _theme;
    readonly PropertyToken _token;
    readonly ThemedValueFormatter _valueFormatter;

    public PropertiesTokenRenderer(MSBuildConsoleTheme theme, PropertyToken token, MessageTemplate outputTemplate, IFormatProvider? formatProvider)
    {
        _outputTemplate = outputTemplate;
        _theme = theme ?? throw new ArgumentNullException(nameof(theme));
        _token = token ?? throw new ArgumentNullException(nameof(token));

        var isJson = false;

        if (token.Format != null)
        {
            for (var i = 0; i < token.Format.Length; ++i)
            {
                if (token.Format[i] == 'j')
                    isJson = true;
            }
        }

        _valueFormatter = isJson
            ? (ThemedValueFormatter)new ThemedJsonValueFormatter(theme, formatProvider)
            : new ThemedDisplayValueFormatter(theme, formatProvider);
    }

    public override void Render(LogEvent logEvent, MSBuildContext context, TextWriter output)
    {
        var included = logEvent.Properties
            .Where(p => !TemplateContainsPropertyName(logEvent.MessageTemplate, p.Key) &&
                        !TemplateContainsPropertyName(_outputTemplate, p.Key))
            .Select(p => new LogEventProperty(p.Key, p.Value));

        var value = new StructureValue(included);

        if (_token.Alignment is null || !_theme.CanBuffer)
        {
            _valueFormatter.Format(value, context, output, null);
            return;
        }

        var buffer = new StringWriter(new StringBuilder(value.Properties.Count * 16));
        var invisible = _valueFormatter.Format(value, context, buffer, null);
        var str = buffer.ToString();
        Padding.Apply(output, str, _token.Alignment.Value.Widen(invisible));
    }

    static bool TemplateContainsPropertyName(MessageTemplate template, string propertyName)
    {
        foreach (var token in template.Tokens)
        {
            if (token is PropertyToken namedProperty &&
                namedProperty.PropertyName == propertyName)
            {
                return true;
            }
        }

        return false;
    }
}
