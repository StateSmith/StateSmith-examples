#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.6-alpha"

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;

// See https://github.com/StateSmith/tutorial-2/blob/main/lesson-1/
SmRunner runner = new(diagramPath: "WaterCannonSm.drawio", new MyRenderConfig(), transpilerId: TranspilerId.C99);
runner.Run();

// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-2
public class MyRenderConfig : IRenderConfig
{
    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class MyExpansions : UserExpansionScriptBase
    {
        
    }
}
