namespace Microscope.Boilerplate.API.Configurations;

public static class AuthorizationConfiguration
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // if jwt has specific claims for roles
            // options.AddPolicy("Administrator", policy => policy.RequireClaim("permissions", "administrator"));
        });

        return services;
    }
}
