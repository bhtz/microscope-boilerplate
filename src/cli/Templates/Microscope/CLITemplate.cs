namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class CLITemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - CLI";
    public string CodeName { get; set; } = "mcsp_cli";
}