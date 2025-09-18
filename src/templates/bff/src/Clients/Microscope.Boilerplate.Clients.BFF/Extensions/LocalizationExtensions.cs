namespace Microscope.Boilerplate.Clients.BFF.Extensions;

public static class LocalizationConfiguration
{
    public static IServiceCollection AddLocalizationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        return services;
    }
}