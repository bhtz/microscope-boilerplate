using System.Diagnostics;
using Cocona;
using Microscope.Boilerplate.Tool.CLI.Commands;
using Microscope.Boilerplate.Tool.CLI.Templates;
using Spectre.Console;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommands<InstallCommands>();

app.AddCommand(() =>
{
    AnsiConsole.Write(
        new FigletText("MICROSCOPE")
            .Color(Color.Aqua)
            .Centered());

    var category = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select template language")
            .AddChoices(new[]
            {
                "Microscope",
                ".NET",
                "Aspire",
                "Community"
            }));

    var selectedTemplate = PromptTemplates(category);
    var templateArguments = selectedTemplate.Prompt();
    var name = AnsiConsole.Ask<string>("What is the name of the project ?");
    var output = AnsiConsole.Ask<string>("Where do you want output ?");

    GenerateDotnetTemplate(selectedTemplate.CodeName, name, output, templateArguments);
});

ITemplate PromptTemplates(string category)
{
    var templates = TemplateService.GetTemplates()
        .Where(x => x.Category == category);

    var prompt = new SelectionPrompt<ITemplate>()
        .Title($"Select {category} template you like to use :");

    prompt.AddChoices(templates);
    prompt.Converter = (tpl) => tpl.Label;
    return AnsiConsole.Prompt(prompt);
}

void GenerateDotnetTemplate(string template, string name, string output, IEnumerable<string> options)
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
    var stdout = process.StandardOutput.ReadToEnd();
    process.WaitForExit();
    Console.WriteLine(stdout);
}

app.Run();