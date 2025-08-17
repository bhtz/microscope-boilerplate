using Carter;
using Microscope.Boilerplate.API.Configurations;
using Microscope.Boilerplate.API.Services;
using Microscope.Boilerplate.Todo.Infrastructure;
using Microscope.Boilerplate.Todo.Slices;
using Microscope.Framework.Application.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region Commons

// builder.AddServiceDefaults();
// builder.AddAIConfiguration();
builder.Services.AddProblemDetails();
builder.Services.AddCqrsConfiguration();
builder.Services
    .AddAuthenticationConfiguration(builder.Configuration)
    .AddAuthorizationConfiguration();

#endregion

#region Protocols

builder.Services.AddOpenApiConfiguration();
builder.Services.AddRestConfiguration();
builder.Services.AddGraphQlConfiguration();

#endregion

#region Modules

builder.Services
    .AddTodoApplication()
    .AddTodoInfrastructure(builder.Configuration);

#endregion

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference();

// app.MapDefaultEndpoints();
app.MapControllers();
app.MapCarter();
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
