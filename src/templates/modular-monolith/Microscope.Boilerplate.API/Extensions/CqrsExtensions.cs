using System.Reflection;
using Microscope.Boilerplate.Framework.Application;

namespace Microscope.Boilerplate.API.Extensions;

public static class CqrsExtensions
{
    public static IServiceCollection AddCqrsConfiguration(this IServiceCollection services)
    {
        services.AddCqrs(Assembly.GetExecutingAssembly());
        return services;
    }
}