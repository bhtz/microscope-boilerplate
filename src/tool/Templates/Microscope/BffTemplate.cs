namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class BffTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - BFF/Frontend";
    public string CodeName { get; set; } = "mcsp_bff";
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service", "--BaaS"),
        new("Reverse proxy", "--Yarp"),
        new("Aspire IAC", "--Aspire"),
        new("Docker compose IAC", "--Docker"),
        new("GraphQL Fusion Gateway", "--Gateway"),
    ];
}