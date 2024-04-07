using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Data;

public interface ITemplate
{
    public string Label { get; set; }
    public string CodeName { get; set; }
    public List<TemplateOption> Options { get; set; }
    public IEnumerable<string> Prompt();
} 

public class Template
{
    public string Label { get; set; }
    public string CodeName { get; set; }
    public virtual List<TemplateOption> Options { get; set; } = [];

    public virtual IEnumerable<string> Prompt()
    {
        var prompt = new MultiSelectionPrompt<TemplateOption>()
            .Title("Include options ?");

        prompt.Converter = (item) => item.Label;
        
        prompt
            .NotRequired()
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle an option, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(Options);

        var result = AnsiConsole.Prompt(prompt);
        return result.Select(x => x.Argument);
    }
}

public class TemplateOption
{
    public TemplateOption(string label, string argument)
    {
        Label = label;
        Argument = argument;
    }

    public string Label { get; set; }
    public string Argument { get; set; }
}

