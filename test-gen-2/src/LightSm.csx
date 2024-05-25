#!/usr/bin/env dotnet-script
// This is a c# script file

#r "nuget: StateSmith, 0.9.12-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;  // Note using! This is required to access StateMachine and NamedVertex classes...
using StateSmith.SmGraph.Visitors;
using StateSmith.Common;

TextWriter mermaidCodeWriter = new StringWriter();
SmRunner htmlRunner = new(diagramPath: "LightSm.drawio.svg", new LightSmRenderConfig(), transpilerId: TranspilerId.JavaScript);
htmlRunner.SmTransformer.InsertBeforeFirstMatch(
    StandardSmTransformer.TransformationId.Standard_FinalValidation,
    new TransformationStep(id: "some string id", action: (sm) =>
    {
        var visitor = new MermaidGenerator(mermaidCodeWriter);
        sm.Accept(visitor);
        visitor.Print(); // print the mermaid code to the mermaidcodewriter
        using(StreamWriter htmlWriter = new StreamWriter($"{sm.Name}.html")) {
            PrintHtml(htmlWriter,sm, mermaidCodeWriter.ToString());
        }
    }));
htmlRunner.Run();


// HACK order is important, the jsRunner must run after the htmlRunner, because the htmlRunner
// also generate js (but without the logging transforms), and the jsRunner must be the last to write
SmRunner jsRunner = new(diagramPath: "LightSm.drawio.svg", new LightSmRenderConfig(), transpilerId: TranspilerId.JavaScript);
jsRunner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1, 
                                            new TransformationStep(id: "my custom step blah", LoggingTransformationStep));
jsRunner.Run();



void PrintHtml(TextWriter writer,  StateMachine sm, string mermaidCode) {

    string foo = $$"""
<!-- 
  -- This file was generated by StateSmith.
  -- It serves as an example of how to use the generated state machine in a web page.
  -- It also serves as an interactive console that you can use to validate the
  -- state machine's behavior.
  -->
<html>
  <body>
    <div id="buttons"></div>

    <pre class="mermaid">
{{mermaidCode}}
    </pre>

    <script src="{{sm.Name}}.js"></script>
    <script type="module">
        import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
        mermaid.initialize({ startOnLoad: false });
        await mermaid.run();

        var sm = new {{sm.Name}}();

        for (const eventName in {{sm.Name}}.EventId) {
            var button = document.createElement('button');
            button.id = 'button_' + eventName;
            button.innerText = eventName;
            button.addEventListener('click', () => sm.dispatchEvent({{sm.Name}}.EventId[eventName]));
            document.getElementById('buttons').appendChild(button);
        }

        sm.start();
    </script>


  </body>
</html>
""";

    writer.WriteLine(foo);
}





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





// TODO it might be more straightforward to iterate over the graph directly instead of using a visitor
class MermaidGenerator : IVertexVisitor
{
    private int indentCount = 0;
    private HashSet<Vertex> leafNodes = new();
    private HashSet<Vertex> compositeNodes = new();
    private TextWriter writer;

    public MermaidGenerator(TextWriter writer)
    {
        this.writer = writer;
    }


    // Format for regular state:
    //   OFF : title
    //   OFF : first line
    //   OFF : second line
    //
    // Format for composite state (multiple lines not supported):
    //   state OFF {
    //    ...
    //   }
    //
    // Transitions must be first
    // Then regular states
    // Then composite states
    //
    // At least that the order that seems to be working best on my test models
    // https://github.com/mermaid-js/mermaid/issues/5522
    public void Print() {
        Print("stateDiagram");
        Print("classDef active fill:yellow,stroke-width:2px;");
        Print("");
        foreach (var node in leafNodes.Concat(compositeNodes)) {
            PrintTransitions(node);
        }
        foreach (var node in leafNodes) {
            PrintLeafNode(node);
        }
        foreach (var node in compositeNodes) {
            PrintCompositeNode(node);
        }
    }


    private void PrintLeafNode(Vertex v) {
        if( v is NamedVertex ) {
            string name = ((NamedVertex)v).Name;
            Print($"{name} : {name}");
            foreach(var b in v.Behaviors.Where(b => b.TransitionTarget==null)) {
                string text = MermaidEscape(b.ToString());
                Print($"{name} : {text}");
            }
            Print("");
        }
    }

    // TODO handle #
    // You can't naively add # to the list of characters because # and ; will interfere with each other
    private string MermaidEscape(string text) {
        foreach( char c in ";\\{}".ToCharArray()) {
            text = text.Replace(c.ToString(), $"#{(int)c};");
        }
        return text;
    }

    private void PrintCompositeNode(Vertex v) {
        if( !(v is NamedVertex) ) {
            throw new Exception("Composite node must be named");
        }

        Print($"state {((NamedVertex)v).Name} {{");
        indentCount++;
        foreach (var child in v.Children)
        {
            if(child is NamedVertex) {
                Print(((NamedVertex)child).Name);
            }
        }
        indentCount--;
        Print("}");
        Print("");
    }

    private void PrintTransitions(Vertex v) {
        foreach (var behavior in v.Behaviors)
        {
            if(behavior.TransitionTarget!=null) {
                string start = v is NamedVertex ? ((NamedVertex)v).Name : "[*]";
                string end = behavior.TransitionTarget is NamedVertex ? ((NamedVertex)behavior.TransitionTarget).Name : "[*]";
                Print($"{start} --> {end}");
            }
        }
        Print("");
    }

    private void Print(string message)
    {
        for (int i = 0; i < indentCount; i++)
        {
            writer.Write("  ");
        }
        writer.WriteLine(message);
    }

    private void VisitChildren(Vertex v)
    {
        foreach (var child in v.Children)
        {
            child.Accept(this);
        }
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
        VisitChildren(v);
    }

    public void Visit(NamedVertex v)
    {
        VisitChildren(v);
    }

    public void Visit(State v)
    {
        if(v.Children.Count > 0) {
            compositeNodes.Add(v);
            VisitChildren(v);
        } else {
            leafNodes.Add(v);
        }
    }

    // orthogonal states are not yet implemented, but will be one day
    public void Visit(OrthoState v)
    {
        throw new NotImplementedException();
    }

    public void Visit(NotesVertex v)
    {
        throw new NotImplementedException();
    }

    public void Visit(InitialState v)
    {
        AssertNoChildren(v);
    }

    public void Visit(ChoicePoint v)
    {
        throw new NotImplementedException();
    }

    public void Visit(EntryPoint v)
    {
        throw new NotImplementedException();
    }

    public void Visit(ExitPoint v)
    {
        throw new NotImplementedException();
    }

    public void Visit(HistoryVertex v)
    {
        throw new NotImplementedException();
    }

    public void Visit(HistoryContinueVertex v)
    {
        throw new NotImplementedException();
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

