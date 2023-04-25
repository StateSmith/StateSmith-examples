#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.2-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;

SmRunner runner = new(diagramPath: "LightSm.puml", new LightSmRenderConfig(), transpilerId: TranspilerId.JavaScript);
runner.Run();

///////////////////////////////////////////////////////////////////////////////////////

// This class gives StateSmith the info it needs to generate working code.
public class LightSmRenderConfig : IRenderConfigJavaScript
{
    string IRenderConfig.AutoExpandedVars => """
        count : 0, // variable for state machine
        """;
}
