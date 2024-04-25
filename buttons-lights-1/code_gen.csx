#!/usr/bin/env dotnet-script
// This is a c# script file

#r "nuget: StateSmith, 0.9.10-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;


// Run code generation for button first
// NOTE!!! Each state machine has its own render config!
SmRunner runner = new(diagramPath: "ButtonLightSm.drawio.svg", new ButtonRenderConfig(), transpilerId: TranspilerId.C99);
runner.Settings.stateMachineName = "ButtonSm";  // this is needed because the diagram has two state machines in it
runner.Run();

// Run code generation for light state machine next
// NOTE!!! Each state machine has its own render config!
runner = new(diagramPath: "ButtonLightSm.drawio.svg", new LightRenderConfig(), transpilerId: TranspilerId.C99);
runner.Settings.stateMachineName = "LightSm";  // this is needed because the diagram has two state machines in it
runner.Run();

//---------------------------------------------------------------------------------------------------------------------------------------------------------------

// ignore C# guidelines for script stuff below
#pragma warning disable IDE1006, CA1050 



// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-2
public class ButtonRenderConfig : IRenderConfigC
{
    string IRenderConfigC.CFileIncludes => """
        #include "Arduino.h"
        """;

    string IRenderConfigC.CFileExtension => ".cpp";
    string IRenderConfigC.HFileExtension => ".hpp";
    string IRenderConfigC.CEnumDeclarer => "typedef enum __attribute__((packed)) {enumName}";

    string IRenderConfig.VariableDeclarations => """
        // Note! This example below uses bit fields just to show that you can. They aren't required.

        // This can be made to be 11 bits if RAM is at a premium. See laser tag menu example.
        uint32_t timer_started_at_ms;

        uint16_t input_is_pressed : 1;     // input
        uint16_t output_event_press : 1;   // output
        uint16_t output_event_release : 1; // output
        uint16_t output_event_long : 1;    // output
        """;

    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class Expansions : UserExpansionScriptBase
    {
        // inputs
        public string is_pressed => VarsPath + "input_is_pressed";
        public string is_released => $"(!{is_pressed})";

        // outputs
        public string output_event(string eventName) => $"{VarsPath}output_event_{eventName.ToLower()} = true";

        // time stuff
        public string now_ms => $"millis()";   // directly calls Arduino C++ code
        public string timer_started_at_ms => AutoVarName();
        public string timer_ms => $"({now_ms} - {timer_started_at_ms})";   // unsigned math works even with ms roll over
        public string reset_timer() => $"{timer_started_at_ms} = {now_ms}";

        public string is_debounced => $"({timer_ms} >= 100)";
    }
}



// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-2
public class LightRenderConfig : IRenderConfigC
{
    string IRenderConfigC.CFileIncludes => """
        #include "LightController.hpp"
        """;

    string IRenderConfigC.CFileExtension => ".cpp";
    string IRenderConfigC.HFileExtension => ".hpp";
    string IRenderConfigC.CEnumDeclarer => "typedef enum __attribute__((packed)) {enumName}";

    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class Expansions : UserExpansionScriptBase
    {
        public string light_off() => $"LightController::{AutoNameCopy()}()"; // ends up being `LightController::light_off()`
        public string light_1() => $"LightController::{AutoNameCopy()}()";
        public string light_2() => $"LightController::{AutoNameCopy()}()";
        public string light_3() => $"LightController::{AutoNameCopy()}()";
    }
}
