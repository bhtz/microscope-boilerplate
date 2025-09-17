namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class SchedulerTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Scheduler";
    public string CodeName { get; set; } = "mcsp_scheduler";
    
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Aspire IAC", "--Aspire"),
        new("Docker compose IAC", "--Docker"),
    ];
}