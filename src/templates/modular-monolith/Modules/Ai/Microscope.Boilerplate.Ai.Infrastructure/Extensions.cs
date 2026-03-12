using System.ClientModel;
using FluentValidation;
using Microscope.Boilerplate.Ai.Infrastructure.Services;
using Microscope.Boilerplate.Ai.Slices.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenAI;

namespace Microscope.Boilerplate.Ai.Infrastructure;

public static class AiExtensions
{
    public static IServiceCollection AddAiInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<AIOptions>()
            .BindConfiguration(AIOptions.ConfigurationKey)
            .Validate(x => new AiOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        var aiOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<AIOptions>>()
            .Value;
        
        services.AddOptions<RemoteMcpOptions>()
            .BindConfiguration(RemoteMcpOptions.ConfigurationKey)
            .Validate(x => new RemoteMcpOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        services.AddSingleton<IRemoteMcpService, RemoteMcpService>();
        
        var openAiClient = new OpenAIClient(new ApiKeyCredential(aiOptions.Key), new OpenAIClientOptions()
        {
            NetworkTimeout = TimeSpan.FromMinutes(2),
        });
        
        services
            .AddChatClient(openAiClient.GetChatClient(aiOptions.Model)
                .AsIChatClient())
            .UseFunctionInvocation()
            .Build();
        
        return services;  
    }
}

public class AIOptions
{
    public const string ConfigurationKey = "AI";

    public string Provider { get; set; } = "openai";
    public string EndPoint { get; set; }
    public string Key { get; set; }
    public string Model { get; set; }
}

public class AiOptionsValidator : AbstractValidator<AIOptions>
{
    public AiOptionsValidator()
    {
        // RuleFor(x => x.Provider)
        //     .NotNull()
        //     .NotEmpty()
        //     .Must(x => (x == "openai" || x == "ollama"))
        //     .WithMessage("AI option Provider must have a value ollama | openai");

        RuleFor(x => x.EndPoint)
            .NotNull()
            .NotEmpty()
            .WithMessage("AI option EndPoint must have a value");

        RuleFor(x => x.Key)
            .NotNull()
            .NotEmpty()
            .When(x => x.Provider == "openai");
        
        RuleFor(x => x.Model)
            .NotNull()
            .NotEmpty()
            .WithMessage("AI option Model must have a value");
    }
}