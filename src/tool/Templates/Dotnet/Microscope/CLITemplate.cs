namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class CLITemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - CLI";
    public string CodeName { get; set; } = "mcsp_cli";
}