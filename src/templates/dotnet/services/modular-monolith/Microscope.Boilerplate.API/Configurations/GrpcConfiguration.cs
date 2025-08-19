using FluentValidation;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.API.Configurations;

public static class GrpcConfiguration
{
    public static IServiceCollection AddGrpcConfiguration(this IServiceCollection services)
    {
        // builder.Services.AddOptions<GrpcOptions>()
        //     .Bind(builder.Configuration.GetSection(GrpcOptions.ConfigurationKey))
        //     .Validate(x => new GrpcOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // var grpcOptions = builder.Services
        //     .BuildServiceProvider()
        //     .GetRequiredService<IOptions<GrpcOptions>>()
        //     .Value;
        
        services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });
        
        return services;
    }
}

public class GrpcOptions
{
    public const string ConfigurationKey = "Grpc";
    
    public string Pouet { get; set; }
}

public class GrpcOptionsValidator : AbstractValidator<GrpcOptions>
{
    public GrpcOptionsValidator()
    {
        RuleFor(x => x.Pouet)
            .NotNull()
            .NotEmpty()
            .WithMessage("Grpc option Pouet must have a value");
    }
}
