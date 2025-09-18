namespace Microscope.Boilerplate.Clients.BFF.Extensions;

public static class ReverseProxyConfiguration
{
    public static IServiceCollection AddReverseProxyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection("ReverseProxy");
        
        if (config.Exists())
        {
            services
                .AddReverseProxy()
                .LoadFromConfig(config);
        }

        return services;
    }
}