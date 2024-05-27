#!/usr/bin/env dotnet-script
// This is a c# script file

#r "nuget: StateSmith, 0.9.14-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;
using StateSmith.SmGraph.Visitors;
using StateSmith.Common;
using System.Text.RegularExpressions;
using StateSmith.Output;

//////////////////////////// REGULAR USER CSX CODE ////////////////////////////
SmRunner regularRunner = new(diagramPath: "LightSm.drawio.svg", transpilerId: TranspilerId.C99);
regularRunner.Run();

public class LightSmRenderConfig : IRenderConfigC
{
    string IRenderConfig.AutoExpandedVars => """
        uint8_t count = 0;
        """;

    public class MyExpansions : UserExpansionScriptBase
    {
        public string light_blue()   => """std::cout << "BLUE\n";""";
        public string light_yellow() => """std::cout << "YELLOW\n";""";
        public string light_red()    => """std::cout << "RED\n";""";
    }
}


/////////////////////////// START OF SOON TO BE STATE SMITH CODE /////////////////////////////////

// the code below will actually be run inside the above SmRunner object when fully implemented in StateSmith.

MermaidEdgeTracker mermaidEdgeTracker = new();

var trackingExpander = new TrackingExpander();
TextWriter mermaidCodeWriter = new StringWriter();
TextWriter mocksWriter = new StringWriter();
SingleFileCapturer fileCapturer = new();

// dependency injection service provider
DiServiceProvider diServiceProvider;

SmRunner runner = new(diagramPath: "LightSm.drawio.svg", renderConfig: new SimRenderConfig(), transpilerId: TranspilerId.JavaScript);
runner.Settings.propagateExceptions = true;

// Registering DI services must be done before accessing `runner.SmTransformer`.
diServiceProvider = runner.GetExperimentalAccess().DiServiceProvider;
diServiceProvider.AddSingletonT<IExpander>(trackingExpander);
diServiceProvider.AddSingletonT<ICodeFileWriter>(fileCapturer);
diServiceProvider.AddSingletonT<IConsolePrinter>(new DiscardingConsolePrinter());

// Note! For `MermaidEdgeTracker` to function correctly, both below transformations must occur in the same `SmRunner`.
// This allows us to easily map an SS behavior to its corresponding mermaid edge ID.
runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_RemoveNotesVertices, new TransformationStep(id: "some string id", GenerateMermaidCode));
// runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1, new TransformationStep(id: "my custom step blah", LoggingTransformationStep));
runner.SmTransformer.InsertBeforeFirstMatch(StandardSmTransformer.TransformationId.Standard_Validation1, new TransformationStep(id: "my custom step blah", V1LoggingTransformationStep));
var stateMachineProvider = diServiceProvider.GetInstanceOf<StateMachineProvider>();
runner.Run();
var smName = stateMachineProvider.GetStateMachine().Name;

mocksWriter.WriteLine(
    """
    // Mocks of functions referenced by your state machine.
    """
);

// Have to disable for now. It is picking up tracing functions. It started to mock `new Date()` and `addHistoryRow()`.
// foreach (var funcAttempt in trackingExpander.AttemptedFunctionExpansions)
// {
//     mocksWriter.WriteLine(
//         $$"""globalThis.{{funcAttempt}} = ()=>{ addHistoryRow(new Date(), "Called mock {{funcAttempt}}()");};""");
// }

using (StreamWriter htmlWriter = new($"{smName}.sim.html")) {
    PrintHtml(htmlWriter, smName: smName, mocksCode: mocksWriter.ToString(), mermaidCode: mermaidCodeWriter.ToString(), jsCode: fileCapturer.CapturedCode);
}


void GenerateMermaidCode(StateMachine sm)
{
    var visitor = new MermaidGenerator(mermaidEdgeTracker);
    visitor.RenderAll(sm);
    mermaidCodeWriter.WriteLine(visitor.GetMermaidCode());
}


