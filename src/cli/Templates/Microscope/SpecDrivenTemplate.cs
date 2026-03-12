using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class SpecDrivenTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Spec Driven";
    public string CodeName { get; set; } = "mcsp_spec-driven";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Aspire specs", "--Aspire"),
    ];
    
    public List<TemplateOption> TemplateOptions { get; set; } =
    [
        new("BFF / Frontend", "--Template bff"),
        new("Service", "--Template service"),
        new("Docs", "--Template docs"),
        new("Scheduler", "--Template scheduler"),
        new("CLI", "--Template cli"),
        new("Desktop", "--Template desktop"),
        new("BaaS", "--Template baas"),
    ];
    
    public override IEnumerable<string> Prompt(IEnumerable<TemplateOption>? options = null)
    {
        var arguments = new List<string>();
        Func<TemplateOption, string> converter = (item) => item.Label;
        
        // ===== Common options
        
        var commonPrompt = new MultiSelectionPrompt<TemplateOption>()
            .NotRequired()
            .Title("Do you want aspire rules & skills ? :");

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

        var templatePrompt = new SelectionPrompt<TemplateOption>()
            .Title("Select template coding rules & skills :")
            .AddChoices(TemplateOptions);

        templatePrompt.Converter = converter;
        
        var uiResult = AnsiConsole.Prompt(templatePrompt);
        
        arguments.Add(uiResult.Argument);
        
        return arguments;
    }
}