using FluentValidation;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class GatewayConfiguration
{
    private static IServiceCollection ValidateGatewayConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GraphQLGatewayOptions>()
            .Bind(configuration.GetSection(GraphQLGatewayOptions.ConfigurationKey))
            .Validate(x => new GraphQLGatewayOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        return services;
    }
    
    public static IServiceCollection AddGraphQlGatewayConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ValidateGatewayConfiguration(configuration);
        
        var gatewayOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<GraphQLGatewayOptions>>()
            .Value;
        
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<BffAuthenticationHeaderHandler>();
        
        var builder = services.AddGraphQLServer();

        foreach (var scalar in gatewayOptions.Scalars)
        {
            builder.AddType(new AnyType(scalar));
        }

        foreach (var schema in gatewayOptions.Schemas)
        {
            services
                .AddHttpClient(schema.Name, c => c.BaseAddress = new Uri(schema.Url))
                .AddHttpMessageHandler<BffAuthenticationHeaderHandler>();

            builder.AddRemoteSchema(schema.Name);
        
            var subgraph = services.AddGraphQL(schema.Name);
            foreach (var scalar in gatewayOptions.Scalars)
            {
                subgraph.AddType(new AnyType(scalar));
            }
        }
    
        builder.AddTypeExtensionsFromFile("./stitching.graphql");
        
        return services;
    }

    public class GraphQLGatewayOptions
    {
        public const string ConfigurationKey = "GraphQLGateway";
        public IEnumerable<GatewaySchema> Schemas { get; set; } = new List<GatewaySchema>();
        public IEnumerable<string> Scalars { get; set; } = new List<string>();
    }

    public record GatewaySchema(string Name, string Url);
    
    public class GraphQLGatewayOptionsValidator : AbstractValidator<GraphQLGatewayOptions>
    {
        public GraphQLGatewayOptionsValidator()
        {
            RuleFor(x => x.Schemas)
                .NotNull()
                .WithMessage("GraphQLGateway option Schemas must have a value");
        }
    }
}