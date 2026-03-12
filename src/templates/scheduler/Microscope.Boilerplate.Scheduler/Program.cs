using Microscope.Boilerplate.Scheduler.Extensions;
using TickerQ.DependencyInjection;
#if (Aspire)
using Microscope.Boilerplate.Clients.Scheduler.Endpoints;
#endif

var builder = WebApplication.CreateBuilder(args);

#if (Aspire)
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
#endif

builder.Services.AddSchedulerConfiguration(builder.Configuration);

var app = builder.Build();

#if (Aspire)
app.UseAuthentication();
app.UseAuthorization();
app.MapAuthenticationEndpoints();
#endif

app.UseTickerQ();

app.MapGet("/", () => Results.Redirect("/scheduler/dashboard"));

app.Run();
