#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.7.11-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using System.Text.RegularExpressions;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

HashSet<NamedVertex> loggedVertices = new();

SmRunner runner = new(diagramPath: "MySm.drawio.svg");
AddRecursiveLogging();
AddSimpleLogging(); // must be called after AddRecursiveLogging() 
runner.Run();

// --- functions follow ---------

void AddSimpleLogging()
{
    runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1,
        new TransformationStep(id: "my simple log helper", action: (sm) =>
        {
            Regex regex = new(@"(?x) # enables regex verbose mode
            ^   # start of input
            \s* # any white space
            log # 'log'
            \s* # any white space
            $   # end of input
            ");

            sm.VisitTypeRecursively<NamedVertex>((namedVertex) =>
            {
                if (IsNotLogged(namedVertex) && ShouldBeLogged(namedVertex, regex))
                {
                    loggedVertices.Add(namedVertex);
                    namedVertex.AddEnterAction($"printf(\"Entered {namedVertex.Name}.\");");
                    namedVertex.AddExitAction($"printf(\"Exited {namedVertex.Name}.\");");
                }
            });
        }));
}

static bool ShouldBeLogged(NamedVertex namedVertex, Regex regex)
{
    return TriggerModHelper.PopModBehaviors(namedVertex, regex).Any();
}

bool IsNotLogged(NamedVertex namedVertex)
{
    return loggedVertices.Contains(namedVertex) == false;
}

/// <summary>
/// Must be called before AddSimpleLogging() as it relies on the simple logging
/// </summary>
void AddRecursiveLogging()
{
    runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1,
        new TransformationStep(id: "my recursive log helper", action: (sm) =>
        {
            Regex regex = new(@"(?x) # enables regex verbose mode
            ^   # start of input
            \s* # any white space
            log_r # 'log'
            \s* # any white space
            $   # end of input
            ");

            sm.VisitTypeRecursively<NamedVertex>((namedVertex) =>
            {
                if (TriggerModHelper.PopModBehaviors(namedVertex, regex).Any())
                {
                    namedVertex.VisitTypeRecursively<NamedVertex>((toLog) =>
                    {
                        toLog.AddBehavior(new Behavior(trigger: "$mod", actionCode: "log"));
                    });
                }
            });
        }));
}

