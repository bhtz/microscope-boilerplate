using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class BffTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - BFF/Frontend";
    public string CodeName { get; set; } = "mcsp_bff";
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service", "--BaaS"),
        new("Reverse proxy", "--Yarp"),
        new("Aspire IAC", "--Aspire"),
        new("Docker compose IAC", "--Docker"),
        new("GraphQL Fusion Gateway", "--Gateway"),
    ];
    
    public List<TemplateOption> UiOptions { get; set; } =
    [
        new("Material UI", "--UI Material "),
        new("Fluent UI", "--UI Fluent ")
    ];

    public override IEnumerable<string> Prompt(IEnumerable<TemplateOption>? options = null)
    {
        var arguments = new List<string>();
        Func<TemplateOption, string> converter = (item) => item.Label;
        
        // ===== Common options
        
        var commonPrompt = new MultiSelectionPrompt<TemplateOption>()
            .NotRequired()
            .Title("Select options :");

        commonPrompt.Converter = converter;

        commonPrompt
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle an option, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(Options);
        
        var commonResult = AnsiConsole.Prompt(commonPrompt);
        
        commonResult.Select(x => x.Argument)
            .ToList()
            .ForEach(x => arguments.Add(x));
        
        // ===== UI options

        var uiPrompt = new SelectionPrompt<TemplateOption>()
            .Title("Select UI :")
            .AddChoices(UiOptions);

        uiPrompt.Converter = converter;
        
        var uiResult = AnsiConsole.Prompt(uiPrompt);
        
        arguments.Add(uiResult.Argument);
        
        return arguments;
    }
}