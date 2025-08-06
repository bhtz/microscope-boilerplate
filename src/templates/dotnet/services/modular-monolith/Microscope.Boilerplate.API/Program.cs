using System.Reflection;
using Carter;
using Microscope.Boilerplate.API.Configurations;
using Microscope.Boilerplate.Framework.CQRS;
using Microscope.Boilerplate.Todo.Slices;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// builder.AddServiceDefaults();
// builder.AddAIConfiguration();
builder.Services.AddProblemDetails();
builder.Services.AddCqrsConfiguration();

#region Protocols

builder.Services.AddRestConfiguration();
builder.Services.AddGraphQlConfiguration();

#endregion

#region Modules

builder.Services
    .AddTodoApplication();

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
