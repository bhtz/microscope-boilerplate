using Microsoft.EntityFrameworkCore;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DbContextFactory;
using TickerQ.EntityFrameworkCore.DependencyInjection;

namespace Microscope.Boilerplate.Scheduler.Extensions;

public static class SchedulerExtensions
{
    public static IServiceCollection AddSchedulerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Missing connection string.");
        var dashboardUserName = configuration.GetValue<string>("Dashboard:Username") ?? throw new Exception("Missing Dashboard:UserName.");
        var dashboardPassword = configuration.GetValue<string>("Dashboard:Password") ?? throw new Exception("Missing Dashboard:Password.");

        services.AddTickerQ(options =>
        {
            options.AddOperationalStore(efOptions =>
            {
                // Use built-in TickerQDbContext with connection string
                efOptions.UseTickerQDbContext<TickerQDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseNpgsql(cs,  b => b.MigrationsAssembly(typeof(Program).Assembly));
                });
            });
    
            options.AddDashboard(dshOptions =>
            {
                dshOptions.SetBasePath("/scheduler/dashboard");
                
                // dshOptions.WithBasicAuth(dashboardUserName, dashboardPassword);
                // dshOptions.WithApiKey("your-secure-api-key");
                // dshOptions.WithNoAuth();
                dshOptions.WithHostAuthentication();
            });
        });
        
        return services;
    }
}
