/*
 * Copyright (c) 2024 Joe Clack
 * Joe Clack licenses this file to you under the LGPL-3.0-OR-LATER license.
 */

using System.Globalization;
using System.IO;
using Serilog.Sinks.MSBuild.Output;
using Serilog.Sinks.MSBuild.Tests.Support;
using Serilog.Sinks.MSBuild.Themes;

namespace Serilog.Sinks.MSBuild.Tests.Output;

public class OutputTemplateRendererTests
{
    [Fact]
    public void BasicMessageRenders()
    {
        var formatter = new OutputTemplateRenderer(MSBuildConsoleTheme.None, "{Message}", CultureInfo.InvariantCulture);
        var @event = DelegatingSink.GetLogEvent(l => l.Information("{Message}", "Hello, world!"));
        var context = MSBuildContext.FromLogEvent(@event);
        var writer = new StringWriter();
        formatter.Format(@event, context, writer);
        var formatted = writer.ToString();
        Assert.Equal(formatted, formatted.Trim());
    }
}
