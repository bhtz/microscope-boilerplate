namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class GatewayConfiguration
{
    public static IServiceCollection AddGraphQlGateway(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<AuthenticationHeaderHandler>();
        
        var option = new GatewayOptions();
        configuration.GetSection("GraphQLGateway").Bind(option);

        var builder = services.AddGraphQLServer();

        foreach (var scalar in option.Scalars)
        {
            builder.AddType(new AnyType(scalar));
        }

        foreach (var schema in option.Schemas)
        {
            services
                .AddHttpClient(schema.Name, c => c.BaseAddress = new Uri(schema.Url))
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();

            builder.AddRemoteSchema(schema.Name);
        
            var subgraph = services.AddGraphQL(schema.Name);
            foreach (var scalar in option.Scalars)
            {
                subgraph.AddType(new AnyType(scalar));
            }
        }
    
        builder.AddTypeExtensionsFromFile("./stitching.graphql");
        
        return services;
    }

    public class GatewayOptions
    {
        public IEnumerable<GatewaySchema> Schemas { get; set; }
        public IEnumerable<string> Scalars { get; set; }
    }

    public record GatewaySchema(string Name, string Url);
}