static void PrintHtml(TextWriter writer,  string smName, string mocksCode, string mermaidCode, string jsCode)
{
    string htmlTemplate = $$"""
<!-- 
  -- This file was generated by StateSmith.
  -- It serves as an example of how to use the generated state machine in a web page.
  -- It also serves as an interactive console that you can use to validate the
  -- state machine's behavior.
  --
  -- Using {{smName}}.js generally looks like:
  --   var sm = new {{smName}}();
  --   sm.start();
  --
  -- And then using sm.dispatchEvent() to dispatch events to the state machine.
  -->
<html>
  <head>
    <style>
      body {
        display: flex;
        flex-direction: row;
      }

      .main {
        flex: 1;
        overflow: auto;
        padding: 10px;
      }

      .sidebar {
        background-color: #f0f0f0;
        border-left: 1px solid #ccc;
        display: flex;
        flex-direction: column;
        width: 300px;
      }

      #buttons {
        display: flex;
        flex-direction: column;
      }

      .titlebar {
        background-color: #ddd;
        border-bottom: 1px solid #ccc;
        font-weight: bold;
        padding: 5px;
      }

      .console {
        border-collapse: collapse;
        margin-top: 10px;
        width: 100%;
      }

      .console th {
        background-color: #f0f0f0;
        border-bottom: 1px solid #ccc;
        font-weight: normal;
        padding: 5px;
        text-align: left;
      }

      .console tbody {
        display: flex;
        flex-direction: column-reverse;
      }

      .console td {
        border-bottom: 1px solid #ccc;
        padding: 5px;
      }

      .history {
        margin-top: 30px;
        overflow: scroll;    
      }

      .console tr:last-child td {
        border-bottom: none;
      }

      button {
        margin: 5px;
      }
    </style>
  </head>

  <body>
    <div class="main">
        <pre class="mermaid">
{{mermaidCode}}
        </pre>
    </div>

    <div class="sidebar">
        <div id="buttons">
            <div class="titlebar">Actions</div>
        </div>

        <div class="history">
            <div class="titlebar">History</div>
            <table class="console">
            <thead>
                <tr>
                    <th>Time</th>
                    <th>Event</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
            </table>
        </div>
    </div>

<script>
{{jsCode}}
</script>

    <script type="module">
        import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
        import svgPanZoom from 'https://cdn.jsdelivr.net/npm/svg-pan-zoom@3.6.1/+esm' ;
        mermaid.initialize({ startOnLoad: false });
        await mermaid.run();

        // svg-pan-zoom doesn't like the mermaid viewbox
        document.querySelector('svg').removeAttribute('viewBox');
        document.querySelector('svg').setAttribute('width', '100%');
        document.querySelector('svg').setAttribute('height', '100%');
        document.querySelector('svg').style["max-width"] = '';

        svgPanZoom(document.querySelector('svg'), {
            zoomEnabled: true,
            controlIconsEnabled: true,
            fit: true,
            center: true
        });

{{mocksCode}}

        // Convert a date to a string in the format HH:MM:SS.sss
        function formatTime(date) {
            return date.getHours().toString().padStart(2, '0') + ':' +
                date.getMinutes().toString().padStart(2, '0') + ':' +
                date.getSeconds().toString().padStart(2, '0') + '.' +
                date.getMilliseconds().toString().padStart(3, '0');
        }

        // Add a row to the history table.
        function addHistoryRow(time, event) {
            var row = document.createElement('tr');
            var timeCell = document.createElement('td');
            timeCell.innerText = formatTime(time);
            var eventCell = document.createElement('td');
            eventCell.innerText = event;
            row.appendChild(timeCell);
            row.appendChild(eventCell);
            document.querySelector('tbody').appendChild(row);
        }
        window.addHistoryRow = addHistoryRow;   // dirty hack for now. FIXME

        var sm = new {{smName}}();

        // prompt the user to evaluate guards manually
        sm.evaluateGuard = (guard) => {
            return confirm('Evaluate guard: ' + guard);
        }; 

        const highlightedEdges = new Set();
        function highlightEdge(edgeId) {
            var edge = document.getElementById(edgeId);
            if (edge) {
                edge.style.stroke = 'red';
                highlightedEdges.add(edge);
            }
        }

        function clearHighlightedEdges() {
            for (const edge of highlightedEdges) {
                const showOldTraversal = true;
                if (showOldTraversal) {
                    // shows that the edge was traversed. Optional, but kinda nice.
                    edge.style.stroke = 'green';
                } else {
                    edge.style.stroke = '';
                }
            }
            highlightedEdges.clear();
        }

        // The simulator uses a tracer callback to perform operations such as 
        // state highlighting and logging. You do not need this functionality
        // when using {{smName}}.js in your own applications, although you may
        // choose to implement a tracer for debugging purposes.
        sm.tracer = {
            enterState: (stateId) => {
                var name = {{smName}}.stateIdToString(stateId);
                document.querySelector('g[data-id=' + name + ']')?.classList.add('active');
                addHistoryRow(new Date(), "Entered " + name);
            },
            exitState: (stateId) => {
                var name = {{smName}}.stateIdToString(stateId);
                document.querySelector('g[data-id=' + name + ']')?.classList.remove('active');
            },
            edgeTransition: (edgeId) => {
                highlightEdge(edgeId);
            }
        };

        // Wire up the buttons that dispatch events for the state machine.
        for (const eventName in {{smName}}.EventId) {
            var button = document.createElement('button');
            button.id = 'button_' + eventName;
            button.innerText = eventName;
            button.addEventListener('click', () => {
                clearHighlightedEdges();
                addHistoryRow(new Date(), "Dispatched " + eventName);
                sm.dispatchEvent({{smName}}.EventId[eventName]); 
            });
            document.getElementById('buttons').appendChild(button);
        }

        sm.start();
    </script>


  </body>
</html>
""";

    writer.WriteLine(htmlTemplate);
}





