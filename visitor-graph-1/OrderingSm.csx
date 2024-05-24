#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.13-alpha"
// spell-checker: ignore drawio

using StateSmith.Runner;
using StateSmith.SmGraph;
using StateSmith.SmGraph.Visitors;

SmRunner runner = new(diagramPath: "OrderingSm.drawio.svg");
AddPrintingVisitor();
runner.Run();


// ------------- functions follow -------------


void AddPrintingVisitor()
{
    runner.SmTransformer.InsertBeforeFirstMatch(
        StandardSmTransformer.TransformationId.Standard_FinalValidation,
        new TransformationStep(id: "some string id", action: (sm) =>
        {
            Console.WriteLine("\n\nStarting to visit the graph");
            var visitor = new MyGraphVisitor();
            sm.Accept(visitor);
            Console.WriteLine("Finished visiting the graph\n\n");
        }));
}


// This class implements the IVertexVisitor interface, which is used to visit the graph.
// If you haven't seen the visitor pattern before, you can check out https://en.wikipedia.org/wiki/Visitor_pattern
// Of note, it is particularly useful for making sure at compile time that we handle all the different types of vertices in the graph.
class MyGraphVisitor : IVertexVisitor
{
    int indentLevel = 1;

    private void Print(string message)
    {
        for (int i = 0; i < indentLevel; i++)
            Console.Write("        ");

        Console.WriteLine(message);
    }

    private void VisitChildren(Vertex v)
    {
        indentLevel++;
        foreach (var child in v.Children)
        {
            child.Accept(this);
        }
        indentLevel--;
    }

    private void AssertNoChildren(Vertex v)
    {
        if (v.Children.Count > 0)
        {
            throw new Exception($"Vertex `{Vertex.Describe(v)}` not expected to have children");
        }
    }

    public void Visit(Vertex v)
    {
        throw new NotImplementedException();
    }

    public void Visit(StateMachine v)
    {
        Print("Visiting StateMachine: " + v.Name);
        VisitChildren(v);
        Print("Finished Visiting StateMachine: " + v.Name);
    }

    public void Visit(NamedVertex v)
    {
        Print("Visiting NamedVertex: " + v.Name);
        VisitChildren(v);
        Print("Finished Visiting NamedVertex: " + v.Name);
    }

    public void Visit(State v)
    {
        Print("Visiting State: " + v.Name);
        VisitChildren(v);
        Print("Finished Visiting State: " + v.Name);
    }

    // orthogonal states are not yet implemented, but will be one day
    public void Visit(OrthoState v)
    {
        throw new NotImplementedException();
    }

    public void Visit(NotesVertex v)
    {
        // just ignore notes and any children
    }

    public void Visit(InitialState v)
    {
        Print("Visiting InitialState");
        AssertNoChildren(v);
    }

    public void Visit(ChoicePoint v)
    {
        Print("Visiting ChoicePoint with label: " + v.label);
        AssertNoChildren(v);
    }

    public void Visit(EntryPoint v)
    {
        Print("Visiting EntryPoint with label: " + v.label);
        AssertNoChildren(v);
    }

    public void Visit(ExitPoint v)
    {
        Print("Visiting ExitPoint with label: " + v.label);
        AssertNoChildren(v);
    }

    public void Visit(HistoryVertex v)
    {
        Print("Visiting HistoryVertex");
        AssertNoChildren(v);
    }

    public void Visit(HistoryContinueVertex v)
    {
        Print("Visiting HistoryContinueVertex");
        AssertNoChildren(v);
    }

    public void Visit(RenderConfigVertex v)
    {
        // just ignore render config and any children
    }

    public void Visit(ConfigOptionVertex v)
    {
        // just ignore config option and any children
    }
}
