using Marten;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore.Repositories;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten.Repositories;
using Microscope.Boilerplate.Framework.Domain.DDD;
using Microscope.Boilerplate.Framework.Infrastructure.Persistence.Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Microscope.Boilerplate.Todo.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services)
    {
        services
            .AddTodoAppInfrastructureSettings()
            .AddTodoAppInfrastructureServices();
        
        return services;
    }

    public static IServiceCollection AddTodoAppInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddPersistenceAdapter();
        
        // .AddMailAdapter()
        // .AddBusAdapter()
        // .AddStorageAdapter()
        // .AddUserAdapter();

        return services;
    }

    public static IServiceCollection AddTodoAppInfrastructureSettings(this IServiceCollection services)
    {
        services.AddOptions<PersistenceOptions>()
            .BindConfiguration(PersistenceOptions.ConfigurationKey)
            .Validate(x => new PersistenceOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        // services.AddOptions<MailOptions>()
        //     .BindConfiguration(MailOptions.ConfigurationKey)
        //     .Validate(x => new MailOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // services.AddOptions<UserOptions>()
        //     .BindConfiguration(UserOptions.ConfigurationKey)
        //     .Validate(x => new UserOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // services.AddOptions<BusOptions>()
        //     .BindConfiguration(BusOptions.ConfigurationKey)
        //     .Validate(x => new BusOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // services.AddOptions<StorageOptions>()
        //     .BindConfiguration(StorageOptions.ConfigurationKey)
        //     .ValidateDataAnnotations()
        //     .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddPersistenceAdapter(this IServiceCollection services)
    {
        var assemblyName = typeof(TodoAppDbContext).Assembly.FullName;

        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<PersistenceOptions>>()
            .Value;

        if (option.Framework == PersistenceOptions.MARTEN_FRAMEWORK)
        {
            services.AddMarten(options =>
            {
                options.Connection(option.ConnectionString);
                options.DatabaseSchemaName = option.Schema;
            }).UseLightweightSessions();
            
            services.AddScoped<IUnitOfWork, MartenUnitOfWork>();
            services.AddScoped<ITodoListRepository, MartenTodoListRepository>();
        }
        else
        {
            services.AddDbContext<TodoAppDbContext>(options =>
            {
                switch (option.Adapter)
                {
                    case PersistenceOptions.POSTGRES_ADAPTER:
                        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                        options.UseNpgsql(option.ConnectionString, o =>
                        {
                            o.MigrationsAssembly(assemblyName);
                            o.EnableRetryOnFailure();
                        });
                        break;

                    case PersistenceOptions.MSSQL_ADAPTER:
                        options.UseSqlServer(option.ConnectionString, o =>
                        {
                            o.MigrationsAssembly(assemblyName);
                            o.EnableRetryOnFailure();
                        });
                        break;

                    case PersistenceOptions.SQLITE_ADAPTER:
                        options.UseSqlite(option.ConnectionString, o => o.MigrationsAssembly(assemblyName));
                        break;

                    case PersistenceOptions.INMEMORY_ADAPTER:
                        options.UseInMemoryDatabase(assemblyName!);
                        break;

                    default:
                        options.UseInMemoryDatabase(assemblyName!);
                        break;
                }
            });

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<ITodoListRepository, EfTodoListRepository>();
        }
        
        return services;
    }
}
