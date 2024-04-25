#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.10-alpha"

using StateSmith.Input.Expansions;
using StateSmith.Output.Gil.C99;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

// See https://github.com/StateSmith/tutorial-2/blob/main/lesson-1/
SmRunner runner = new(diagramPath: "ButtonSm1Cpp.puml", new MyRenderConfig(), transpilerId: TranspilerId.C99);
runner.Settings.outputDirectory = "src";
runner.Run();

//---------------------------------------------------------------------------------------------------------------------

// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-2
public class MyRenderConfig : IRenderConfigC
{
    // NOTE!!! Idiomatic C++ code generation is coming. This will improve.
    // See https://github.com/StateSmith/StateSmith/issues/126
    string IRenderConfigC.CFileExtension => ".cpp"; // the generated StateSmith C code is also valid C++ code
    string IRenderConfigC.HFileExtension => ".h";   // could also be .hh, .hpp or whatever you like
    string IRenderConfigC.CEnumDeclarer => "typedef enum __attribute__((packed)) {enumName}";   // save RAM by packing enum type to smallest int type

    string IRenderConfigC.CFileIncludes => """
        #include "Arduino.h"
        """;

    string IRenderConfig.VariableDeclarations => """
        // Note! This example below uses bit fields just to show that you can. They aren't required.

        // This can be made to be 11 bits if RAM is at a premium. See laser tag menu example.
        uint32_t debounce_started_at_ms;

        uint16_t input_is_pressed : 1;     // input
        uint16_t output_event_press : 1;   // output
        uint16_t output_event_release : 1; // output
        uint16_t output_event_held : 1;    // output
        uint16_t output_event_tap : 1;     // output
        """;

    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class Expansions : UserExpansionScriptBase
    {
        public string time_ms => $"millis()";   // directly calls Arduino C++ code

        public string is_pressed => VarsPath + "input_" + AutoNameCopy(); // ends up as "input_is_pressed"
        public string is_released => $"(!{is_pressed})";

        public string output_event(string eventName) => $"{VarsPath}output_event_{eventName.ToLower()} = true";

        public string debounce_started_at_ms => AutoVarName();
        public string reset_debounce_timer() => $"{debounce_started_at_ms} = {time_ms}";
        public string debounce_ms() => $"({time_ms} - {debounce_started_at_ms})";       // unsigned math work even with ms roll over
        public string after_debounce_ms(string ms) => $"( {debounce_ms()} >= {ms} )";

        public string is_debounced => $"({after_debounce_ms("20")})";

        // Expansion for PlantUML. I can't get it to render multiline action code nicely (it center aligns it).
        public string release_events() => $$"""
            if ({{debounce_ms()}} <= 200) {
                {{output_event("tap")}};
            }
            {{output_event("release")}}
            """;
    }
}
