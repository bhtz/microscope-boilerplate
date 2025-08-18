namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class ModularMonolithTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - modular monolith";
    public string CodeName { get; set; } = "mcsp_service";
    
    public override bool OptionRequired { get; set; } = true;

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Rest", "--Rest"),
        new("GraphQL", "--GraphQL")
    ];
}
