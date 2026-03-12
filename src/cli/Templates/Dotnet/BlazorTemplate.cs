using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class BlazorTemplate : Template, ITemplate
{
    public string Category { get; set; } = ".NET";
    public string Label { get; set; } = ".NET - Blazor";
    public string CodeName { get; set; } = "blazor";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Authentication", "--auth Individual"),
        new("Use main ?", "--use-program-main")
    ];

    public override IEnumerable<string> Prompt(IEnumerable<TemplateOption>? options = null)
    {
        var framework = PromptFramework();
        var interactivity = PromptInteractivity();
        return base.Prompt(new[] { framework, interactivity });
    }

    private TemplateOption PromptFramework()
    {
        var prompt = new SelectionPrompt<TemplateOption>();
        prompt.Title = "Select framework";
        prompt.Converter = (item) => item.Label;
        prompt.AddChoices(new[]
        {
            new TemplateOption("net8.0", "-f net8.0"),
            new TemplateOption("net7.0", "-f net7.0"),
            new TemplateOption("net6.0", "-f net6.0"),
            new TemplateOption("net5.0", "-f net5.0"),
            new TemplateOption("netcoreapp3.1", "-f netcoreapp3.1"),
        });

        return AnsiConsole.Prompt(prompt);
    }
    
    private TemplateOption PromptInteractivity()
    {
        var prompt = new SelectionPrompt<TemplateOption>();
        prompt.Title = "Select interactive mode";
        prompt.Converter = (item) => item.Label;
        prompt.AddChoices(new[]
        {
            new TemplateOption("None", "--interactivity None"),
            new TemplateOption("Server", "--interactivity Server"),
            new TemplateOption("Webassembly", "--interactivity Webassembly"),
            new TemplateOption("Auto", "--interactivity Auto"),
        });

        return AnsiConsole.Prompt(prompt);
    }
}