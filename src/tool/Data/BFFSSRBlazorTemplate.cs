namespace Microscope.Boilerplate.Tool.CLI.Data;

public sealed class BffSsrBlazorTemplate : Template, ITemplate
{
    public string Label { get; set; } = "BFF SSR Blazor (aka #blasura)";
    public string CodeName { get; set; } = "mcsp_bff_ssr_blazor";
    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service (Hasura)", "--BaaS"),
    ];
}