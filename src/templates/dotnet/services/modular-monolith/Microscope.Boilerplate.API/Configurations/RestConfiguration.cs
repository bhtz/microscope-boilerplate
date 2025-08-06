using Asp.Versioning;
using Asp.Versioning.Builder;

namespace Microscope.Boilerplate.API.Configurations;

public static class RestApiVersioningConfiguration
{
    public static IServiceCollection AddRestConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = new ApiVersion(1);
            x.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(x =>
        {
            x.GroupNameFormat = "'v'V";
            x.SubstituteApiVersionInUrl = true;
        });
        
        return services;
    }
}
