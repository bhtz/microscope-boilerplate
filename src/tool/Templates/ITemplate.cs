using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates;

public interface ITemplate
{
    public string Category { get; set; }
    public string Language { get; set; }
    public string Label { get; set; }
    public string CodeName { get; set; }
    public bool OptionRequired { get; set; }
    public List<TemplateOption> Options { get; set; }
    public IEnumerable<string> Prompt(IEnumerable<TemplateOption>? options = null);
} 

public class Template
{
    public string Label { get; set; }
    public string CodeName { get; set; }
    public virtual bool OptionRequired { get; set; } = false;
    public virtual List<TemplateOption> Options { get; set; } = [];

    public virtual IEnumerable<string> Prompt(IEnumerable<TemplateOption>? options = null)
    {
        var prompt = new MultiSelectionPrompt<TemplateOption>()
            .Title("Include options ?");

        prompt.Converter = (item) => item.Label;

        if (OptionRequired)
        {
            prompt.Required();
        }
        else
        {
            prompt.NotRequired();
        }

        prompt
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle an option, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(Options);

        if (Options.Count == 0)
        {
            return new List<string>();
        }
        
        var result = AnsiConsole.Prompt(prompt);

        if (options is not null)
        {
            result.AddRange(options);
        }
        
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

