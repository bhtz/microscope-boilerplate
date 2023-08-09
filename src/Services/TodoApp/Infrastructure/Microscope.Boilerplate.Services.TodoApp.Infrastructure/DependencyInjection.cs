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

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTodoApplication();
        services.AddPersistenceAdapter(configuration);
        services.AddMailAdapter(configuration);
        services.AddBusAdapter(configuration);
        services.AddStorageAdapter(configuration);
        
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
        services.AddStorage(configuration); // cross cutting Microscope.Storage
        services.AddScoped<IFileStorageService, FileStorageService>();
        return services;
    }
    
    public static IServiceCollection AddPdfAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddMailAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var option = new MailOptions();
        var section = configuration.GetSection(MailOptions.ConfigurationKey);
        section.Bind(option);
        
        services.AddOptions<MailOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

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

    public static IServiceCollection AddBusAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBusService, MassTransitBusService>();
        
        var option = new BusOptions();
        var section = configuration.GetSection(BusOptions.ConfigurationKey);
        section.Bind(option);
        
        services.AddOptions<BusOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
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
