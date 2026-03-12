using Microscope.Management.Product.Slices;
using Microscope.Management.Strategy.Slices;
using Microscope.Management.Tech.Slices;

namespace Microscope.Management.API.Extensions;

public static class McpExtensions
{
    public static IServiceCollection AddMcpConfiguration(this IServiceCollection services)
    {
        services
            .AddMcpServer()
            .WithHttpTransport()
            .WithToolsFromAssembly(typeof(IProductModule).Assembly)
            .WithToolsFromAssembly(typeof(IStrategyModule).Assembly)
            .WithToolsFromAssembly(typeof(ITechModule).Assembly);
        
        return services;
    }
}
