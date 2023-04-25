#!/usr/bin/env dotnet-script

#r "nuget: Microsoft.Extensions.FileSystemGlobbing, 7.0.0"
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing;

static readonly string stateSmithVersion = "0.9.2-alpha";

readonly string thisDir = GetScriptFolder();
readonly string projectRootDir = thisDir + "/../";

WriteLine("Running!");

UpdateCsxFiles();

UpdateStateSmithPlugin();

WriteLine("Done!");

//////////////////////////////////////////////////

void UpdateStateSmithPlugin()
{
    Matcher matcher = new();
    matcher.AddIncludePatterns(new[] { "StateSmith-drawio-plugin*.js" });
    var pluginFilePathToCopy = matcher.GetResultsInFullPath(thisDir).Single();
    var desiredFileName = Path.GetFileName(pluginFilePathToCopy);

    matcher = new();
    matcher.AddIncludePatterns(new[] { "**/StateSmith-drawio-plugin*.js" });
    var matches = matcher.GetResultsInFullPath(projectRootDir);

    foreach (var oldFilePath in matches)
    {
        var otherFileName = Path.GetFileName(oldFilePath);
        if (otherFileName != desiredFileName)
        {
            WriteLine("Updating: " + oldFilePath);
            File.Copy(sourceFileName: pluginFilePathToCopy, Path.GetDirectoryName(oldFilePath) + "/" + desiredFileName);
            File.Delete(oldFilePath);
        }
    }

    matcher = new();
    matcher.AddIncludePatterns(new[] { "**/.vscode/settings.json" });
    matches = matcher.GetResultsInFullPath(projectRootDir);

    foreach (var settingsPath in matches)
    {
        var text = File.ReadAllText(settingsPath);
        var regex = new Regex("""
            (?xm) StateSmith-drawio-plugin- .*? .js (?=")
            """);

        var newText = regex.Replace(text, desiredFileName);
        if (newText != text)
        {
            WriteLine(settingsPath);
            File.WriteAllText(settingsPath, newText);
        }
    }
}

void UpdateCsxFiles()
{
    Matcher matcher = new();
    matcher.AddIncludePatterns(new[] { "**/*.csx" });
    var matches = matcher.GetResultsInFullPath(projectRootDir);

    foreach (var m in matches)
    {
        UpdateStateSmithNugetReference(m);
        EnsureExecutablePermission(m);
    }
}

void EnsureExecutablePermission(string filePath)
{
    var relPath = Path.GetRelativePath(projectRootDir, filePath);
    ShellHelper.RunShellCommand(workingDirectory: projectRootDir, command: "git", args: "update-index --chmod=+x " + relPath);
}

// update all .csx files to use latest version of state smith
static void UpdateStateSmithNugetReference(string filePath)
{
    var text = File.ReadAllText(filePath);
    var regex = new Regex("""
        (?xm) ^ 
        (
            \s* 
            [#]r \s+
            " \s* nuget: \s* StateSmith \s* ,
        )
        (.*?) "
        """);

    var newText = regex.Replace(text, $"""
        $1 {stateSmithVersion}"
        """);
    if (newText != text)
    {
        WriteLine(filePath);
        File.WriteAllText(filePath, newText);
    }
}



public static string GetScriptPath([CallerFilePath] string path = null) => path;
public static string GetScriptFolder([CallerFilePath] string path = null) => Path.GetDirectoryName(path);

// https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
{
    // Get information about the source directory
    var dir = new DirectoryInfo(sourceDir);

    // Check if the source directory exists
    if (!dir.Exists)
        throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

    // Cache directories before we start copying
    DirectoryInfo[] dirs = dir.GetDirectories();

    // Create the destination directory
    Directory.CreateDirectory(destinationDir);

    // Get the files in the source directory and copy to the destination directory
    foreach (FileInfo file in dir.GetFiles())
    {
        string targetFilePath = Path.Combine(destinationDir, file.Name);
        file.CopyTo(targetFilePath, overwrite: true);
    }

    // If recursive and copying subdirectories, recursively call this method
    if (recursive)
    {
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
}


public static class ShellHelper
{
    public static void RunShellCommand(string workingDirectory, string command, string args)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = command,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = workingDirectory
        };

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception($"Command '{command}' failed with error: {error}");
        }
    }
}
