---
apply: always
---

# REST API Endpoints Coding Rules

Spec-driven development rules for Carter-based REST endpoints with API versioning.

## Architecture Principles

- **Minimal APIs**: Use Carter for clean, maintainable REST endpoint definitions
- **API Versioning**: Version endpoints using Asp.Versioning for backward compatibility
- **OpenAPI Documentation**: Auto-generate OpenAPI/Swagger from code
- **Status Codes**: Return proper HTTP status codes (200, 201, 400, 404, 500, etc.)
- **Request Validation**: Use FluentValidation for input validation

## Carter Endpoint Structure

### Endpoint Class Pattern

Location: `Modules/{ModuleName}/{Namespace}.Slices/Features/{UseCase}/{UseCase}Endpoints.cs`

```csharp
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

public class GetTodoListEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v{apiVersion:apiVersion}/todo/todo-lists", GetTodoLists)
            .WithApiVersionSet(TodoRestConfiguration.GetTodoModuleVersionSet(app))
            .MapToApiVersion(1)
            .RequireAuthorization();
    }
    
    private async Task<IResult> GetTodoLists([FromServices] IMediator mediator)
    {
        var resp = await mediator.Send(new GetTodoListQuery());
        return Results.Ok(resp);
    }
}
```

## API Versioning

### Setup in API/Host

```csharp
namespace Microscope.Boilerplate.API.Extensions;

public static class RestExtensions
{
    public static IServiceCollection AddRestConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCarter();
        
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = new ApiVersion(1);
            x.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(x =>
        {
            x.GroupNameFormat = "'v'V";
            x.SubstituteApiVersionInUrl = true;
        });
        
        return services;
    }
}
```

## Code Quality Standards

- **Naming**: Use PascalCase for classes/records, camelCase for parameters
- **Async/Await**: Always use async patterns with CancellationToken
- **Null Safety**: Enable nullable reference types
- **Validation**: Validate all inputs using FluentValidation
- **Error Handling**: Return proper HTTP status codes
- **Logging**: Use structured logging via ILogger<T>
- **Documentation**: Include XML comments and OpenAPI attributes

## Common HTTP Status Codes

| Code | Usage |
|------|-------|
| 200 | OK - Successful GET/PUT |
| 201 | Created - Successful POST |
| 204 | No Content - Successful DELETE |
| 400 | Bad Request - Validation failure |
| 401 | Unauthorized - Authentication required |
| 403 | Forbidden - Authorization failure |
| 404 | Not Found - Resource not found |
| 409 | Conflict - Duplicate/constraint violation |
| 500 | Internal Server Error - Server exception |