void LoggingTransformationStep(StateMachine sm)
{
    // The below code will visit all states in the state machine and add custom enter and exit behaviors.
    sm.VisitTypeRecursively<State>((State state) =>
    {
        state.AddEnterAction($"this.tracer?.enterState({sm.Name}.StateId.{state.Name});", index:0); // use index to insert at start
        state.AddExitAction($"this.tracer?.exitState({sm.Name}.StateId.{state.Name});");
    });

    sm.VisitTypeRecursively<Vertex>((Vertex vertex) =>
    {
        foreach (var behavior in vertex.Behaviors.Where( b => b.TransitionTarget!=null && b.HasGuardCode() ))
        {
            behavior.guardCode = $"this.evaluateGuard != null ? this.evaluateGuard('{behavior.guardCode}') : {behavior.guardCode}";
        }

        foreach (var b in vertex.TransitionBehaviors())
        {
            if (mermaidEdgeTracker.ContainsEdge(b))
            {
                // Note: most history behaviors will not be shown in the mermaid diagram
                var domId = "edge" + mermaidEdgeTracker.GetEdgeId(b);
                // NOTE! Avoid single quotes in ss guard/action code until bug fixed: https://github.com/StateSmith/StateSmith/issues/282
                b.actionCode += $"""this.tracer.edgeTransition("{domId}");""";
            }
        }
    });
}





void V1LoggingTransformationStep(StateMachine sm)
{
    sm.VisitRecursively((Vertex vertex) =>
    {
        foreach (var behavior in vertex.Behaviors)
        {
            V1ModBehaviorsForSimulation(vertex, behavior);
        }

        V1AddEntryExitTracing(sm, vertex);
        V1AddEdgeTracing(vertex);
    });
}

void V1AddEdgeTracing(Vertex vertex)
{
    foreach (var b in vertex.TransitionBehaviors())
    {
        if (mermaidEdgeTracker.ContainsEdge(b))
        {
            // Note: most history behaviors will not be shown in the mermaid diagram
            var domId = "edge" + mermaidEdgeTracker.GetEdgeId(b);
            // NOTE! Avoid single quotes in ss guard/action code until bug fixed: https://github.com/StateSmith/StateSmith/issues/282
            b.actionCode += $"""this.tracer.edgeTransition("{domId}");""";
        }
    }
}

static void V1AddEntryExitTracing(StateMachine sm, Vertex vertex)
{
    // we purposely don't want to trace the entry/exit of the state machine itself.
    // That's why we use `State` instead of `NamedVertex`.
    if (vertex is State state)
    {
        var id = $"{sm.Name}.StateId.{state.Name}";
        state.AddEnterAction($"""this.tracer?.enterState({id});""", index: 0);
        state.AddExitAction($"""this.tracer?.exitState({id});""");
    }
}

