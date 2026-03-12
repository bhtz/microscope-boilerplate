namespace Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

public sealed class BaasTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Microscope";
    public string Label { get; set; } = "Microscope - Backend as a service";
    public string CodeName { get; set; } = "mcsp_baas";

        public override List<TemplateOption> Options { get; set; } =
    [
        new("Hasura", "--Hasura"),
        new("Data API builder", "--Dab")
    ];
}
