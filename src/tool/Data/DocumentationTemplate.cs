namespace Microscope.Boilerplate.Tool.CLI.Data;

public sealed class DocumentationTemplate : Template, ITemplate
{
    public string Label { get; set; } = "Documentation as code";
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