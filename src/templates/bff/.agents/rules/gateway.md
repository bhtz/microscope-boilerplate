---
apply: always
---

# BFF (Backend for Frontend) Coding Rules

Spec-driven development rules for BFF endpoints, GraphQL resolvers, and authentication flows.

## Architecture Principles

- **Security by Isolation**: The BFF is the only component handling sensitive tokens (Access/Refresh tokens)
- **Vertical Slices**: Features implemented as self-contained modules across BFF and UI
- **Gateway Federation**: Multiple GraphQL subgraphs merged via HotChocolate Fusion
- **Reverse Proxy**: YARP for routing to downstream services

## Endpoint Development

### REST Endpoints (Carter)

When creating new REST endpoints:

1. **Location**: Create endpoints in `src/Clients/Microscope.Boilerplate.Clients.BFF/Endpoints/{FeatureName}/`
2. **Naming**: Use descriptive, feature-based names (e.g., `LeadsEndpoints.cs`)
3. **Structure**: Use Carter's minimal API pattern with static extension methods
4. **Registration**: Register in `Program.cs` via `MapGroup()` or `MapCarter()`

**Example Pattern**:
```csharp
namespace Microscope.Boilerplate.Clients.BFF.Endpoints.Leads;

public static class LeadsEndpoints
{
    public static void MapLeadsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/leads")
            .WithName("Leads")
            .WithOpenApi();

        group.MapGet("/", GetLeads).WithName("GetLeads").WithOpenApi();
        group.MapPost("/", CreateLead).WithName("CreateLead").WithOpenApi();
    }

    private static async Task<IResult> GetLeads(IMediator mediator)
    {
        var query = new GetLeadsQuery();
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private static async Task<IResult> CreateLead(CreateLeadCommand command, IMediator mediator)
    {
        var result = await mediator.Send(command);
        return Results.Created($"/api/leads/{result.Id}", result);
    }
}
```

## GraphQL Development

### GraphQL Resolvers (HotChocolate)

When creating new GraphQL resolvers:

1. **Location**: Create resolvers in `src/Clients/Microscope.Boilerplate.Clients.BFF/GraphQL/{FeatureName}/`
2. **Naming**: Use domain-driven naming (e.g., `LeadsQuery.cs`, `LeadsMutation.cs`)
3. **Auto-Discovery**: Use `[QueryType]`, `[MutationType]`, `[SubscriptionType]` attributes
4. **Registration**: Automatically discovered via `AddBffTypes()` in `Program.cs`

**Query Example**:
```csharp
namespace Microscope.Boilerplate.Clients.BFF.GraphQL.Leads;

[QueryType]
public static class LeadsQuery
{
    [GraphQLName("leads")]
    public static async Task<IEnumerable<Lead>> GetLeads(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetLeadsQuery();
        return await mediator.Send(query, cancellationToken);
    }

    [GraphQLName("lead")]
    public static async Task<Lead?> GetLead(
        long id,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetLeadByIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }
}
```

**Mutation Example**:
```csharp
[MutationType]
public static class LeadsMutation
{
    [GraphQLName("createLead")]
    public static async Task<Lead> CreateLead(
        CreateLeadInput input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CreateLeadCommand(input.FirstName, input.LastName, input.Email, input.Phone);
        return await mediator.Send(command, cancellationToken);
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
            .RequireAuthorization("Bearer");

        group.MapGet("/", GetProtectedData);
    }

    private static async Task<IResult> GetProtectedData(ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Results.Ok(new { userId });
    }
}
```

## Data API Builder Integration

### YARP Reverse Proxy Routes

1. **Configuration**: Update `Program.cs` with YARP configurations
2. **Pattern**: Path-based routing to Data API Builder endpoints
3. **Purpose**: Forward requests while maintaining security

**Example**:
```csharp
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// In appsettings.json:
{
  "ReverseProxy": {
    "Routes": {
      "dab-route": {
        "ClusterId": "dab-cluster",
        "Match": { "Path": "/api/data/{**catch-all}" },
        "Transforms": [
          { "PathRemovePrefix": "/api/data" }
        ]
      }
    },
    "Clusters": {
      "dab-cluster": {
        "Destinations": {
          "dab": { "Address": "http://localhost:4700" }
        }
      }
    }
  }
}
```

## MediatR Commands & Queries

### Command Pattern

```csharp
// Command Definition
public record CreateLeadCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Phone = null) : IRequest<Lead>;

// Handler
public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, Lead>
{
    private readonly IMartenService _martenService;

    public async Task<Lead> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = new Lead
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            CreatedAt = DateTime.UtcNow
        };

        await _martenService.SaveAsync(lead, cancellationToken);
        return lead;
    }
}
```

## Marten Document Storage

1. **Database**: PostgreSQL 15 with Marten for document storage
2. **Models**: Use domain entities as document types
3. **Queries**: Use Marten's LINQ support for querying
4. **Transactions**: Use `IDocumentSession` for transactional operations

## Code Quality Standards

- **Naming**: Use PascalCase for classes/methods, camelCase for parameters
- **Async/Await**: Always use async patterns with CancellationToken
- **Null Safety**: Use nullable reference types (enabled in project)
- **Logging**: Use structured logging via ILogger<T>
- **Error Handling**: Use Result pattern or exceptions with proper HTTP status codes
- **API Documentation**: Include [OpenApi] attributes and XML comments

## Testing

- **Unit Tests**: Test handlers and services in isolation
- **Integration Tests**: Test endpoints with test containers
- **GraphQL Tests**: Use HotChocolate testing utilities
- **Location**: Place tests in parallel project structure with `.Tests` suffix