void V1ModBehaviorsForSimulation(Vertex vertex, Behavior behavior)
{
    if (behavior.HasActionCode())
    {
        // GIL is Generic Intermediary Language. It is used by history vertices and other special cases.
        if (behavior.actionCode.Contains("$gil("))
        {
            // keep actual code
            behavior.actionCode += $"""addHistoryRow(new Date(), "Executed action: " + {EscapeFsmCode(behavior.actionCode)});""";
        }
        else
        {
            // we don't want to execute the action, just log it.
            behavior.actionCode = $"""addHistoryRow(new Date(), "FSM would execute action: " + {EscapeFsmCode(behavior.actionCode)});""";
        }
    }

    if (vertex is HistoryVertex)
    {
        if (behavior.HasGuardCode())
        {
            // we want the history vertex to work as is without prompting the user to evaluate those guards.
            var logCode = $"""addHistoryRow(new Date(), "History state evaluating guard: " + {EscapeFsmCode(behavior.guardCode)})""";
            var actualCode = behavior.guardCode;
            behavior.guardCode = $"""{logCode} || {actualCode}""";
        }
        else
        {
            behavior.actionCode += $"""addHistoryRow(new Date(), "History state taking default transition.");""";
        }
    }
    else
    {
        if (behavior.HasGuardCode())
        {
            var logCode = $"""addHistoryRow(new Date(), "User evaluating guard: " + {EscapeFsmCode(behavior.guardCode)})""";
            var confirmCode = $"""this.evaluateGuard({EscapeFsmCode(behavior.guardCode)})""";
            behavior.guardCode = $"""{logCode} || {confirmCode}""";
            // NOTE! logCode doesn't return a value, so the confirm code will always be evaluated.
        }
    }
}

string EscapeFsmCode(string code)
{
    return "\"" + code.Replace("\"", "\\\"") + "\"";
}



class MermaidGenerator : IVertexVisitor
{
    int indentLevel = 0;
    StringBuilder sb = new();
    MermaidEdgeTracker mermaidEdgeTracker;

    public MermaidGenerator(MermaidEdgeTracker edgeOrderTracker)
    {
        this.mermaidEdgeTracker = edgeOrderTracker;
    }

    public void RenderAll(StateMachine sm)
    {
        sm.Accept(this);
        RenderEdges(sm);
    }

    public string GetMermaidCode()
    {
        return sb.ToString();
    }

    public void Visit(StateMachine v)
    {
        AppendLn("stateDiagram");
        AppendLn("classDef active fill:yellow,stroke-width:2px;");
        indentLevel--; // don't indent the state machine contents
        VisitChildren(v);
    }

    public void Visit(State v)
    {
        if (v.Children.Count <= 0)
        {
            VisitLeafState(v);
        }
        else
        {
            VisitCompoundState(v);
        }
    }

    private void VisitCompoundState(State v)
    {
        AppendLn($$"""state {{v.Name}} {""");
        // FIXME - add behavior code here when supported by mermaid
        // https://github.com/StateSmith/StateSmith/issues/268#issuecomment-2111432194
        VisitChildren(v);
        AppendLn("}");
    }

    private void VisitLeafState(State v)
    {
        string name = v.Name;
        AppendLn(name);
        AppendLn($"{name} : {name}");
        foreach (var b in v.Behaviors.Where(b => b.TransitionTarget == null))
        {
            string text = b.ToString();
            text = MermaidEscape(text);
            AppendLn($"{name} : {text}");
        }
    }

    public void Visit(InitialState initialState)
    {
        string initialStateId = MakeVertexDiagramId(initialState);

        // Mermaid and PlantUML don't have a syntax that allows transitions to an initial state.
        // If you do `someState --> [*]`, it means transition to a final state.
        // StateSmith, however, does allow transitions to initial states so we add a dummy state to represent the initial state.
        AppendLn($"[*] --> {initialStateId}");
        mermaidEdgeTracker.AdvanceId();  // we skip this "work around" edge for now. We can improve this later.
        AppendLn($"""state "$initial_state" as {initialStateId}""");
    }

    public void Visit(ChoicePoint v)
    {
        AppendLn($"""state {MakeVertexDiagramId(v)} <<choice>>""");
    }

