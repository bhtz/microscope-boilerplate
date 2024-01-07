using System.Diagnostics;
using Cocona;
using Microscope.Boilerplate.Tool.CLI.Commands;
using Spectre.Console;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommands<SampleCommands>();
app.AddCommand(() =>
{
    AnsiConsole.Write(
        new FigletText("MICROSCOPE")
            .Color(Color.Aqua)
            .Centered());
    
    var template = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("What template would you like to use ?")
            .AddChoices(new[] {
                "mcsp_distributed",
                "mcsp_cli",
                "mcsp_doc"
            }));

    var distributedOptions = new MultiSelectionPrompt<string>()
        .Title("Include options ?")
        .NotRequired()
        .InstructionsText(
            "[grey](Press [blue]<space>[/] to toggle an option, " +
            "[green]<enter>[/] to accept)[/]")
        .AddChoices(new[]
        {
            "--CLI",
            "--E2E",
            "--BaaS",
            "--Terraform"
        });
    
    var docOptions = new MultiSelectionPrompt<string>()
        .Title("Include options ?")
        .NotRequired()
        .InstructionsText(
            "[grey](Press [blue]<space>[/] to toggle an option, " +
            "[green]<enter>[/] to accept)[/]")
        .AddChoices(new[]
        {
            "--Guidelines"
        });

    var templateOptions = template switch
    {
        "mcsp_distributed" => AnsiConsole.Prompt(distributedOptions),
        "mcsp_cli" => new List<string>(),
        "mcsp_doc" => AnsiConsole.Prompt(docOptions),
        _ => throw new ArgumentException("incorrect option"),
    };

    var name = AnsiConsole.Ask<string>("What is the name of the project ?");
    var output = AnsiConsole.Ask<string>("Where do you want output ?");
    
    RunDotnetTemplate(template, name, output, templateOptions);
});

void RunDotnetTemplate(string template, string name, string output, List<string> options)
{
    string nameArg = string.IsNullOrEmpty(name) ? string.Empty : $"-n {name}";
    string outputArg = string.IsNullOrEmpty(output) ? string.Empty : $"-o {output}";
    string templateArg = string.Join(" ", options);
    
    string arguments = $"new {template} {nameArg} {outputArg} {templateArg}";
    
    Process process = new Process();
    process.StartInfo.FileName = "dotnet";
    process.StartInfo.Arguments = arguments;

    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;

    process.Start();

    string stdout = process.StandardOutput.ReadToEnd();

    process.WaitForExit();

    Console.WriteLine(stdout);
}

app.Run();
