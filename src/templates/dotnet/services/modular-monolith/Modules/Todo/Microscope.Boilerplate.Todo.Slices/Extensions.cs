using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using FluentValidation;
using Microscope.Boilerplate.Todo.Slices.Policies;
using Microsoft.AspNetCore.Authorization;
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

        services.AddScoped<IAuthorizationHandler, TodoListCreatedByRequirementHandler>();

        services
            .AddGraphQL()
            .AddTodoTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
    
    public static ApiVersionSet GetTodoModuleVersionSet(IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .Build();
    }
}