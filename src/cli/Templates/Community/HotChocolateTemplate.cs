namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class HotChocolateTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Community";
    public string Label { get; set; } = "Hotchocolate GraphQL";
    public string CodeName { get; set; } = "graphql";
    public override List<TemplateOption> Options { get; set; } = [];
}