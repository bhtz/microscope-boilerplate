namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class XUnitTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Dotnet";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = ".NET - Xunit";
    public string CodeName { get; set; } = "xunit";
}