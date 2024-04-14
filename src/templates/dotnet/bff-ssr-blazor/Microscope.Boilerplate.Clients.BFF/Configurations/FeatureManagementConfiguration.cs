using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using Microsoft.FeatureManagement;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class FeatureManagementConfiguration
{
    public static IServiceCollection AddFeatureManagementConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement(configuration.GetSection(FeatureManagementOptions.ConfigurationKey));
        services.AddScoped<IFeatureManagementService, ServerFeatureManagementService>();
        
        return services;
    }
}

public static class FeatureManagementOptions
{
    public const string ConfigurationKey = "FeatureManagement";
}

public class ServerFeatureManagementService(IFeatureManager featureManager) : IFeatureManagementService
{
    public async Task<Dictionary<string, bool>?> GetFeatureManagement()
    {
        return await featureManager.GetFeatureNamesAsync()
            .ToDictionaryAsync(
                feature => feature, 
                feature => featureManager.IsEnabledAsync(feature).GetAwaiter().GetResult());
    }
}
