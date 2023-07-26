#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.7-alpha"

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph; // required to access StateMachine and NamedVertex classes

// See https://github.com/StateSmith/tutorial-2/blob/main/lesson-1/
SmRunner runner = new(diagramPath: "MySm.drawio.svg", transpilerId: TranspilerId.C99);
AddLoggingTransformationStep();
runner.Run();

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

void AddLoggingTransformationStep()
{
    // This method adds your custom logging transformation step into the StateSmith transformation pipeline.
    // Some more info here: https://github.com/StateSmith/StateSmith/wiki/How-StateSmith-Works
    runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1, 
                                                new TransformationStep(id: "my custom logger", LoggingTransformationStep));
}

void LoggingTransformationStep(StateMachine sm)
{
    // The below code will visit all states in the state machine and add custom enter and exit behaviors.
    sm.VisitTypeRecursively((State state) =>
    {
        state.AddEnterAction($"printf(\"--> Entered {state.Name}.\\n\");", index:0); // use index to insert at start
        // NOTE! AddEnterAction() `index` argument was added in release 0.9.7. If you are on an older release, you can use the line below:
        // state.AddBehavior(index:0, behavior: Behavior.NewEnterBehavior($"printf(\"--> Entered {state.Name}.\\n\");"));

        state.AddExitAction($"printf(\"<-- Exited {state.Name}.\\n\");"); // behavior added to end

        // NOTE! to save on code size, it would be better to do one of the options below.
        /*
            // This option saves space because the same strings can be re-used as most compilers use a string table.
            // Instead of having strings like "--> Entered OFF.", "--> Entered ON.", "--> Entered ON1."...
            // The program will have strings like "--> Entered %s.", "OFF", "ON", "ON1".
            state.AddEnterAction($"printf(\"--> Entered %s.\\n\", "{state.Name}");", index:0);
        */
        /*
            // You can also choose to call your own custom callback function like below
            state.AddEnterAction($"my_custom_state_entered_callback("{state.Name}");", index:0);
        */
    });
}
