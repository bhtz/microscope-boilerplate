using Marten;
using Marten.Services;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore.Repositories;
using Microscope.Boilerplate.Todo.Infrastructure.Persistence.Marten.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Todo.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddTodoInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddTodoAppInfrastructureSettings(configuration)
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

    public static IServiceCollection AddTodoAppInfrastructureSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<PersistenceOptions>()
            .BindConfiguration(PersistenceOptions.ConfigurationKey)
            .Validate(x => new PersistenceOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        // services.AddOptions<MailOptions>()
        //     .Bind(configuration.GetSection(MailOptions.ConfigurationKey))
        //     .Validate(x => new MailOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // services.AddOptions<UserOptions>()
        //     .Bind(configuration.GetSection(UserOptions.ConfigurationKey))
        //     .Validate(x => new UserOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // services.AddOptions<BusOptions>()
        //     .Bind(configuration.GetSection(BusOptions.ConfigurationKey))
        //     .Validate(x => new BusOptionsValidator().Validate(x).IsValid)
        //     .ValidateOnStart();
        //
        // services.AddOptions<StorageOptions>()
        //     .Bind(configuration.GetSection(StorageOptions.ConfigurationKey))
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
            
            // services.AddScoped<IUnitOfWork, UnitOfWork>();
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
                        options.UseInMemoryDatabase(assemblyName);
                        break;

                    default:
                        options.UseInMemoryDatabase(assemblyName);
                        break;
                }
            });

            // services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITodoListRepository, EfTodoListRepository>();
        }


        return services;
    }
}