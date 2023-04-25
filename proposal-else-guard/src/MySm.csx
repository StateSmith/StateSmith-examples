#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.2-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using StateSmith.Common;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

SmRunner runner = new(diagramPath: "MySm.drawio.svg");
SupportElseGuard(runner);
runner.Run();

static void SupportElseGuard(SmRunner runner)
{
    runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_SupportOrderAndElse,
        new TransformationStep(id: nameof(SupportElseGuard), action: (sm) =>
        {
            sm.VisitTypeRecursively<Vertex>(vertex =>
            {
                foreach (var behavior in vertex.Behaviors)
                {
                    if (behavior.guardCode.Trim().ToLower() == "else")
                    {
                        if (behavior.order != Behavior.DEFAULT_ORDER)
                            throw new BehaviorValidationException(behavior, "can't specify order and `[else]` guard at the same time.");

                        behavior.guardCode = "";
                        behavior.order = Behavior.ELSE_ORDER;
                    }
                }
            });
        })
    );
}
