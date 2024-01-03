using System.Reflection;
using FluentValidation;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Common.Behaviors;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Policies;
using Microscope.Boilerplate.Services.TodoApp.Application.Policies.CreatedByRequirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Services.TodoApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoApplication(this IServiceCollection services)
    {
        var execAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(execAssembly);
        services.AddValidatorsFromAssembly(execAssembly);
        services.AddMediatR(execAssembly);

        services.AddSingleton<IAuthorizationHandler, TodoListCreatedByRequirementHandler>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}
