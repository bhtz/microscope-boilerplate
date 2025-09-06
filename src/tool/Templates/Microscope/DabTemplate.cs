namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class DabTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Azure Data API Builder";
    public string CodeName { get; set; } = "mcsp_dab";
}
