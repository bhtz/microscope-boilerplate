namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class WorkerTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Dotnet";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = ".NET - Worker";
    public string CodeName { get; set; } = "worker";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Use main ?", "--use-program-main")
    ];
}