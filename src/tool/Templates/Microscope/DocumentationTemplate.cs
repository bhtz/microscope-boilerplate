namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class DocumentationTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
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