using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Todo.Slices;

public static class Extensions
{
    public static IServiceCollection AddTodoApplication(this IServiceCollection services)
    {
        var execAssembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(execAssembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(execAssembly);
            // add specific behaviors if needed
        });

        // services.AddSingleton<IAuthorizationHandler, TodoListCreatedByRequirementHandler>();

        services.AddGraphQL().AddTodoTypes();

        return services;
    }
    
    public static ApiVersionSet GetModuleVersionSet(IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .Build();
    }
}