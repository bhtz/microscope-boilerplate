using Microscope.Boilerplate.Services.TodoApp.Application;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories;
using Microscope.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTodoApplication();
        services.AddPersistenceAdapter(configuration);
        
        return services;
    }
    
    public static IServiceCollection AddPersistenceAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceName = "TodoAppService";
        var provider = configuration.GetValue<string>("DatabaseProvider");
        var connectionString = configuration.GetConnectionString(serviceName);
        var assemblyName = typeof(TodoAppDbContext).Assembly.FullName;

        services.AddDbContext<TodoAppDbContext>(options =>
        {
            switch (provider)
            {
                case "postgres":
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    options.UseNpgsql(connectionString, o => o.MigrationsAssembly(assemblyName));
                    break;

                case "mssql":
                    options.UseSqlServer(connectionString, o => o.MigrationsAssembly(assemblyName));
                    break;
                
                case "sqlite":
                    options.UseSqlite(connectionString, o => o.MigrationsAssembly(assemblyName));
                    break;

                default:
                    options.UseInMemoryDatabase(serviceName);
                    break;
            }
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddStorageAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddPdfAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddMailAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddAiAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
