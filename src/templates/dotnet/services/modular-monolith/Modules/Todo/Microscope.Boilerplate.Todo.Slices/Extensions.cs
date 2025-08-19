#if (Grpc)
using Microscope.Boilerplate.Todo.Slices.Services;
#endif
using System.Reflection;
using FluentValidation;
using Microscope.Boilerplate.Todo.Slices.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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

        #if (GraphQL)
        services
            .AddGraphQL()
            .AddTodoTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
        #endif

        return services;
    }
    
    #if (Grpc)
    public static WebApplication MapTodoGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<TodoGrpcService>();
        return app;
    }
    #endif
}