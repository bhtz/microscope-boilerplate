using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using AspNetCore.Authentication.ApiKey;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        // services.AddSingleton<IAuthorizationHandler, TodoListCreatedByRequirementHandler>();

        services
            .AddGraphQL()
            .AddTodoTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
    
    public static ApiVersionSet GetModuleVersionSet(IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .Build();
    }
    
    /// <summary>
    /// Applique l'autorisation commune (JWT + ApiKey) à n'importe quel builder d'endpoint
    /// </summary>
    /// <typeparam name="TBuilder">Type du builder (RouteHandlerBuilder, etc.)</typeparam>
    /// <param name="builder">Le builder d'endpoint</param>
    /// <returns>Le builder avec l'autorisation appliquée</returns>
    public static TBuilder RequireCommonAuthorization<TBuilder>(this TBuilder builder) 
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(new AuthorizeAttribute
        {
            AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},{ApiKeyDefaults.AuthenticationScheme}"
        });
    }
}