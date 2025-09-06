using System.Reflection;
using Microscope.Boilerplate.Framework.Application.CQRS.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Framework.Application;

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
            
            // Todo: need to fix the behavior with marten transaction
            // configuration.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
        
        return services;
    }
}