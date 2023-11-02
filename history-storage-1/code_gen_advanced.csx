#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.9-alpha"

using StateSmith.Runner;
using StateSmith.SmGraph;

// USAGE: Try changing renaming state "SIZE" to "PIZZA_SIZE" and run this file. You'll get a detailed exception message.

// Here you manually list the states that you expect to be tracked by the History vertex
var expectedTrackedStates = new List<string> { "PIZZA_BUILD", "CRUST", "PURCHASING", "ORDERED", "TOPPINGS", "SIZE" };

// See https://github.com/StateSmith/tutorial-2/blob/main/lesson-1/
SmRunner runner = new(diagramPath: "PizzaSm.drawio", transpilerId: TranspilerId.JavaScript);
AddHistoryStateChangeDetection();
runner.Run();

//////////////////////////////////////////////////////////////////////////////////////////////////

void AddHistoryStateChangeDetection()
{
    // Register our custom code to run after StateSmith transforms the StateMachine graph to support History vertices (it adds state transitions to tracked states).
    runner.SmTransformer.InsertAfterFirstMatch(StandardSmTransformer.TransformationId.Standard_SupportHistory, new TransformationStep(id: "our-custom-code", (StateMachine sm) =>
    {
        var stateMachineHistoryVertex = sm.ChildType<HistoryVertex>();

        // We can find the tracked states from the History state's behaviors (AKA transitions).
        // We convert the state names to uppercase to match our expected state names, but we could go with lowercase instead if we wanted.
        var actualTrackedStates = stateMachineHistoryVertex.Behaviors.Select(behavior => behavior.TransitionTarget as NamedVertex).Select(vertex => vertex.Name.ToUpper()).ToList();
        
        var expectedButMissing = expectedTrackedStates.Except(actualTrackedStates).ToList();
        var unexpected = actualTrackedStates.Except(expectedTrackedStates).ToList();

        if (expectedButMissing.Count > 0 || unexpected.Count > 0)
        {
            var detail = "";
            foreach (var name in unexpected)
                detail += "+" + name + ", ";

            foreach (var name in expectedButMissing)
                detail += "-" + name + ", ";

            throw new Exception($"State machine root history changes: {detail}");
        }
    }));
}