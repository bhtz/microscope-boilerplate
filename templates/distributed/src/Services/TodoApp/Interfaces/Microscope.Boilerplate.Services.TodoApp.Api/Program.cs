using Microscope.Boilerplate.Services.TodoApp.Api.Configurations;
using Microscope.Boilerplate.Services.TodoApp.Api.Middlewares;
using Microscope.Boilerplate.Services.TodoApp.Application;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// register host / web layer options & services
builder.Services
    .AddWebSettings(builder.Configuration)
    .AddWebServices(builder.Configuration);

// register TodoApp options & services
builder.Services
    .AddTodoApplication()
    .AddTodoAppInfrastructureSettings(builder.Configuration)
    .AddTodoAppInfrastructureServices();

var app = builder.Build();
    
using var scope = app.Services.CreateScope();
var persistenceOptions = scope.ServiceProvider.GetRequiredService<IOptions<PersistenceOptions>>();
if (persistenceOptions.Value.EnableMigration)
{
    scope.ServiceProvider.GetRequiredService<TodoAppDbContext>().Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHsts();

app.UseCors("allow-all");

app.UseMiddleware<HttpExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.MapHealthChecks("/health");
app.MapHealthChecks("/alive", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live")
});

app.Run();
