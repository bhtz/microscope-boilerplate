using System.Reflection;
using MediatR;
using Microscope.Boilerplate.Framework.CQRS.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Framework.CQRS;

public static class Extensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly targetAssembly)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(targetAssembly);
            
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(PerformanceBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
        
        return services;
    }
}