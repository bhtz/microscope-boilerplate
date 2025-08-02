namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class DabTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - Azure Data API Builder";
    public string CodeName { get; set; } = "mcsp_dab";
}
