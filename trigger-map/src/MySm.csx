#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.2-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;

private SmRunner runner = new(diagramPath: "MySm.drawio.svg", new MyRenderConfig(), transpilerId: TranspilerId.JavaScript);
runner.Run();

public class MyRenderConfig : IRenderConfigJavaScript
{
    public class MyExpansions : UserExpansionScriptBase
    {
        public string log_unhandled_event() => $"""log_unhandled_event("{CurrentTrigger.ToUpper()}")"""; // CurrentTrigger returns trigger in lower case so we convert it to upper case.
    }
}