namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class BffSsrBlazorTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "(DEPRECATED) Microscope - BFF SSR Blazor (aka #blasura)";
    public string CodeName { get; set; } = "mcsp_bff_ssr_blazor";
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service (Hasura)", "--BaaS"),
    ];
}