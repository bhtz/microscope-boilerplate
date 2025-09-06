using Cocona;
using System.Diagnostics;
using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Commands;

public class InstallCommands
{
    [Command("install")]
    public async Task Installation()
    {
        InstallMicroscopeTemplates();
        await InstallAspireTemplates();
        await InstallHotChocolateTemplates();
        await InstallAspireCli();
    }

    private void InstallMicroscopeTemplates()
    {
        LabelInstallation("Installing microscope templates ...");
        Console.WriteLine("Installing microscope templates not implemented yet ...");
    }

    private async Task InstallAspireTemplates()
    {
        var arguments = "new install Aspire.ProjectTemplates";
        
        LabelInstallation("Installing Aspire templates ...");
        await RunProcessWithArguments("dotnet", arguments);
    }

    private async Task InstallAspireCli()
    {
        var arguments = "tool install -g Aspire.Cli";
        
        LabelInstallation("Installing Aspire CLI ...");
        await RunProcessWithArguments("dotnet", arguments);
    }

    private async Task InstallHotChocolateTemplates()
    {
        var arguments = "new install HotChocolate.Templates";
        
        LabelInstallation("Installing Hotchocolate templates ...");
        await RunProcessWithArguments("dotnet", arguments);

    }

    private void LabelInstallation(string label)
    {
        AnsiConsole.Console.MarkupLine($"[bold green]{label}[/]");
    }

    private async Task RunProcessWithArguments(string fileName, string arguments)
    {
        var process = new Process();
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();
        var stdout = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();
        
        Console.WriteLine(stdout);
    }
}
