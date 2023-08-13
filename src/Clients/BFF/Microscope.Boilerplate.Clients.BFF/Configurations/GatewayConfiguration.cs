namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class CorsConfiguration
{
    public static IServiceCollection AddGraphQlGateway(this IServiceCollection services, IConfiguration configuration)
    {
        var requestExecutorBuilder = services.AddGraphQLServer();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<AuthenticationHeaderHandler>();
        
        var schemas = configuration.GetSection("GraphQLGateway:Schemas").Get<GqlSchemas[]>();
        
        foreach (var schema in schemas)
        {
            services
                .AddHttpClient(schema.Name, c => c.BaseAddress = new Uri(schema.Url))
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
            
            requestExecutorBuilder.AddRemoteSchema(schema.Name);
        }

        requestExecutorBuilder.AddTypeExtensionsFromFile("./stitching.graphql");

        return services;
    }
    
    public record GqlSchemas(string Name, string Url);
}