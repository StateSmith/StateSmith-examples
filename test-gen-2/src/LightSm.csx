#!/usr/bin/env dotnet-script
// This is a c# script file

#r "nuget: StateSmith, 0.9.12-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;  // Note using! This is required to access StateMachine and NamedVertex classes...
using StateSmith.SmGraph.Visitors;

SmRunner runner = new(diagramPath: "LightSm.drawio.svg", new LightSmRenderConfig(), transpilerId: TranspilerId.JavaScript);

// This code modifies the js state machine itself
runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1, 
                                            new TransformationStep(id: "my custom step blah", LoggingTransformationStep));

// This code generate the html
runner.SmTransformer.InsertBeforeFirstMatch(
    StandardSmTransformer.TransformationId.Standard_FinalValidation,
    new TransformationStep(id: "some string id", action: (sm) =>
    {
        // TextWriter mermaidCode = new StringWriter();
        TextWriter mermaidCode = Console.Out;
        mermaidCode.WriteLine("stateDiagram");
        var visitor = new MermaidGenerator(mermaidCode);
        sm.Accept(visitor);
    }));


runner.Run();



void LoggingTransformationStep(StateMachine sm)
{
    // The below code will visit all states in the state machine and add custom enter and exit behaviors.
    sm.VisitTypeRecursively<State>((State state) =>
    {
        state.AddEnterAction($"console.log(\"--> Entered {state.Name}.\");", index:0); // use index to insert at start
        state.AddExitAction($"console.log(\"<-- Exited {state.Name}.\");"); // behavior added to end

        // TODO how to handle escaping state names
        state.AddEnterAction($"document.querySelector('g[data-id={state.Name}]')?.classList.add('active');", index:0); // use index to insert at start
        state.AddExitAction($"document.querySelector('g[data-id={state.Name}]')?.classList.remove('active');");
    });
}





// This class implements the IVertexVisitor interface, which is used to visit the graph.
// If you haven't seen the visitor pattern before, you can check out https://en.wikipedia.org/wiki/Visitor_pattern
// Of note, it is particularly useful for making sure at compile time that we handle all the different types of vertices in the graph.
class MermaidGenerator : IVertexVisitor
{
    private readonly TextWriter writer;

    public MermaidGenerator(TextWriter writer)
    {
        this.writer = writer;
    }

    int indentLevel = 0;

    private void Print(string message)
    {
        for (int i = 0; i < indentLevel; i++)
            writer.Write("        ");

        writer.WriteLine(message);
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
        
        Print( $"state {v.Name} {{");
        VisitChildren(v);
        Print( $"}}");
    }

    public void Visit(NamedVertex v)
    {
        Print("Visiting NamedVertex: " + v.Name);
        VisitChildren(v);
        Print("Finished Visiting NamedVertex: " + v.Name);
    }

    // Format for regular state:
    //   OFF : title
    //   OFF : first line
    //   OFF : second line
    //
    // Format for composite state (multiple lines not supported) https://github.com/mermaid-js/mermaid/issues/5522:
    //   state OFF {
    //   }
    //
    // Note: if a transition crosses a composite state boundary, it must be declared inside the composite state
    // eg
    // STATE1
    // state GROUP {
    //     STATE2
    //     STATE1 --> STATE2  RIGHT
    // }
    // STATE1 --> STATE2  WRONG
    public void Visit(State v)
    {
        if(v.Children.Count > 0) {
            Print( $"state {v.Name} {{");
            VisitChildren(v);
            Print( "}");
        } else {
            Print($"{v.Name}: {v.Name}");        
        }
        VisitBehaviors(v);
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
        VisitBehaviors(v);
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

    private void VisitBehaviors(Vertex v)
    {
        foreach (var behavior in v.Behaviors)
        {
            if(behavior.TransitionTarget!=null) {
                string start = v is NamedVertex ? ((NamedVertex)v).Name : "[*]";
                string end = behavior.TransitionTarget is NamedVertex ? ((NamedVertex)behavior.TransitionTarget).Name : "[*]";
                
                Print($"{start} --> {end}");
            }
        }
    }
}





// This class gives StateSmith the info it needs to generate working C code.
// It adds user code to the generated .c/.h files, declares user variables,
// and provides diagram code expansions. This class can have any name.
public class LightSmRenderConfig : IRenderConfigJavaScript
{

    string IRenderConfig.AutoExpandedVars => """
        count: 0 // variable for state machine
        """;


    // This nested class creates expansions. It can have any name.
    public class MyExpansions : UserExpansionScriptBase
    {
        // public string light_blue()   => """std::cout << "BLUE\n";""";
        // public string light_yellow() => """std::cout << "YELL-OH\n";""";
        // public string light_red()    => """std::cout << "RED\n";""";
    }
}


// string foo = $"""
// <html>
//   <body>

//     <button id="button1">{events[0].Name}</button>
//     <button id="button2">{events[1].Name}</button>

//     <pre class="mermaid">
//         {mermaidCode}
//     </pre>

//     <script src="{sm.Name}.js"></script>
//     <script type="module">
//         import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
//         mermaid.initialize({ startOnLoad: false });
//         await mermaid.run();

//         var sm = new {sm.Name}();

//         document.getElementById("button1").addEventListener ("click", ()=>sm.dispatchEvent({sm.Name}.EventId.{events[0].Name}), false);
//         document.getElementById("button2").addEventListener ("click", ()=>sm.dispatchEvent({sm.Name}.EventId.{events[1].Name}), false);

//         sm.start();
//     </script>


//   </body>
// </html>
// """;