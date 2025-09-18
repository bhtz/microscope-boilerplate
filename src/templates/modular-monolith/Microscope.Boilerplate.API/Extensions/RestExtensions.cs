using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;

namespace Microscope.Boilerplate.API.Extensions;

public static class RestExtensions
{
    public static IServiceCollection AddRestConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCarter();
        
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
