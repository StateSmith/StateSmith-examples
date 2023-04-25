#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.2-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using System.Text.RegularExpressions;
using StateSmith.Input.Expansions;
using StateSmith.Output;
using StateSmith.Output.C99BalancedCoder1;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;

SmRunner runner = new(diagramPath: "MySm.drawio.svg");

// add our custom functionality to StateSmith
runner.GetExperimentalAccess().DiServiceProvider.AddSingletonT<ICodeFileWriter, MyCodeFileWriter>();
runner.GetExperimentalAccess().DiServiceProvider.AddSingletonT<MyEventIdToStringCreator, MyEventIdToStringCreator>();

// run StateSmith
runner.Run();


/////////////////////////////

/// <summary>
/// This class allows us to get access to the generated code before it gets written to file.
/// We can then use our custom code to post process it to add and remove what we want.
/// </summary>
public class MyCodeFileWriter : ICodeFileWriter
{
    // This dependency is fulfilled by dependency injection (DiServiceProvider)
    readonly MyEventIdToStringCreator myEventIdToStringCreator;

    // constructor required for dependency injection
    public MyCodeFileWriter(MyEventIdToStringCreator myEventIdToStringCreator)
    {
        this.myEventIdToStringCreator = myEventIdToStringCreator;
    }

    // implements the ICodeFileWriter interface
    void ICodeFileWriter.WriteFile(string filePath, string code)
    {
        code = MyRegexPostProcessor.RemoveStateIdToStringFunction(code);
        code = MyRegexPostProcessor.SurroundRootExitWithAnnotations(code);

        // add custom event ID to string function
        if (filePath.EndsWith(".h"))
        {
            // .h file code
            code += myEventIdToStringCreator.FunctionPrototypeCode();
        }
        else
        {
            // .c file code
            code += myEventIdToStringCreator.FunctionCode();
        }

        // We now write the file
        File.WriteAllText(path: filePath, code);
    }
}

/// <summary>
/// Provides methods to help post process code based on regular expression replacements.
/// Based on user request https://github.com/StateSmith/StateSmith/issues/105
/// </summary>
public class MyRegexPostProcessor
{
    public static string RemoveStateIdToStringFunction(string code)
    {
        // See https://www.debuggex.com/r/gXfQMzS0tRShoWTp
        Regex regex = new(@"(?xm)

            # optional leading blank line
            ^ [\t ]* (?: \r\n | \r | \n )?

            # matches any leading single line comments that are attached
            (?:
                // .*                # single line comment
                (?: \r\n | \r | \n ) # a line ending
            )*

            ^ [\w* \t]+ state_id_to_string \s* [(]

            # matches function prototype or body
            (?:
                [^)]+ [)]   # matches function parameters and closing brace ')'
                \s*
                ;
                |
                [\s\S]+? # matches anything lazily
                ^}
                \s*?
            )
            (?: \r\n | \r | \n )? # an optional line ending
        ");

        code = regex.Replace(input: code, replacement: "", count: 1);

        return code;
    }

    public static string SurroundRootExitWithAnnotations(string code)
    {
        // See https://www.debuggex.com/r/bCAg1nmGxB9Zeim0
        Regex regex = new(@"(?xm)
            (?<original_function> # named capture group
                ^static \s+ void \s+ ROOT_exit \s* [(] [^)]* [)] \s* [{]
                [^}]+   # match everything until closing brace
                [}]
                [ \t]*  # any horizontal whitespace
                (?: \r\n | \r | \n ) # a line ending
            )
        ");

        var replacement = StringUtils.DeIndentTrim(@"
            // The root exit function can't actually be called so we add code coverage markers.
            // LCOV_EXCLUDE_START
            // GCOV_EXCLUDE_START
            // GCOVR_EXCLUDE_START
            ${original_function}// LCOV_EXCLUDE_STOP
            // GCOV_EXCLUDE_STOP
            // GCOVR_EXCLUDE_STOP
        ");

        code = regex.Replace(input: code, replacement: replacement, count: 1);

        return code;
    }
}

/// <summary>
/// Pretty cool. This class generates a new function that takes a state machine event ID, and returns its string representation.
/// Why would you want to do this? StateSmith doesn't provide a function like this yet.
/// You could also use this approach to replace the state ID to string function to make it more suitable to your hardware platform.
/// Based on user request https://github.com/StateSmith/StateSmith/issues/109
/// </summary>
public class MyEventIdToStringCreator
{
    // These dependencies are fulfilled by dependency injection (DiServiceProvider)
    readonly StateMachineProvider smProvider;
    readonly CNameMangler mangler;
    readonly CodeStyleSettings codeStyle;

    // constructor required for dependency injection
    public MyEventIdToStringCreator(StateMachineProvider smProvider, CNameMangler mangler, CodeStyleSettings codeStyle)
    {
        this.smProvider = smProvider;
        this.mangler = mangler;
        this.codeStyle = codeStyle;
    }

    string FunctionSignatureCode()
    {
        var output = "// Converts an event id to a string. Thread safe.\n";
        output += $"const char* {mangler.SmName}_event_id_to_string(const enum {mangler.SmEventEnum} id)";
        return output;
    }

    public string FunctionPrototypeCode()
    {
        return FunctionSignatureCode() + ";\n";
    }

    public string FunctionCode()
    {
        var sm = smProvider.GetStateMachine();
        var sb = new StringBuilder();
        var output = new OutputFile(codeStyle, sb);

        output.Append(FunctionSignatureCode());
        output.StartCodeBlock();
        {
            output.Append("switch (id)");
            output.StartCodeBlock();
            {
                foreach (var eventName in sm.GetEventListCopy())
                {
                    output.AppendLine($"case {mangler.SmEventEnumValue(eventName)}: return \"{eventName}\";");
                }
            }
            output.FinishCodeBlock();
            output.AppendLine("return \"?\";");
        }
        output.FinishCodeBlock();
        return output.ToString();
    }
}