/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Output/OutputTemplateRenderer.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Output;

class OutputTemplateRenderer : ITextFormatter
{
    readonly OutputTemplateTokenRenderer[] _renderers;

    public OutputTemplateRenderer(MSBuildConsoleTheme theme, string outputTemplate, IFormatProvider? formatProvider)
    {
        if (outputTemplate is null) throw new ArgumentNullException(nameof(outputTemplate));
        var template = new MessageTemplateParser().Parse(outputTemplate);

        var renderers = new List<OutputTemplateTokenRenderer>();
        foreach (var token in template.Tokens)
        {
            if (token is TextToken tt)
            {
                renderers.Add(new TextTokenRenderer(theme, tt.Text));
                continue;
            }

            var pt = (PropertyToken)token;
            if (pt.PropertyName == OutputProperties.LevelPropertyName)
            {
                renderers.Add(new LevelTokenRenderer(theme, pt));
            }
            else if (pt.PropertyName == OutputProperties.NewLinePropertyName)
            {
                renderers.Add(new NewLineTokenRenderer(pt.Alignment));
            }
            else if (pt.PropertyName == OutputProperties.TraceIdPropertyName)
            {
                renderers.Add(new TraceIdTokenRenderer(theme, pt));
            }
            else if (pt.PropertyName == OutputProperties.SpanIdPropertyName)
            {
                renderers.Add(new SpanIdTokenRenderer(theme, pt));
            }
            else if (pt.PropertyName == OutputProperties.ExceptionPropertyName)
            {
                renderers.Add(new ExceptionTokenRenderer(theme, pt));
            }
            else if (pt.PropertyName == OutputProperties.MessagePropertyName)
            {
                renderers.Add(new MessageTemplateOutputTokenRenderer(theme, pt, formatProvider));
            }
            else if (pt.PropertyName == OutputProperties.TimestampPropertyName)
            {
                renderers.Add(new TimestampTokenRenderer(theme, pt, formatProvider));
            }
            else if (pt.PropertyName == OutputProperties.PropertiesPropertyName)
            {
                renderers.Add(new PropertiesTokenRenderer(theme, pt, template, formatProvider));
            }
            else
            {
                renderers.Add(new EventPropertyTokenRenderer(theme, pt, formatProvider));
            }
        }

        _renderers = renderers.ToArray();
    }

    public void Format(LogEvent logEvent, TextWriter output)
    {
        if (logEvent is null) throw new ArgumentNullException(nameof(logEvent));
        if (output is null) throw new ArgumentNullException(nameof(output));

        foreach (var renderer in _renderers)
            renderer.Render(logEvent, output);
    }
}
