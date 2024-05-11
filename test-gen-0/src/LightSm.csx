#!/usr/bin/env dotnet-script
// This is a c# script file

#r "nuget: StateSmith, 0.9.12-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;  // Note using! This is required to access StateMachine and NamedVertex classes...

SmRunner runner = new(diagramPath: "LightSm.drawio.svg", new LightSmRenderConfig(), transpilerId: TranspilerId.JavaScript);
AddPipelineStep();
runner.Run();


/////////////////////////////////////////////////////////////////////////////////////////

void AddPipelineStep()
{
    // This method adds your custom step into the StateSmith transformation pipeline.
    // Some more info here: https://github.com/StateSmith/StateSmith/wiki/How-StateSmith-Works
    runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1, 
                                                new TransformationStep(id: "my custom step blah", PrintSmInfo));
}

// This shows roughly how to inspect a state machine.
// This same idea could be used to generate test scaffolding code or to generate documentation.
void PrintSmInfo(StateMachine sm)
{
    Console.WriteLine($"");
    BehaviorDescriber describer = new(singleLineFormat: true);

    InitialState rootInitialState = sm.ChildType<InitialState>();
    Console.WriteLine($"Root initial state behaviors:");
    PrintBehaviors(rootInitialState, describer);
    Console.WriteLine($"");

    // The below code will visit all states in the state machine.
    // A more complete solution would also visit pseudo states like initial, choice, enter and exit points.
    // Let's start simple for now and ignore pseudo states.
    sm.VisitTypeRecursively((State state) =>
    {
        Console.WriteLine($"State {state.Name} behaviors:");
        // could use name to generate a test function name

        PrintBehaviors(state, describer);
        Console.WriteLine("");
    });
}

// Note that this function takes a vertex, which is a base class for states and pseudo states.
static void PrintBehaviors(Vertex vertex, BehaviorDescriber describer)
{
    foreach (var behavior in vertex.Behaviors)
    {
        // To learn how to inspect a behavior for transitions, guards, ...
        // see https://github.com/StateSmith/StateSmith/blob/41d7b6bb663dac3d99c0223a15dda3638cd21548/src/StateSmith/SmGraph/BehaviorDescriber.cs#L8
        Console.WriteLine("    " + describer.Describe(behavior));
    }
}








/////////////////////////////////////////////////////////////////////////////////////////
// NOTHING NOTABLE BELOW THIS LINE
/////////////////////////////////////////////////////////////////////////////////////////



// This class gives StateSmith the info it needs to generate working C code.
// It adds user code to the generated .c/.h files, declares user variables,
// and provides diagram code expansions. This class can have any name.
public class LightSmRenderConfig : IRenderConfigJavaScript
{
    bool IRenderConfigJavaScript.UseExportOnClass => true;

    string IRenderConfig.AutoExpandedVars => """
        count: 0 // variable for state machine
        """;

    
    // This nested class creates expansions. It can have any name.
    public class MyExpansions : UserExpansionScriptBase
    {
    }
}
