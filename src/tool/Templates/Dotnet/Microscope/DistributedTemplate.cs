namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class DistributedTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "Microscope - Distributed (microservice oriented)";
    public string CodeName { get; set; } = "mcsp_distributed";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("CLI", "--CLI"),
        new("End to end testing project", "--E2E"),
        new("Backend as a Service (Hasura)", "--BaaS"),
        new("Terraform IAC", "--Terraform")
    ];
}