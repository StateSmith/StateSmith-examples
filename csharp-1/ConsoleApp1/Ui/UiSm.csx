#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.8.6-alpha"

// This is a C# script file. Very useful for running StateSmith.
// https://github.com/StateSmith/StateSmith/wiki/Using-c%23-script-files-(.CSX)-instead-of-solutions-and-projects

using System.Text.RegularExpressions;
using StateSmith.Input.Expansions;
using StateSmith.Output;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.Common;

SmRunner runner = new(diagramPath: "UiSm.drawio", new MyGlueLogic(), transpilerId: TranspilerId.CSharp);
runner.Run();

//-------------------------------------------------------

// ignore C# guidelines for script stuff below
#pragma warning disable IDE1006, CA1050


/// <summary>
/// This class is read by StateSmith to determine configuration and expansions.
/// </summary>
public class MyGlueLogic : IRenderConfigCSharp
{
    string IRenderConfigCSharp.NameSpace => "ConsoleApp1.Ui;";

    string IRenderConfigCSharp.BaseList => "UiSmBase";

    /// <summary>
    /// These expansions allow you to write clear concise code in diagrams.
    /// More info: https://github.com/StateSmith/StateSmith/blob/main/docs/diagram-features.md#write-code-directly-or-use-expansions
    /// </summary>
    public class MyExpansions : UserExpansionScriptBase
    {
        public string t1Restart() => $"t1.Restart()";
        public string t1AfterMs(string msCount) => $"t1.ElapsedMilliseconds >= {msCount}";
        public string t1After(string timeStr) => $"{t1AfterMs(TimeStrToMs(timeStr))}";

        // Just showing an expansion example. This doesn't need to be an expansion.
        public string distance => $"\"3.7 parsecs\"";

        // Just showing an expansion example. This doesn't need to be an expansion.
        public string Print(string msg) => $"System.Console.WriteLine({msg})";
    }
}

public static string TimeStrToMs(string timeStr)
{
    return TimeStringParser.ElapsedTimeStringToMs(timeStr).ToString();
}