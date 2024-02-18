/*
 * This file is based upon
 * https://github.com/serilog/serilog-sinks-console/blob/1f0919ed791159781c234851d338619c18a505ff/src/Serilog.Sinks.Console/Sinks/SystemConsole/Formatting/ThemedDisplayValueFormatter.cs
 * Copyright 2017 Serilog Contributors
 * Serilog Contributors license the referenced file to Joe Clack under the terms of the Apache-2.0 license.
 *
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System;
using System.IO;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Formatting;

class ThemedDisplayValueFormatter : ThemedValueFormatter
{
    readonly IFormatProvider? _formatProvider;

    public ThemedDisplayValueFormatter(MSBuildConsoleTheme theme, IFormatProvider? formatProvider)
        : base(theme)
    {
        _formatProvider = formatProvider;
    }

    public override ThemedValueFormatter SwitchTheme(MSBuildConsoleTheme theme)
    {
        return new ThemedDisplayValueFormatter(theme, _formatProvider);
    }

    protected override int VisitScalarValue(ThemedValueFormatterState state, ScalarValue scalar)
    {
        if (scalar is null)
            throw new ArgumentNullException(nameof(scalar));
        return FormatLiteralValue(scalar, state.Context, state.Output, state.Format);
    }

    protected override int VisitSequenceValue(ThemedValueFormatterState state, SequenceValue sequence)
    {
        if (sequence is null)
            throw new ArgumentNullException(nameof(sequence));

        var count = 0;

        using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
            state.Output.Write('[');

        var delim = string.Empty;
        for (var index = 0; index < sequence.Elements.Count; ++index)
        {
            if (delim.Length != 0)
            {
                using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
                    state.Output.Write(delim);
            }

            delim = ", ";
            Visit(state, sequence.Elements[index]);
        }

        using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
            state.Output.Write(']');

        return count;
    }

    protected override int VisitStructureValue(ThemedValueFormatterState state, StructureValue structure)
    {
        var count = 0;

        if (structure.TypeTag != null)
        {
            using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.Name, ref count))
                state.Output.Write(structure.TypeTag);

            state.Output.Write(' ');
        }

        using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
            state.Output.Write('{');

        var delim = string.Empty;
        for (var index = 0; index < structure.Properties.Count; ++index)
        {
            if (delim.Length != 0)
            {
                using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
                    state.Output.Write(delim);
            }

            delim = ", ";

            var property = structure.Properties[index];

            using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.Name, ref count))
                state.Output.Write(property.Name);

            using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('=');

            count += Visit(state.Nest(), property.Value);
        }

        using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
            state.Output.Write('}');

        return count;
    }

    protected override int VisitDictionaryValue(ThemedValueFormatterState state, DictionaryValue dictionary)
    {
        var count = 0;

        using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
            state.Output.Write('{');

        var delim = string.Empty;
        foreach (var element in dictionary.Elements)
        {
            if (delim.Length != 0)
            {
                using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
                    state.Output.Write(delim);
            }

            delim = ", ";

            using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write('[');

            using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.String, ref count))
                count += Visit(state.Nest(), element.Key);

            using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
                state.Output.Write("]=");

            count += Visit(state.Nest(), element.Value);
        }

        using (ApplyStyle(state.Context, state.Output, MSBuildConsoleThemeStyle.TertiaryText, ref count))
            state.Output.Write('}');

        return count;
    }

    public int FormatLiteralValue(ScalarValue scalar, MSBuildContext context, TextWriter output, string? format)
    {
        var value = scalar.Value;
        var count = 0;

        if (value is null)
        {
            using (ApplyStyle(context, output, MSBuildConsoleThemeStyle.Null, ref count))
                output.Write("null");
            return count;
        }

        if (value is string str)
        {
            using (ApplyStyle(context, output, MSBuildConsoleThemeStyle.String, ref count))
            {
                if (format != "l")
                    JsonValueFormatter.WriteQuotedJsonString(str, output);
                else
                    output.Write(str);
            }
            return count;
        }

        if (value is ValueType)
        {
            if (value is int || value is uint || value is long || value is ulong ||
                value is decimal || value is byte || value is sbyte || value is short ||
                value is ushort || value is float || value is double)
            {
                using (ApplyStyle(context, output, MSBuildConsoleThemeStyle.Number, ref count))
                    scalar.Render(output, format, _formatProvider);
                return count;
            }

            if (value is bool b)
            {
                using (ApplyStyle(context, output, MSBuildConsoleThemeStyle.Boolean, ref count))
                    output.Write(b);

                return count;
            }

            if (value is char ch)
            {
                using (ApplyStyle(context, output, MSBuildConsoleThemeStyle.Scalar, ref count))
                {
                    output.Write('\'');
                    output.Write(ch);
                    output.Write('\'');
                }
                return count;
            }
        }

        using (ApplyStyle(context, output, MSBuildConsoleThemeStyle.Scalar, ref count))
            scalar.Render(output, format, _formatProvider);

        return count;
    }
}
