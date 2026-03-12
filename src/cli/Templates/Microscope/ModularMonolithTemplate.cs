using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class ModularMonolithTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Service";
    public string CodeName { get; set; } = "mcsp_service";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Rest", "--Rest"),
        new("GraphQL", "--GraphQL"),
        new("Grpc", "--Grpc"),
        new("MCP", "--Mcp"),
        new("Unit Test", "--UnitTest"),
        new("Aspire", "--Aspire")
    ];
}
