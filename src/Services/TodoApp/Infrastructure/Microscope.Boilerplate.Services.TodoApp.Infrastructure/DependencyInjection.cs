using MassTransit;
using Microscope.Boilerplate.Services.TodoApp.Application;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Storage;
using Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;
using Microscope.SharedKernel;
using Microscope.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTodoAppSettings(configuration)
            .AddTodoApplication()
            .AddPersistenceAdapter()
            .AddMailAdapter()
            .AddBusAdapter()
            .AddStorageAdapter();

        return services;
    }
    
    public static IServiceCollection AddTodoAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PersistenceOptions>()
            .Bind(configuration.GetSection(PersistenceOptions.ConfigurationKey))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<MailOptions>()
            .Bind(configuration.GetSection(MailOptions.ConfigurationKey))
            .Validate(x => new MailOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<BusOptions>()
            .Bind(configuration.GetSection(BusOptions.ConfigurationKey))
            .ValidateDataAnnotations()
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
                case "postgres":
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    options.UseNpgsql(option.ConnectionString, o => o.MigrationsAssembly(assemblyName));
                    break;

                case "mssql":
                    options.UseSqlServer(option.ConnectionString, o => o.MigrationsAssembly(assemblyName));
                    break;
                
                case "sqlite":
                    options.UseSqlite(option.ConnectionString, o => o.MigrationsAssembly(assemblyName));
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
            case "smtp":
                services.AddScoped<IMailService, SMTPMailService>();
                break;

            case "sendgrid":
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
                case "rabbitmq":
                    configuration.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(option.Host, x =>
                        {
                            x.Username(option.Username);
                            x.Password(option.Password);
                        });
                    });
                    break;

                case "azure":
                    throw new NotImplementedException();
                    break;

                default:
                    configuration.UsingInMemory();
                    break;
            }
        });

        return services;
    }
    
    public static IServiceCollection AddAiAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
