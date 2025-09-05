using FluentValidation;
using HotChocolate.Types;
using Microscope.Boilerplate.BFF.Configurations.Http;
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
    
    public static IServiceCollection AddGraphQlGatewayConfiguration(this IServiceCollection services, IHostEnvironment env, IConfiguration configuration)
    {
        services.ValidateGatewayConfiguration(configuration);
        
        var gatewayOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<GraphQLGatewayOptions>>()
            .Value;
        
        services.AddScoped<GatewayAuthenticationHeaderHandler>();
        
        services
            .AddHttpClient("Fusion")
            .AddHttpMessageHandler<GatewayAuthenticationHeaderHandler>();

        var fgpPath = env.IsDevelopment() ? "gateway.Development.fgp" : "gateway.fgp";
        
        var graphqlFusionGatewayBuilder = services
            .AddFusionGatewayServer()
            .ConfigureFromFile(fgpPath);

        foreach (var scalar in gatewayOptions.Scalars)
        {
            graphqlFusionGatewayBuilder
                .CoreBuilder
                .AddType(new AnyType(scalar));
        }

        // do not enable query plan in production !
        if (env.IsDevelopment())
        {
            graphqlFusionGatewayBuilder.ModifyFusionOptions(x =>
            {
                x.AllowQueryPlan = true;
                x.IncludeDebugInfo = true;
            });
        }
        
        return services;
    }

    public class GraphQLGatewayOptions
    {
        public const string ConfigurationKey = "GraphQLGateway";
        public IEnumerable<string> Scalars { get; set; } = new List<string>();
    }
    
    public class GraphQLGatewayOptionsValidator : AbstractValidator<GraphQLGatewayOptions>
    {
        public GraphQLGatewayOptionsValidator()
        {
            RuleFor(x => x.Scalars)
                .NotNull()
                .WithMessage("GraphQLGateway option Scalars should not null");
        }
    }
}