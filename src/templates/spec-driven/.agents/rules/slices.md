---
apply: always
---

# Vertical Slices Pattern Coding Rules

Spec-driven development rules for building vertical slices using multiple protocols (REST, GraphQL, gRPC, MCP) integrated with CQRS pattern.

## Architecture Principles

- **Vertical Slices**: Each feature/use-case is completely self-contained
- **Single Responsibility**: One slice handles one business capability
- **Protocol Agnostic CQRS**: Use same Query/Command for all protocols
- **Feature Ownership**: All protocol handlers live together in same feature folder
- **Multi-Protocol Support**: Single CQRS handler exposed via REST, GraphQL, gRPC, and MCP simultaneously
- **Lazy Initialization**: Only enable protocols you need via feature flags or configuration

## Directory Structure

A complete vertical slice contains handlers for all supported protocols:

```
Modules/MyModule/
├── Microscope.Boilerplate.MyModule.Slices/
│   ├── Features/
│   │   ├── CreateMyResource/                          # Use Case: Create
│   │   │   ├── CreateMyResourceCommand.cs             # CQRS: Command definition
│   │   │   ├── CreateMyResourceValidator.cs           # Validation (optional)
│   │   │   ├── CreateMyResourceHandler.cs             # CQRS: Command handler
│   │   │   ├── CreateMyResourceEndpoints.cs           # Protocol: REST (Carter)
│   │   │   ├── CreateMyResourceResolver.cs            # Protocol: GraphQL (HotChocolate)
│   │   │   ├── CreateMyResourceRpcService.cs          # Protocol: gRPC
│   │   │   └── CreateMyResourceMcpTool.cs             # Protocol: MCP (AI tools)
│   │   ├── GetMyResources/                            # Use Case: Query
│   │   │   ├── GetMyResourcesQuery.cs                 # CQRS: Query definition
│   │   │   ├── GetMyResourcesHandler.cs               # CQRS: Query handler
│   │   │   ├── GetMyResourcesEndpoints.cs             # Protocol: REST
│   │   │   ├── GetMyResourcesResolver.cs              # Protocol: GraphQL
│   │   │   ├── GetMyResourcesRpcService.cs            # Protocol: gRPC
│   │   │   └── GetMyResourcesMcpTool.cs               # Protocol: MCP
│   │   ├── GetMyResourceById/
│   │   ├── UpdateMyResource/
│   │   └── DeleteMyResource/
│   ├── Protos/
│   │   └── MyResource.proto                           # gRPC definitions (if needed)
│   ├── Extensions.cs                                  # DI & route registration
│   └── ITodoModule.cs                                 # Module marker interface
```

## CQRS Foundation

Every vertical slice is built on a **CQRS command or query** that encapsulates the business logic. For detailed information on creating CQRS commands, queries, handlers, and validators, see:

📖 **[CQRS Pattern Rules](./cqrs.md)** - Complete guide to commands, queries, handlers, and validators

Key points:
- Commands modify state, queries read state (never mix them)
- Each command/query has a corresponding handler
- Validators are defined alongside commands
- Handlers contain the core business logic used by all protocol implementations

## Protocol Implementation Pattern

Each protocol handler in a vertical slice **calls the same CQRS handler** through MediatR:

```csharp
// All protocol handlers use the same pattern:
var result = await mediator.Send(new CreateTodoListCommand("My List"));

// The protocol handler just maps request → command, then command → protocol response
// Business logic stays in one place: the CQRS handler
```

For detailed implementation patterns for each protocol, refer to the protocol-specific rules:

| Protocol | Rule File | Details |
|----------|-----------|---------|
| **REST (Carter)** | [rest.md](./rest.md) | HTTP endpoints, routing, versioning, OpenAPI |
| **GraphQL (HotChocolate)** | [graphql.md](./graphql.md) | Queries, mutations, resolvers, authorization |
| **gRPC (Protocol Buffers)** | [grpc.md](./grpc.md) | Service definitions, streaming, error handling |
| **MCP (Model Context Protocol)** | [mcp.md](./mcp.md) | AI tools, tool discovery, agent integration |

Each protocol rule file contains:
- Architecture principles specific to that protocol
- Complete code examples for command/query handlers
- Integration patterns with MediatR
- Authorization and validation approaches
- Testing strategies
- Best practices and common pitfalls
---

## Best Practices

### Do's ✅
- Keep all protocol handlers for one use-case in the same folder
- Use same CQRS handler for all protocols (DRY principle)
- Separate domain logic (handler) from presentation (protocol handlers)
- Use DTOs for API responses, not domain entities
- Document endpoint/tool purposes with descriptions
- Apply authorization consistently across all protocols
- Use consistent naming: `{UseCase}{Protocol}` (e.g., `CreateTodoListEndpoints`)

### Dont's ❌
- Don't create separate CQRS handlers per protocol
- Don't expose domain entities through any protocol
- Don't duplicate validation logic in protocol handlers
- Don't skip authorization checks in any protocol
- Don't mix business logic into protocol handlers
- Don't create monolithic feature files - keep them focused
- Don't forget to include all protocol files if they're enabled