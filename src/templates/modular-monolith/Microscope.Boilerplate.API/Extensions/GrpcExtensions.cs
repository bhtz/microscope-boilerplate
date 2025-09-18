using FluentValidation;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.API.Configurations;

public static class GrpcConfiguration
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GrpcOptions>()
            .Bind(configuration.GetSection(GrpcOptions.ConfigurationKey))
            .Validate(x => new GrpcOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        var grpcOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<GrpcOptions>>()
            .Value;
        
        services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = grpcOptions.EnableDetailedErrors;
        });
        
        return services;
    }
}

public class GrpcOptions
{
    public const string ConfigurationKey = "Grpc";

    public bool EnableDetailedErrors { get; set; } = true;
}

public class GrpcOptionsValidator : AbstractValidator<GrpcOptions>
{
    public GrpcOptionsValidator()
    {
        
    }
}
