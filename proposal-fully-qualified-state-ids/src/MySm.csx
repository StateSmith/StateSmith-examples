#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.8.15-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using StateSmith.Common;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

SmRunner runner = new(diagramPath: "MySm.drawio.svg");
SupportFullyQualifiedStateIds(runner);
runner.Run();

// https://github.com/StateSmith/StateSmith/issues/137
static void SupportFullyQualifiedStateIds(SmRunner runner)
{
    runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_SupportPrefixingModder,
        new TransformationStep(id: nameof(SupportFullyQualifiedStateIds), action: (sm) =>
        {
            sm.VisitTypeRecursively<State>(state =>
            {
                var parent = state.NonNullParent;

                if (parent is not NamedVertex namedParent)
                    return;

                if (namedParent is StateMachine)
                    return;

                state.Rename(namedParent.Name + "__" + state.Name);
            });
        })
    );
}


