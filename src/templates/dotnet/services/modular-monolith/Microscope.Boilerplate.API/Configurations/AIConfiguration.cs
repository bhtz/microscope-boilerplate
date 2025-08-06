// using FluentValidation;
// using Microsoft.Extensions.Options;
//
// namespace Microscope.Management.API.Configurations;
//
// public static class AIConfiguration
// {
//     public static IHostApplicationBuilder AddAIConfiguration(this IHostApplicationBuilder builder)
//     {
//         builder.Services.AddOptions<AIOptions>()
//             .Bind(builder.Configuration.GetSection(AIOptions.ConfigurationKey))
//             .Validate(x => new AIOptionsValidator().Validate(x).IsValid)
//             .ValidateOnStart();
//
//         var aiOptions = builder.Services
//             .BuildServiceProvider()
//             .GetRequiredService<IOptions<AIOptions>>()
//             .Value;
//
//         if (aiOptions.Provider == "ollama")
//         {
//             builder
//                 .AddOllamaApiClient(aiOptions.Provider, x =>
//                 {
//                     x.Endpoint = new Uri(aiOptions.EndPoint);
//                     x.Models = new[] { aiOptions.Model };
//                     x.SelectedModel = aiOptions.Model;
//                 })
//                 .AddChatClient();
//         }
//         else
//         {
//             // use user secret here to store OPENAI Key
//             // dotnet user-secrets init
//             // dotnet user-secrets set "AI:Key" "<my-openai-key>"
//             
//             builder.AddOpenAIClient(aiOptions.Provider, x =>
//             {
//                 x.Endpoint = new Uri(aiOptions.EndPoint);
//                 x.Key = aiOptions.Key;
//             }).AddChatClient(aiOptions.Model);
//         }
//
//         return builder;
//     }
// }
//
// public class AIOptions
// {
//     public const string ConfigurationKey = "AI";
//     
//     public string Provider { get; set; }
//     public string EndPoint { get; set; }
//     public string? Key { get; set; }
//     public string Model { get; set; }
// }
//
// public class AIOptionsValidator : AbstractValidator<AIOptions>
// {
//     public AIOptionsValidator()
//     {
//         RuleFor(x => x.Provider)
//             .NotNull()
//             .NotEmpty()
//             .Must(x => (x == "openai" || x == "ollama"))
//             .WithMessage("AI option Provider must have a value ollama | openai");
//
//         RuleFor(x => x.EndPoint)
//             .NotNull()
//             .NotEmpty()
//             .WithMessage("AI option EndPoint must have a value");
//
//         RuleFor(x => x.Key)
//             .NotNull()
//             .NotEmpty()
//             .When(x => x.Provider == "openai");
//         
//         RuleFor(x => x.Model)
//             .NotNull()
//             .NotEmpty()
//             .WithMessage("AI option Model must have a value");
//     }
// }
