namespace Microscope.Boilerplate.Tool.CLI.Templates.Rust;

public sealed class RustCliTemplate : Template, ITemplate
{
    public string Category { get; set; } = "Rust";
    public string Language { get; set; } = "Rust";
    public string Label { get; set; } = "Rust CLI";
    public string CodeName { get; set; } = "mcsp_rust_cli";
}