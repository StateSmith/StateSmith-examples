#!/usr/bin/env dotnet-script
// If you have any questions about this file, check out https://github.com/StateSmith/tutorial-2
#r "nuget: StateSmith, 0.17.5"

using StateSmith.Common;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

private const string DiagramPath = "LightSm.plantuml";

CppGen.Generate();
PyGen.Generate();


/////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////// C++ CODE GEN //////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////

class CppGen
{
    public static void Generate()
    {
        SmRunner runner = new(diagramPath: DiagramPath, new RenderConfig(), transpilerId: TranspilerId.Cpp);
        runner.Settings.outputDirectory = "cpp";
        runner.Settings.simulation.enableGeneration = true;
        runner.Run();
    }

    public class RenderConfig : IRenderConfigCpp
    {
        string IRenderConfig.AutoExpandedVars => """
            LightBulb bulb;
            """;
        string IRenderConfigCpp.HFileIncludes => """
            #include "LightBulb.hpp"
            """;
    }
}


/////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////// PYTHON CODE GEN //////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////

class PyGen
{
    public static void Generate()
    {
        SmRunner runner = new(diagramPath: DiagramPath, new RenderConfig(), transpilerId: TranspilerId.Python);
        runner.Settings.outputDirectory = "py";
        runner.Settings.simulation.enableGeneration = true;
        // Note! Simulator doesn't yet support custom transformations https://github.com/StateSmith/StateSmith/issues/463
        
        PythonifyDiagramBehaviors(runner);
        runner.Run();
    }
    
    public class RenderConfig : IRenderConfigPython
    {
        string IRenderConfig.AutoExpandedVars => """
            self.bulb = LightBulb()
            """;
        string IRenderConfigPython.Imports => """
            from LightBulb import LightBulb
            """;
    }

    static void PythonifyDiagramBehaviors(SmRunner stateMachineRunner)
    {
        List<TransformationStep> transformationPipeline = stateMachineRunner.SmTransformer.transformationPipeline;

        // NOTE!!! This runs before any other transformations (insert at index 0) so we can be confident that the code we are modifying is in the original form
        // from the diagram and not something that was added by a transformation for something like history vertices.
        transformationPipeline.Insert(0, new TransformationStep(action: (stateMachine) =>
        {
            stateMachine.VisitRecursively((stateMachineVertex) =>
            {
                foreach (var behavior in stateMachineVertex.Behaviors)
                {
                    behavior.actionCode = PythonifyDiagramCode(behavior.actionCode);
                    behavior.guardCode = PythonifyDiagramCode(behavior.guardCode);
                }
            });
        }));
    }

    private static string PythonifyDiagramCode(string str)
    {
        // This is rather simplistic string replacements.
        // You could also parse the code if you needed something more sophisticated.
        str = str.Replace(";", "");
        str = str.Replace("++", " += 1");
        str = str.Replace("/*", "#");
        str = str.Replace("//", "#");
        str = str.Replace("true", "True");
        str = str.Replace("false", "False");
        return str;
    }
}
