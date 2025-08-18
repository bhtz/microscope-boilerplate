using Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;
using Microscope.Boilerplate.Tool.CLI.Templates.Dotnet.Microscope;
using Microscope.Boilerplate.Tool.CLI.Templates.JS;
using Microscope.Boilerplate.Tool.CLI.Templates.Rust;

namespace Microscope.Boilerplate.Tool.CLI.Templates;

public static class TemplateService
{
    public static IEnumerable<ITemplate> GetTemplates()
    {
        return new List<ITemplate>
        {
            // Custom dotnet
            new CLITemplate(),
            new DesktopTemplate(),
            new BffTemplate(),
            new ModularMonolithTemplate(),
            new DocumentationTemplate(),
            new DabTemplate(),
            
            // Some default from dotnet 
            new ConsoleTemplate(),
            new LibraryTemplate(),
            new BlazorTemplate(),
            new GrpcTemplate(),
            new WorkerTemplate(),
            new XUnitTemplate(),

            // Aspire templates
            new AspireHostTemplate(),
            new AspireServiceDefaultsTemplate(),
            
            // Custom dotnet (deprecated)
            new DistributedTemplate(),
            new BffSsrBlazorTemplate(),

            // Custom rust
            new RustCliTemplate(),
            new RustApiTemplate(),
            
            // Custom node
            new NodeCliTemplate()
        };
    }
}