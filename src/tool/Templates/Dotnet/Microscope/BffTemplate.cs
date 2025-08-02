namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class BffTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - BFF ";
    public string CodeName { get; set; } = "mcsp_bff";
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service", "--BaaS"),
    ];
}