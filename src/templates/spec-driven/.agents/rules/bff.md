---
apply: always
---

# BFF (Backend for Frontend) Coding Rules

Spec-driven development rules for BFF simple endpoints, GraphQL resolvers, and authentication flows.

## Architecture Principles

- **Security by Isolation**: The BFF is the only component handling sensitive tokens (Access/Refresh tokens)
- **Simple Routes & Operations**: REST endpoints and GraphQL resolvers for local server operations
- **Gateway Federation & Reverse Proxy**: See [Gateway Rules](./.gateway.md) for YARP and GraphQL gateway configuration

## REST Endpoints (Carter)

### Feature Flags Endpoint

Register in `Program.cs` via `app.MapFeatureManagementEndpoints();`

```csharp
using Microscope.Boilerplate.Clients.Web.Shared.Services;

namespace Microscope.Boilerplate.Clients.BFF.Endpoints;

public static class FeatureManagementEndpoints
{
    public static void MapFeatureManagementEndpoints(this WebApplication app)
    {
        app.MapGet("/api/features", FeatureFlags);
    }

    private static async Task<Dictionary<string, bool>?> FeatureFlags(IFeatureManagementService featureManagementService)
    {
        return await featureManagementService.GetFeatureManagement();
    }
}
```

## GraphQL Resolvers (HotChocolate)

### Feature Flags Query

Register in `Program.cs` via `app.MapGraphQL("/graphql", "bff");`:

```csharp
namespace Microscope.Boilerplate.Todo.Slices.Features.FeatureManagement;

[QueryType]
public static class GetFeatureManagementResolver
{
    [AllowAnonymous]
    public static async Task<IDictionary<string, bool>?> GetFlags(IFeatureManagementService featureManagementService)
    {
        return await featureManagementService.GetFeatureManagement();
    }
}
```

## Authentication & Authorization

### OIDC Configuration

1. **Flow**: OpenID Connect (Keycloak or Entra ID)
2. **Token Storage**: Secure, HTTP-only cookies (never localStorage)
3. **JWT Bearer**: Used for API authentication
4. **Refresh**: Automatic token refresh via refresh token grant

**Authorization Pattern**:
```csharp
[Authorize(AuthenticationSchemes = "Bearer")]
public static class ProtectedEndpoints
{
    public static void MapProtected(this WebApplication app)
    {
        var group = app.MapGroup("/api/protected")
            .RequireAuthorization();

        group.MapGet("/", GetProtectedData);
    }

    private static IResult GetProtectedData(ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Results.Ok(new { userId });
    }
}
```

## Code Quality Standards

- **Naming**: Use PascalCase for classes/methods, camelCase for parameters
- **Async/Await**: Use async patterns when appropriate with CancellationToken
- **Null Safety**: Use nullable reference types (enabled in project)
- **Logging**: Use structured logging via ILogger<T>
- **Error Handling**: Use proper HTTP status codes and error responses
- **API Documentation**: Include `[OpenApi]` attributes and XML comments
