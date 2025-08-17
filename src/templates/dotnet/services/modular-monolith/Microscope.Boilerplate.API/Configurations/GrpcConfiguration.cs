// using FluentValidation;
// using Microsoft.Extensions.Options;
//
// namespace Microscope.Boilerplate.API.Configurations;
//
// public static class GrpcConfiguration
// {
//     public static IHostApplicationBuilder AddGrpcConfiguration(this IHostApplicationBuilder builder)
//     {
//         builder.Services.AddOptions<GrpcOptions>()
//             .Bind(builder.Configuration.GetSection(GrpcOptions.ConfigurationKey))
//             .Validate(x => new GrpcOptionsValidator().Validate(x).IsValid)
//             .ValidateOnStart();
//
//         var grpcOptions = builder.Services
//             .BuildServiceProvider()
//             .GetRequiredService<IOptions<GrpcOptions>>()
//             .Value;
//         
//         return builder;
//     }
// }
//
// public class GrpcOptions
// {
//     public const string ConfigurationKey = "Grpc";
//     
//     public string Pouet { get; set; }
// }
//
// public class GrpcOptionsValidator : AbstractValidator<GrpcOptions>
// {
//     public GrpcOptionsValidator()
//     {
//         RuleFor(x => x.Pouet)
//             .NotNull()
//             .NotEmpty()
//             .WithMessage("Grpc option Pouet must have a value");
//     }
// }
