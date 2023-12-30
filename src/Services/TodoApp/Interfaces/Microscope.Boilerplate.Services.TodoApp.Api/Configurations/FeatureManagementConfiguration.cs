using Microsoft.FeatureManagement;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class FeatureManagementConfiguration
{
    public static IServiceCollection AddFeatureManagementConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement(configuration.GetSection("FeatureManagement"));
        
        return services;
    }
}
