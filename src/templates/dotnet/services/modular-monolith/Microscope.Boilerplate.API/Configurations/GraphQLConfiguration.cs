namespace Microscope.Management.API.Configurations;

public static class GraphQlConfiguration
{
    public static IServiceCollection AddGraphQlConfiguration(this IServiceCollection services)
    {
        services.AddErrorFilter<GraphQlErrorFilter>();
        
        services
            .AddGraphQLServer()
            .AddTypes()
            .AddAuthorization();
        
        return services;
    }
}

public class GraphQlErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        return error.WithMessage(error.Code);
    }
}
