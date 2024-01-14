namespace Microscope.Boilerplate.Tool.CLI.Data;

public sealed class DesktopTemplate : Template, ITemplate
{
    public string Label { get; set; } = "Desktop (cross platform)";
    public string CodeName { get; set; } = "mcsp_desktop";
    public override List<TemplateOption> Options { get; set; } = [];
    public override IEnumerable<string> Prompt()
    {
        return Array.Empty<string>();
    }
}