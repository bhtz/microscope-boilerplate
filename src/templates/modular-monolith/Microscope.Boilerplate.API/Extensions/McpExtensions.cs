using Microscope.Boilerplate.Todo.Slices;

namespace Microscope.Boilerplate.API.Extensions;

public static class McpExtensions
{
    public static IServiceCollection AddMcpConfiguration(this IServiceCollection services)
    {
        services
            .AddMcpServer()
            .WithHttpTransport()
            .AddAuthorizationFilters()
            .WithToolsFromAssembly(typeof(ITodoModule).Assembly);
        
        return services;
    }
}
