using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Ai.Slices;

public static class Extensions
{
    public static IServiceCollection AddAiApplication(this IServiceCollection services)
    {
        var execAssembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(execAssembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(execAssembly);
            // add specific behaviors if needed
        });
        
        #if (GraphQL) 
        services
            .AddGraphQL()
            .AddAiTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
         #endif
        return services;   
    }
}