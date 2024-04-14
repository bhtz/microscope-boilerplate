using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<SwaggerOptions>>()
            .Value;
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(option.Name, new OpenApiInfo { Title = option.Title, Version = option.Version });
        });
        
        return services;
    }
}

public class SwaggerOptions
{
    public const string ConfigurationKey = "Swagger";

    public string Name { get; set; } = "v1";
    public string Title { get; set; } = "TodoApp.Api";
    public string Version { get; set; } = "v1";
}

public class SwaggerOptionsValidator : AbstractValidator<SwaggerOptions>
{
    public SwaggerOptionsValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("API Swagger document must have a name");
        
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("API Swagger document must have a title");
        
        RuleFor(x => x.Version)
            .NotNull()
            .NotEmpty()
            .Must(x => x.StartsWith("v"))
            .WithMessage("API Swagger document must have a version start with 'v'");

    }
}
