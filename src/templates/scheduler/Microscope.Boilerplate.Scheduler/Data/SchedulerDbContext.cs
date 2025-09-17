using Microsoft.EntityFrameworkCore;
using TickerQ.EntityFrameworkCore.Configurations;

namespace Microscope.Boilerplate.Scheduler.Data;

public class SchedulerDbContext : DbContext
{
    public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply TickerQ entity configurations explicitly
        // Default Schema is "ticker".
        builder.ApplyConfiguration(new TimeTickerConfigurations());  
        builder.ApplyConfiguration(new CronTickerConfigurations()); 
        builder.ApplyConfiguration(new CronTickerOccurrenceConfigurations()); 

        // Alternatively, apply all configurations from assembly:
        // builder.ApplyConfigurationsFromAssembly(typeof(TimeTickerConfigurations).Assembly);
    }
}