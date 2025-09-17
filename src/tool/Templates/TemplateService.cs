using Microscope.Boilerplate.Tool.CLI.Templates.Aspire;
using Microscope.Boilerplate.Tool.CLI.Templates.Dotnet;
using Microscope.Boilerplate.Tool.CLI.Templates.Microscope;

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
            new SchedulerTemplate(),
            
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
            
            // Community
            new HotChocolateTemplate()
        };
    }
}