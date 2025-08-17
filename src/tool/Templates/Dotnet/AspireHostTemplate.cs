using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class AspireHostTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Dotnet";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = ".NET - Aspire Host";
    public string CodeName { get; set; } = "aspire-apphost";
}
