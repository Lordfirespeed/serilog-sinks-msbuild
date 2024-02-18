/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Rendering/ThemedMessageTemplateRenderer.cs
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
using Serilog.Parsing;
using Serilog.Sinks.MSBuild.Formatting;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Rendering;

class ThemedMessageTemplateRenderer
{
    readonly MSBuildConsoleTheme _theme;
    readonly ThemedValueFormatter _valueFormatter;
    readonly bool _isLiteral;
    static readonly MSBuildConsoleTheme NoTheme = new EmptyMSBuildConsoleTheme();
    readonly ThemedValueFormatter _unthemedValueFormatter;

    public ThemedMessageTemplateRenderer(MSBuildConsoleTheme theme, ThemedValueFormatter valueFormatter, bool isLiteral)
    {
        _theme = theme ?? throw new ArgumentNullException(nameof(theme));
        _valueFormatter = valueFormatter;
        _isLiteral = isLiteral;
        _unthemedValueFormatter = valueFormatter.SwitchTheme(NoTheme);
    }

    public int Render(MessageTemplate template, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        var count = 0;
        foreach (var token in template.Tokens)
        {
            if (token is TextToken tt)
            {
                count += RenderTextToken(tt, properties, output);
            }
            else
            {
                var pt = (PropertyToken)token;
                count += RenderPropertyToken(pt, properties, output);
            }
        }
        return count;
    }

    int RenderTextToken(TextToken tt, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        var count = 0;
        using (_theme.Apply(output, GetDefaultedContextTextThemeStyle(), ref count))
            output.Write(tt.Text);
        return count;

        MSBuildConsoleThemeStyle GetDefaultedContextTextThemeStyle()
            => GetContextTextThemeStyle() ?? MessageClass.Default.Style;

        MSBuildConsoleThemeStyle? GetContextTextThemeStyle()
        {
            if (!properties.TryGetValue(MessageClass.PropertyName, out var propertyValue))
                return null;

            if (propertyValue is not ScalarValue scalarValue)
                return null;

            if (scalarValue.Value is MessageClass @class)
                return @class.Style;

            return null;
        }
    }

    int RenderPropertyToken(PropertyToken pt, IReadOnlyDictionary<string, LogEventPropertyValue> properties, TextWriter output)
    {
        if (!properties.TryGetValue(pt.PropertyName, out var propertyValue))
        {
            var count = 0;
            using (_theme.Apply(output, MSBuildConsoleThemeStyle.Invalid, ref count))
                output.Write(pt.ToString());
            return count;
        }

        if (!pt.Alignment.HasValue)
        {
            return RenderValue(_theme, _valueFormatter, propertyValue, output, pt.Format);
        }

        var valueOutput = new StringWriter();

        if (!_theme.CanBuffer)
            return RenderAlignedPropertyTokenUnbuffered(pt, output, propertyValue);

        var invisibleCount = RenderValue(_theme, _valueFormatter, propertyValue, valueOutput, pt.Format);

        var value = valueOutput.ToString();

        if (value.Length - invisibleCount >= pt.Alignment.Value.Width)
        {
            output.Write(value);
        }
        else
        {
            Padding.Apply(output, value, pt.Alignment.Value.Widen(invisibleCount));
        }

        return invisibleCount;
    }

    int RenderAlignedPropertyTokenUnbuffered(PropertyToken pt, TextWriter output, LogEventPropertyValue propertyValue)
    {
        if (pt.Alignment == null) throw new ArgumentException("The PropertyToken should have a non-null Alignment.", nameof(pt));

        var valueOutput = new StringWriter();
        RenderValue(NoTheme, _unthemedValueFormatter, propertyValue, valueOutput, pt.Format);

        var valueLength = valueOutput.ToString().Length;
        if (valueLength >= pt.Alignment.Value.Width)
        {
            return RenderValue(_theme, _valueFormatter, propertyValue, output, pt.Format);
        }

        if (pt.Alignment.Value.Direction == AlignmentDirection.Left)
        {
            var invisible = RenderValue(_theme, _valueFormatter, propertyValue, output, pt.Format);
            Padding.Apply(output, string.Empty, pt.Alignment.Value.Widen(-valueLength));
            return invisible;
        }

        Padding.Apply(output, string.Empty, pt.Alignment.Value.Widen(-valueLength));
        return RenderValue(_theme, _valueFormatter, propertyValue, output, pt.Format);
    }

    int RenderValue(MSBuildConsoleTheme theme, ThemedValueFormatter valueFormatter, LogEventPropertyValue propertyValue, TextWriter output, string? format)
    {
        if (_isLiteral && propertyValue is ScalarValue sv && sv.Value is string)
        {
            var count = 0;
            using (theme.Apply(output, MSBuildConsoleThemeStyle.String, ref count))
                output.Write(sv.Value);
            return count;
        }

        return valueFormatter.Format(propertyValue, output, format, _isLiteral);
    }
}
