using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class GrpcTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Dotnet";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = ".NET - Grpc";
    public string CodeName { get; set; } = "grpc";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Use main ?", "--use-program-main")
    ];
}