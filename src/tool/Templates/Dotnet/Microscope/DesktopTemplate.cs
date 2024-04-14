namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class DesktopTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - Desktop (Avalonia)";
    public string CodeName { get; set; } = "mcsp_desktop";
}