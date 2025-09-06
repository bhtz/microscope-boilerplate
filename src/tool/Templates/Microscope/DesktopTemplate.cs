namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class DesktopTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Desktop (Avalonia)";
    public string CodeName { get; set; } = "mcsp_desktop";
}