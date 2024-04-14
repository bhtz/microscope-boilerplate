namespace Microscope.Boilerplate.Tool.CLI.Templates.JS;

public sealed class NodeCliTemplate : Template, ITemplate
{
    public string Category { get; set; } = "JS";
    public string Language { get; set; } = "JS";
    public string Label { get; set; } = "Node CLI";
    public string CodeName { get; set; } = "mcsp_node_cli";
}