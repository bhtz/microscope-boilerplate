---
apply: always
---

# Code guidelines (BFF / Frontend variant)

This file provides guidance when working with the BFF (Backend-for-Frontend) template: a gateway that composes UI and backend subgraphs, hosts gateway resolvers, and serves front-end assets.

### Technology Stack

- **Architecture Pattern**: Backend-for-Frontend (BFF) / UI composition layer
- **BFF Host**: .NET 10.0, ASP.NET Core (minimal API)
- **Frontend**: Blazor (Fluent UI) and optional React static app
- **Gateway Protocols**:
    - GraphQL gateway (HotChocolate) for composing subgraphs and exposing a unified schema
    - REST proxy endpoints for simple browser requests
    - gRPC-Web for streaming scenarios to browsers
    - MCP (Model Context Protocol) to expose agent tools from the BFF
- **Composition/Tooling**:
    - Fusion / subgraph packs (.fsp / .fgp) for UI and GraphQL composition
    - Docker Compose / Aspire for local orchestration
- **Validation**: FluentValidation
- **Testing**: Playwright (E2E UI), xUnit for BFF integration

### Architecture (BFF)

```
Browser Clients  -->  BFF Gateway (GraphQL + REST + Static Assets)
                          ├─ UI Hosting (Blazor/Static)
                          ├─ GraphQL Composition (HotChocolate + Fusion)
                          └─ Proxy to Microservices / Subgraphs

Subgraphs / Services -->  GraphQL Subgraphs / REST APIs
```

The BFF primarily composes user-facing APIs and performs cross-cutting tasks such as authentication forwarding, response shaping, and schema stitching.

### Project Structure (BFF)

```
Clients/BFF/                       # BFF gateway & UI composition
├── Components/                     # Shared Blazor components (if hosted)
├── GraphQL/                        # Gateway schema, resolvers, composition config
├── SubGraphs/                      # Subgraph packs (.fsp / .fgp) used by Fusion
├── Static/                         # Optional static front-end assets
└── Program.cs                      # BFF startup and DI configuration

Clients/Web/                        # Optional separate front-end projects
├── Blazor/                          # Blazor client projects (Fluent UI)
└── React/                           # Static React app (optional)

IAC/                                # Infrastructure for local dev (Docker / Aspire)
```

### Available Skills for Coding Agents (BFF)

This template provides BFF-specific skills in **[`./.agents/skills/`](./.agents/skills/)**. Examples include:

- `fusion-compose`: compose UI/fusion artifacts
- `fusion-subgraphs-pack`: pack and publish subgraph artifacts
- `dotnet-run-bff`: run the BFF host locally (Aspire / dotnet run)
- `dotnet-build-bff`: build the BFF host
- Additional generic skills (docker-compose-up, docker-compose-down, user-secrets-config)

Agents should prefer using BFF skills to perform local composition tasks and to run the development host.

## Adding New Features (BFF)

When adding a new UI or gateway feature follow the same high-level steps as backend modules but focused on composition and UI exposure.

### 1. Create Feature Structure
```
Clients/BFF/MyFeature/
├── Components/                     # Blazor components or view fragments
├── GraphQL/
│   ├── Resolvers/                  # Gateway resolvers that map to subgraph fields
│   └── SchemaExtensions.graphql    # Gateway schema augmentations
├── SubGraphs/                      # Local subgraph packs to compose into gateway
└── Extensions.cs                   # DI registration for the feature
```

### 2. Implement Composition Slice
- **UI Components**: Add Blazor components and register them in the shared layout
- **Gateway Resolvers**: Provide resolvers that delegate to subgraphs or shape responses
- **Fusion Packs**: Add `.fsp`/`.fgp` entries for the feature when it needs composition
- **MCP Exposure**: Optionally add MCP tools to surface feature operations to agents

### 3. Register in BFF Host
```csharp
// In Clients/BFF/Program.cs
services
    .AddFeatureMyFeature()
    .AddGraphQLGateway()   // configure fusion/subgraph composition
    .AddAspireDefaults();
```

## Infrastructure (BFF)

### Local Orchestration
- Use `docker-compose` and `aspire` for starting dependent services (subgraphs, mock services)
- `docker-compose` typically defines a lightweight Postgres for local development if needed and the subgraph mock services

### Composition Artifacts
- Keep subgraph packs under `Clients/BFF/SubGraphs/` as `.fsp` or `.fgp` files
- CI pipelines should validate composition by running `fusion-compose` and `fusion-subgraphs-pack` skills

### Authentication & Security
- BFF forwards JWT tokens to subgraphs and enforces presentation-layer policies
- Use smaller, feature-focused policies in the gateway and push domain authorization to subgraphs

### AI & Agent Capabilities
- The BFF can expose MCP tools for UI-centric operations (e.g., UI scaffolding, localized text updates)
- Agents may call `dotnet-run-bff` and `fusion-compose` skills during dev flows

````

