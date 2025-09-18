namespace Microscope.Boilerplate.Clients.BFF.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddLocalizationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        return services;
    }
}