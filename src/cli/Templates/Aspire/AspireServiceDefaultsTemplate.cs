using Spectre.Console;

namespace Microscope.Boilerplate.Tool.CLI.Templates.Aspire;

public sealed class AspireServiceDefaultsTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Aspire";
    public string Label { get; set; } = ".NET - Aspire Service Defaults";
    public string CodeName { get; set; } = "aspire-servicedefaults";
}
