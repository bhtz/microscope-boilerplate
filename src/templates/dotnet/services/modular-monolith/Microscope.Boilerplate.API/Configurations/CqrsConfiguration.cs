using System.Reflection;
using Microscope.Framework.Application;

namespace Microscope.Boilerplate.API.Configurations;

public static class CqrsConfiguration
{
    public static IServiceCollection AddCqrsConfiguration(this IServiceCollection services)
    {
        services.AddCqrs(Assembly.GetExecutingAssembly());
        return services;
    }
}