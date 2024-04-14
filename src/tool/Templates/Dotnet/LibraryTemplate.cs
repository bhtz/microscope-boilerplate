namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;

public sealed class LibraryTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Dotnet";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = ".NET - Library";
    public string CodeName { get; set; } = "classlib";
    public override List<TemplateOption> Options { get; set; } = [];
}