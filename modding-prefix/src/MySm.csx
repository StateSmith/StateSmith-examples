#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.8.15-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;

SmRunner runner = new(diagramPath: "MySm.drawio.svg");
runner.Run();

