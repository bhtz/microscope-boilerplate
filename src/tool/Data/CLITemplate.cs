namespace Microscope.Boilerplate.Tool.CLI.Data;

public sealed class CLITemplate : Template, ITemplate
{
    public string Label { get; set; } = "CLI";
    public string CodeName { get; set; } = "mcsp_cli";
    public override List<TemplateOption> Options { get; set; } = [];
    public override IEnumerable<string> Prompt()
    {
        return Array.Empty<string>();
    }
}