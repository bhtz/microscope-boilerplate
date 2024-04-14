using Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;
using Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;
using Microscope.Boilerplate.Tool.CLI.Templates.JS;
using Microscope.Boilerplate.Tool.CLI.Templates.Rust;

namespace Microscope.Boilerplate.Tool.CLI.Templates;

public static class TemplateService
{
    public static IEnumerable<ITemplate> GetTemplates()
    {
        return new List<ITemplate>()
        {
            // Some default from dotnet 
            new ConsoleTemplate(),
            new LibraryTemplate(),
            new BlazorTemplate(),
            new GrpcTemplate(),
            new WorkerTemplate(),
            new XUnitTemplate(),
            
            // Custom dotnet
            new DistributedTemplate(),
            new BffSsrBlazorTemplate(),
            new DocumentationTemplate(),
            new DesktopTemplate(),
            new CLITemplate(),
            
            // Custom rust
            new RustCliTemplate(),
            
            // Custom node
            new NodeCliTemplate()
        };
    }
}