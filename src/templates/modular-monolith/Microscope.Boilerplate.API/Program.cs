#if (Rest)
using Carter;
using Scalar.AspNetCore;
#endif

#if (Mcp)
using Microscope.Boilerplate.Ai.Infrastructure;
using Microscope.Boilerplate.Ai.Slices;
#endif

using Microscope.Boilerplate.API.Extensions;
#if (Rest)
using Microscope.Boilerplate.API.Endpoints;
#endif
using Microscope.Boilerplate.Todo.Infrastructure;
using Microscope.Boilerplate.Todo.Slices;

var builder = WebApplication.CreateBuilder(args);

#region Commons

#if (Aspire)
builder.AddServiceDefaults();
#endif

builder.Services.AddFeatureManagementConfiguration(builder.Configuration);
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

#if (Grpc)
builder.Services.AddGrpcConfiguration(builder.Configuration);
#endif

#if (Mcp)
builder.Services.AddMcpConfiguration();
#endif

#endregion

#region Modules

builder.Services
    .AddTodoApplication()
    .AddTodoInfrastructure();

#if (Mcp)
builder.Services
    .AddAiApplication()
    .AddAiInfrastructure();
#endif

#endregion

var app = builder.Build();

// app.UseMiddleware<HttpExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

#if (Rest)
app.MapOpenApi();
app.MapScalarApiReference();
app.MapFeatureManagementEndpoints();
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

#if (Grpc)
app.MapTodoGrpcServices();
#endif

#if (Mcp)
app.MapMcp("/mcp")
    .RequireAuthorization();
#endif

#if (GraphQL)
app.RunWithGraphQLCommands(args);
#else
app.Run();
#endif
