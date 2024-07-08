#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.11.2-alpha-drawio-error-location-2"

using StateSmith.Runner;

// The below drawio file is purposely broken to demonstrate the error location feature.
// An xml mxCell element is missing the required attribute "id".
// See https://github.com/StateSmith/StateSmith/issues/353
// NOTE: I couldn't reproduce this issue in a .drawio.svg file as draw.io wouldn't create it.
SmRunner runner = new(diagramPath: "LightSm.drawio", transpilerId: TranspilerId.JavaScript);
runner.Run();
