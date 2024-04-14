namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class DocumentationTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - Doc as code (vitepress)";
    public string CodeName { get; set; } = "mcsp_doc";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Product documentation", "--Product"),
        new("Tech documentation", "--Tech"),
        new("Organization & governance documentation", "--Organization"),
        new("Technical blog", "--Blog"),
        new("Opiniated guidelines", "--Guidelines")
    ];
}