    public void Visit(EntryPoint v)
    {
        AppendLn($"""state "$entry_pt.{v.label}" as {MakeVertexDiagramId(v)}""");
    }

    public void Visit(ExitPoint v)
    {
        AppendLn($"""state "$exit_pt.{v.label}" as {MakeVertexDiagramId(v)}""");
    }

    public void Visit(HistoryVertex v)
    {
        AppendLn($"""state "$H" as {MakeVertexDiagramId(v)}""");
    }

    public void Visit(HistoryContinueVertex v)
    {
        AppendLn($"""state "$HC" as {MakeVertexDiagramId(v)}""");
    }


    public void RenderEdges(StateMachine sm)
    {
        sm.VisitRecursively((Vertex v) =>
        {
            string vertexDiagramId = MakeVertexDiagramId(v);

            foreach (var behavior in v.Behaviors)
            {
                if (behavior.TransitionTarget != null)
                {
                    var text = behavior.ToString();
                    text = Regex.Replace(text, @"\s*TransitionTo[(].*[)]", ""); // bit of a hack to remove the `TransitionTo(SOME_STATE)` text
                    text = MermaidEscape(text);
                    sb.AppendLine($"{vertexDiagramId} --> {MakeVertexDiagramId(behavior.TransitionTarget)} : {text}");
                    mermaidEdgeTracker.AddEdge(behavior);
                }
            }
        });
    }

    public static string MakeVertexDiagramId(Vertex v)
    {
        switch (v)
        {
            case NamedVertex namedVertex:
                return namedVertex.Name;
            default:
                // see https://github.com/StateSmith/StateSmith/blob/04955e5df7d5eb6654a048dccb35d6402751e4c6/src/StateSmithTest/VertexDescribeTests.cs
                return Vertex.Describe(v).Replace("<", "(").Replace(">", ")");
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

    private void AppendLn(string message)
    {
        for (int i = 0; i < indentLevel; i++)
            sb.Append("        ");

        sb.AppendLine(message);
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

    public void Visit(RenderConfigVertex v)
    {
        // just ignore render config and any children
    }

    public void Visit(ConfigOptionVertex v)
    {
        // just ignore config option and any children
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

    public void Visit(NamedVertex v)
    {
        throw new NotImplementedException(); // should not be called
    }

    public void Visit(Vertex v)
    {
        throw new NotImplementedException(); // should not be called
    }
}


public class SimRenderConfig : IRenderConfigJavaScript
{
    // for sim v1, we won't understand any variables
    string IRenderConfig.AutoExpandedVars => """
        
        """;

    string IRenderConfigJavaScript.ClassCode => """        
        // Null by default.
        // May be overridden to override guard evaluation (eg. in a simulator)
        evaluateGuard = null;
    """;
}

/// <summary>
/// This class maps a behavior to its corresponding mermaid edge ID.
/// </summary>
public class MermaidEdgeTracker
{
    Dictionary<Behavior, int> edgeIdMap = new();
    int nextId = 0;

    public int AddEdge(Behavior b)
    {
        int id = nextId;
        AdvanceId();
        edgeIdMap.Add(b, id);
        return id;
    }

    // use when a non-behavior edge is added
    public int AdvanceId()
    {
        return nextId++;
    }

    public bool ContainsEdge(Behavior b)
    {
        return edgeIdMap.ContainsKey(b);
    }

    public int GetEdgeId(Behavior b)
    {
        return edgeIdMap[b];
    }
}


/// <summary>
/// Allows us to capture the the code that would be written to a file.
/// </summary>
public class SingleFileCapturer : ICodeFileWriter
{
    public string CapturedCode { get; private set; } = "";
    public string FilePath { get; private set; } = "";

    void ICodeFileWriter.WriteFile(string filePath, string code)
    {
        if (CapturedCode != "")
        {
            throw new InvalidOperationException(nameof(SingleFileCapturer) + " can only capture one file. Already captured file: " + FilePath);
        }

        FilePath = filePath;
        CapturedCode = code;
    }

    public string FileName => Path.GetFileName(FilePath);
}



// we may want to capture this info at some point
public class DiscardingConsolePrinter : IConsolePrinter
{
    public void WriteErrorLine(string message)
    {
    }

    public void WriteLine()
    {
    }

    public void WriteLine(string message)
    {
    }
}