#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.9-alpha"

using StateSmith.Runner;

// See https://github.com/StateSmith/tutorial-2/blob/main/lesson-1/
SmRunner runner = new(diagramPath: "PizzaSm.drawio", transpilerId: TranspilerId.JavaScript);
runner.Run();
