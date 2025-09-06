namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class ReverseProxyConfiguration
{
    public static IServiceCollection AddReverseProxyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));

        return services;
    }
}