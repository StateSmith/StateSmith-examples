#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.8"

// spell-checker: ignore drawio

using StateSmith.Common;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

SmRunner runner = new(diagramPath: "MySm.drawio.svg");

// You can find settings and descriptions at 
// https://github.com/StateSmith/StateSmith/blob/main/src/StateSmith/Runner/SmDesignDescriberSettings.cs
runner.Settings.smDesignDescriber.enabled = true;
// try commenting/uncommenting below lines to see the difference in output:
// runner.Settings.smDesignDescriber.outputSections.beforeTransformations = false; // defaults to true. Good for git diffs
// runner.Settings.smDesignDescriber.outputSections.afterTransformations = true;   // defaults to false. Good for understanding StateSmith transformations.
// runner.Settings.smDesignDescriber.outputAncestorHandlers = true;                // defaults to false. Good for understanding nested state machines.

runner.Run();
