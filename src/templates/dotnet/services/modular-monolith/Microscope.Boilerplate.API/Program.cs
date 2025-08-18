#if (Rest)
using Carter;
using Scalar.AspNetCore;
#endif
using Microscope.Boilerplate.API.Configurations;
using Microscope.Boilerplate.Todo.Infrastructure;
using Microscope.Boilerplate.Todo.Slices;

var builder = WebApplication.CreateBuilder(args);

#region Commons

#if (Aspire)
builder.AddServiceDefaults();
#endif

builder.Services.AddProblemDetails();
builder.Services.AddCqrsConfiguration();
builder.Services
    .AddAuthenticationConfiguration(builder.Configuration)
    .AddAuthorizationConfiguration();

#endregion

#region Protocols

#if (Rest)
builder.Services.AddOpenApiConfiguration();
builder.Services.AddRestConfiguration();
#endif

#if (GraphQL)
builder.Services.AddGraphQlConfiguration();
#endif

// builder.Services.AddGrpcConfiguration();

#endregion

#region Modules

builder.Services
    .AddTodoApplication()
    .AddTodoInfrastructure(builder.Configuration);

#endregion

var app = builder.Build();

// app.UseMiddleware<HttpExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

#if (Rest)
app.MapOpenApi();
app.MapScalarApiReference();
#endif

#if (Aspire)
app.MapDefaultEndpoints();
#endif

#if (Rest)
app.MapControllers();
app.MapCarter();
#endif

#if (GraphQL)
app.MapGraphQL();
#endif

app.RunWithGraphQLCommands(args);
