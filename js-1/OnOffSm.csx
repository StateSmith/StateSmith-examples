#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.8.8-alpha"

// This is a C# script file. Very useful for running StateSmith.
// https://github.com/StateSmith/StateSmith/wiki/Using-c%23-script-files-(.CSX)-instead-of-solutions-and-projects

using System.Text.RegularExpressions;
using StateSmith.Input.Expansions;
using StateSmith.Output;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.Common;

SmRunner runner = new(diagramPath: "OnOffSm.drawio", new MyGlueLogic(), transpilerId: TranspilerId.JavaScript);
runner.Run();

//-------------------------------------------------------

// ignore C# guidelines for script stuff below
#pragma warning disable IDE1006, CA1050


/// <summary>
/// This class is read by StateSmith to determine configuration and expansions.
/// </summary>
public class MyGlueLogic : IRenderConfigCSharp
{
    /// <summary>
    /// These expansions allow you to write clear concise code in diagrams.
    /// More info: https://github.com/StateSmith/StateSmith/blob/main/docs/diagram-features.md#write-code-directly-or-use-expansions
    /// </summary>
    public class MyExpansions : UserExpansionScriptBase
    {
        public string light_off() => "console.log('light is off')";
        public string light_blue() => "console.log('light is blue')";
    }
}
