#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.6-alpha"

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;

// See https://github.com/StateSmith/tutorial-2/blob/main/lesson-1/
SmRunner runner = new(diagramPath: "MySm.drawio.svg", new MyRenderConfig(), transpilerId: TranspilerId.C99);
runner.Run();

// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-2
public class MyRenderConfig : IRenderConfigC
{
    string IRenderConfigC.CFileIncludes => """
        #include <stdint.h>  // for t1_start_ms
        #include "Leds.h"
        #include "esp_timer.h"
        """;

    string IRenderConfig.VariableDeclarations => """
        uint32_t t1_start_ms;  // ms since boot when timer 1 was started
        """;

    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class MyExpansions : UserExpansionScriptBase
    {
        string t1_start_ms => AutoVarName();    // expansion to get the name of the variable field
        string t1_ms() => $"({ms_since_boot} - {t1_start_ms})"; // expansion to get the time since timer 1 started
        string t1_restart() => $"{t1_start_ms} = {ms_since_boot}";

        string ms_since_boot = "((uint32_t)esp_timer_get_time()/1000)"; // esp_timer_get_time() returns microseconds since boot in i64
    }
}
