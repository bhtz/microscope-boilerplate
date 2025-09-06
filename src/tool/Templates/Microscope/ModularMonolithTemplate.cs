using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class ModularMonolithTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Modular monolith";
    public string CodeName { get; set; } = "mcsp_service";

    public List<TemplateOption> ProtocolOptions { get; set; } =
    [
        new("Rest", "--Rest"),
        new("GraphQL", "--GraphQL"),
        new("Grpc", "--Grpc")
    ];
    
    public  List<TemplateOption> IacOptions { get; set; } =
    [
        new("Docker", "--Docker"),
        new("Aspire", "--Aspire")
    ];
    
    public override IEnumerable<string> Prompt(IEnumerable<TemplateOption>? options = null)
    {
        var arguments = new List<string>();
        Func<TemplateOption, string> converter = (item) => item.Label;
        
        var protocolPrompt = new MultiSelectionPrompt<TemplateOption>()
            .NotRequired()
            .Title("Select protocol options :");

        protocolPrompt.Converter = converter;

        protocolPrompt
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle an option, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(ProtocolOptions);
        
        var protocolResult = AnsiConsole.Prompt(protocolPrompt);
        
        protocolResult.Select(x => x.Argument)
            .ToList()
            .ForEach(x => arguments.Add(x));
        
        var iacPrompt = new MultiSelectionPrompt<TemplateOption>()
            .NotRequired()
            .Title("Select IAC options :");

        iacPrompt.Converter = converter;

        iacPrompt
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle an option, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices(IacOptions);
        
        var iacResult = AnsiConsole.Prompt(iacPrompt);
        
        iacResult.Select(x => x.Argument)
            .ToList()
            .ForEach(x => arguments.Add(x));
        
        return arguments;
    }
}
