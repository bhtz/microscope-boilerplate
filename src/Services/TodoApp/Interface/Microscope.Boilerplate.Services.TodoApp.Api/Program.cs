using Microscope.Boilerplate.Services.TodoApp.Api.Configurations;
using Microscope.Boilerplate.Services.TodoApp.Api.Middlewares;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure;
using Microscope.Boilerplate.Services.TodoApp.Infrastructure.Persistence;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// TodoApp service (app & infra)
builder.Services.AddTodoAppServices(builder.Configuration);

// Feature management
builder.Services
    .AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));

// Web services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLConfiguration(builder.Configuration);
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Configuration);
builder.Services.AddHealthCheckConfiguration(builder.Configuration);
builder.Services.AddHttpClient();

// Authentication & authorization
builder.Services.AddJwtAuthenticationConfiguration(builder.Configuration);
builder.Services.AddApiKeyAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorizationConfiguration(builder.Configuration);

var app = builder.Build();

var isMigrationEnabled = builder.Configuration.GetValue<bool>("EnableMigration");
if (isMigrationEnabled)
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<TodoAppDbContext>().Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allow-all");

app.UseMiddleware<HttpExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.MapHealthChecks("/healthchecks");

app.Run();