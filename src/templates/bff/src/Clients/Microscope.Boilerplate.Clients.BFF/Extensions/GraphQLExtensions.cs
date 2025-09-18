using HotChocolate;

namespace Microscope.Boilerplate.BFF.Extensions;

public static class GraphQlConfiguration
{
    public static IServiceCollection AddGraphQlConfiguration(this IServiceCollection services)
    {
        services
            .AddGraphQLServer("bff")
            .AddErrorFilter<GraphQlErrorFilter>()
            .AddAuthorization()
            .AddBffTypes();
        
        return services;
    }
}

public class GraphQlErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        var code = string.IsNullOrWhiteSpace(error.Code) ? "SERVER_ERROR" : error.Code;
        var message = string.IsNullOrWhiteSpace(error.Exception?.Message)
            ? (string.IsNullOrWhiteSpace(error.Message) ? code : error.Message)
            : error.Exception!.Message;

        return error
            .WithCode(code)
            .WithMessage(message);
    }
}
