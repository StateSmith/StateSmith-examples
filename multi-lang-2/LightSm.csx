#!/usr/bin/env dotnet-script
// If you have any questions about this file, check out https://github.com/StateSmith/tutorial-2
#r "nuget: StateSmith, 0.17.5"

using StateSmith.Common;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

GenerateCpp();
GeneratePython();

void GenerateCpp()
{
    SmRunner runner = new(diagramPath: "LightSm.plantuml", new CppRenderConfig(), transpilerId: TranspilerId.Cpp);
    runner.Settings.outputDirectory = "cpp";
    runner.Run();
}

void GeneratePython()
{
    SmRunner runner = new(diagramPath: "LightSm.plantuml", new PythonRenderConfig(), transpilerId: TranspilerId.Python);
    runner.Settings.outputDirectory = "py";
    PythonifyDiagramBehaviors(runner);
    runner.Run();
}

static void PythonifyDiagramBehaviors(SmRunner runner)
{
    // NOTE!!! This runs before any other transformations (insert at index 0) so we can be confident that the code we are modifying is in the original form
    // from the diagram and not something that was added by a transformation (like history vertices).
    runner.SmTransformer.transformationPipeline.Insert(0, new TransformationStep(action: (sm) =>
    {
        sm.VisitRecursively((node) =>
        {
            foreach (var behavior in node.Behaviors)
            {
                behavior.actionCode = PythonifyDiagramCode(behavior.actionCode);
                behavior.guardCode = PythonifyDiagramCode(behavior.guardCode);
            }
        });
    }));
}

private static string PythonifyDiagramCode(string str)
{
    str = str.Replace(";", "");
    str = str.Replace("++", " += 1");
    str = str.Replace("/*", "#");
    str = str.Replace("//", "#");
    str = str.Replace("true", "True");
    str = str.Replace("false", "False");
    return str;
}



// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-2/ (basics)
// See https://github.com/StateSmith/tutorial-2/tree/main/lesson-5/ (language specific options)
public class CppRenderConfig : IRenderConfigCpp
{
    string IRenderConfig.FileTop => """
        // Whatever you put in the IRenderConfig.FileTop section ends up at the top of the generated file(s).
        """;
    
    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class MyExpansions : UserExpansionScriptBase
    {
        // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-4 for timing expansions
    }
}

public class PythonRenderConfig : IRenderConfigPython
{
    string IRenderConfig.AutoExpandedVars => """
        self.bulb = LightBulb()
        """;

    string IRenderConfigPython.Imports => """
        from LightBulb import LightBulb
        """;
    
    // See https://github.com/StateSmith/tutorial-2/tree/main/lesson-3
    public class MyExpansions : UserExpansionScriptBase
    {
    }
}
