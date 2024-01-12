using MassTransit;
using Microscope.Boilerplate.Services.TodoApp.Application;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Storage;
using Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;
using Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.User;
using Microscope.SharedKernel;
using Microscope.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoAppInfrastructureServices(this IServiceCollection services)
    {
        services
            .AddPersistenceAdapter()
            .AddMailAdapter()
            .AddBusAdapter()
            .AddStorageAdapter()
            .AddUserAdapter();

        return services;
    }
    
    public static IServiceCollection AddTodoAppInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PersistenceOptions>()
            .Bind(configuration.GetSection(PersistenceOptions.ConfigurationKey))
            .Validate(x => new PersistenceOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<MailOptions>()
            .Bind(configuration.GetSection(MailOptions.ConfigurationKey))
            .Validate(x => new MailOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<UserOptions>()
            .Bind(configuration.GetSection(UserOptions.ConfigurationKey))
            .Validate(x => new UserOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<BusOptions>()
            .Bind(configuration.GetSection(BusOptions.ConfigurationKey))
            .Validate(x => new BusOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<StorageOptions>()
            .Bind(configuration.GetSection(StorageOptions.ConfigurationKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
    
    public static IServiceCollection AddPersistenceAdapter(this IServiceCollection services)
    {
        var assemblyName = typeof(TodoAppDbContext).Assembly.FullName;
        
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<PersistenceOptions>>()
            .Value;

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
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddStorageAdapter(this IServiceCollection services)
    {
        services.AddStorage(); // cross cutting Microscope.Storage
        services.AddScoped<IFileStorageService, FileStorageService>();
        return services;
    }
    
    public static IServiceCollection AddPdfAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddMailAdapter(this IServiceCollection services)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<MailOptions>>()
            .Value;

        switch (option.Adapter)
        {
            case MailOptions.SMTP_ADAPTER:
                services.AddScoped<IMailService, SMTPMailService>();
                break;

            case MailOptions.SENDGRID_ADAPTER:
                services.AddScoped<IMailService, SendGridMailService>();
                break;

            default:
                services.AddScoped<IMailService, SMTPMailService>();
                break;
        }
        
        return services;
    }

    public static IServiceCollection AddBusAdapter(this IServiceCollection services)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<BusOptions>>()
            .Value;
        
        services.AddScoped<IBusService, MassTransitBusService>();
        
        services.AddMassTransit(configuration =>
        {
            switch (option.Adapter)
            {
                case BusOptions.RABBITMQ_ADAPTER:
                    configuration.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(option.Host, x =>
                        {
                            x.Username(option.Username);
                            x.Password(option.Password);
                        });
                    });
                    break;

                case BusOptions.AZURE_ADAPTER:
                    throw new NotImplementedException();
                    break;
                
                case BusOptions.AWS_ADAPTER:
                    throw new NotImplementedException();
                    break;
                
                case BusOptions.INMEMORY_ADAPTER:
                    configuration.UsingInMemory();
                    break;
            }
        });

        return services;
    }
    
    public static IServiceCollection AddUserAdapter(this IServiceCollection services)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<UserOptions>>()
            .Value;
        
        switch (option.Adapter)
        {
            case UserOptions.KEYCLOAK_ADAPTER :
                services.AddScoped<IUserService, KeycloakUserService>();
                break;
        }
        
        return services;
    }
}
