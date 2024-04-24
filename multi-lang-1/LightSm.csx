#!/usr/bin/env dotnet-script
// This is a c# script file

#r "nuget: StateSmith, 0.9.9-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;


// run code generation for C99 first
SmRunner runner = new(diagramPath: "LightSm.drawio.svg", new C99RenderConfig(), transpilerId: TranspilerId.C99);
runner.Run();

// run code generation for JavaScript
runner = new(diagramPath: "LightSm.drawio.svg", new JsRenderConfig(), transpilerId: TranspilerId.JavaScript);
runner.Run();



// ####################################################################################
// #################################### EXPANSIONS ####################################
// ####################################################################################

// ignore C# guidelines for script stuff below
#pragma warning disable IDE1006, CA1050 


// generic expansions that can be used in both C and JS
public abstract class GenericExpansions : UserExpansionScriptBase
{
    public string count => AutoVarName();

    public string light_blue()   => print("\"BLUE\"");
    public string light_yellow() => print("\"YELLOW\"");
    public string light_red()    => print("\"RED\"");

    // parameter varPath will be something like `sm->vars.count`
    public string print_var(string varPath)
    {
        string varName = varPath.Split('.').Last(); // get just the variable name
        return print_name_value(varName, varPath);
    }

    // Note! These are abstract. C and JS expansions will implement this differently.
    public abstract string print(string msg);
    public abstract string print_name_value(string name, string value);
}


// expansions for C
public class CExpansions : GenericExpansions
{
    public override string print(string msg) => $"""printf({msg} "\n")""";
    public override string print_name_value(string name, string value) => $"""printf("{name}: %i\n", {value})""";
}


// expansions for JS
public class JsExpansions : GenericExpansions
{
    public override string print(string msg) => $"console.log({msg})";
    public override string print_name_value(string name, string value) => $"""console.log("{name}:", {value})""";
}



// ####################################################################################
// ################################ RENDER CONFIGS ####################################
// ####################################################################################

// Generic RenderConfig settings. 
// You don't need to use a base class like this if you don't want to.
public class GenericRenderConfig : IRenderConfig
{
    // the file top stuff is the same for all languages
    string IRenderConfig.FileTop => """
        // Example file top stuff for all languages...
        """;
}

// RenderConfig for JavaScript
public class JsRenderConfig : GenericRenderConfig, IRenderConfigJavaScript
{
    // NOTE! Expansions can be also be used like this (composition).
    // They don't have to be defined in the same class as the RenderConfig.
    JsExpansions expansions = new();

    string IRenderConfig.VariableDeclarations => """
        count : 0, // variable for state machine
        """;

    // ... other stuff if you want
}

// RenderConfig for C
public class C99RenderConfig : GenericRenderConfig, IRenderConfigC
{
    // NOTE! Expansions can be also be used like this (composition).
    // They don't have to be defined in the same class as the RenderConfig.
    CExpansions expansions = new();

    string IRenderConfig.VariableDeclarations => """
        uint8_t count; // variable for state machine
        """;

    string IRenderConfigC.CFileTop => """
        // user IRenderConfigC.CFileTop: whatever you want to put in here.
        #include <stdio.h> // user include. required for printf.
        """;

    // ... other stuff if you want
}
