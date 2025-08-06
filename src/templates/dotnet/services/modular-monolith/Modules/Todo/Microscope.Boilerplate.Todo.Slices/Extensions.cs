using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using FluentValidation;
using MediatR;
using Microscope.Boilerplate.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        services.RegisterCarterModules();

        return services;
    }

    public static IServiceCollection RegisterCarterModules(this IServiceCollection services)
    {
        var carterModules = typeof(ITodoModule).Assembly.GetTypes()
            .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && 
                        !t.IsInterface && 
                        !t.IsAbstract)
            .ToArray();

        services.AddCarter(configurator: c =>
        {
            c.WithModules(carterModules);
        });
        
        return services;
    }
    
    public static ApiVersionSet GetModuleVersionSet(IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .Build();
    }
}