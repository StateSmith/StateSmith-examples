#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.12-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Runner;
SmRunner runner = new(diagramPath: "LightSm.drawio.svg", transpilerId: TranspilerId.C99);
runner.Run();
