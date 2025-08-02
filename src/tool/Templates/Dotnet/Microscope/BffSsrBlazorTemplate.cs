namespace Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;

public sealed class BffSsrBlazorTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Custom";
    public string Language { get; set; } = "Dotnet";
    public string Label { get; set; } = "(DEPRECATED) Microscope - BFF SSR Blazor (aka #blasura)";
    public string CodeName { get; set; } = "mcsp_bff_ssr_blazor";
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service (Hasura)", "--BaaS"),
    ];
}