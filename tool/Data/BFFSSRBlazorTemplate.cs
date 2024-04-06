namespace Microscope.Boilerplate.Tool.CLI.Data;

public sealed class BFFSSRBlazorTemplate : Template, ITemplate
{
    public string Label { get; set; } = "BFF SSR Blazor";
    public string CodeName { get; set; } = "mcsp_bff_ssr_blazor";

    public override List<TemplateOption> Options { get; set; } =
    [
        new("Backend as a service (Hasura)", "--BaaS"),
    ];
}