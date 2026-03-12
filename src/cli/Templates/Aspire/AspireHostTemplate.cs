using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Aspire;

public sealed class AspireHostTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Aspire";
    public string Label { get; set; } = ".NET - Aspire Host";
    public string CodeName { get; set; } = "aspire-apphost";
}
