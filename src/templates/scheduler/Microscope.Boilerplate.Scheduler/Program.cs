using Microscope.Boilerplate.Scheduler.Data;
using Microsoft.EntityFrameworkCore;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Missing connection string.");

builder.Services.AddDbContext<SchedulerDbContext>(options => options.UseNpgsql(cs));
builder.Services.AddTickerQ(options =>
{
    options.AddOperationalStore<SchedulerDbContext>(efOpts =>
    {
        efOpts.UseModelCustomizerForMigrations();
        efOpts.CancelMissedTickersOnAppStart();
    });
    
    options.AddDashboard(dshOptions =>
    {
        dshOptions.BasePath = "/";
        dshOptions.EnableBasicAuth = true;
    });
});

var app = builder.Build();

app.UseTickerQ();

app.Run();
