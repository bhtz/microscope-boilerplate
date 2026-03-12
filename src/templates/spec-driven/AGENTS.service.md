---
apply: always
---

# Code guidelines (Service / Backend variant)

This file provides guidance when working with code in this repository (Service / Backend template).

### Technology Stack

- **Architecture Pattern**: Modular Monolith with Vertical Slices
- **Backend**: .NET 10.0, ASP.NET Core
- **API Protocols**:
    - REST (Carter for minimal APIs) with API Versioning
    - GraphQL (HotChocolate with module federation)
    - gRPC (Protocol Buffers with streaming support)
    - MCP (Model Context Protocol) for AI tools
- **Application Pattern**: CQRS (MediatR) + Vertical Slices
- **Data Persistence**:
    - Marten (Document Store) - Primary
    - Entity Framework Core - Alternative
    - PostgreSQL 15 - Database
- **AI & Agents**: OpenAI GPT-4o, MCP tools, Remote MCP clients
- **Validation**: FluentValidation
- **Orchestration**: .NET Aspire
- **Testing**: xUnit, Fluent Assertions

### Modular Monolith Architecture

```
┌─────────────────────────────────┐
│   API HOST / Program.cs         │
│  ├─ REST (v1.0 - Carter)        │
│  ├─ GraphQL (HotChocolate)      │
│  ├─ gRPC                        │
│  └─ MCP Server (Tools)          │
└──────────────┬──────────────────┘
               │
        ┌──────┴──────┐
        │             │
┌───────▼────────┐ ┌──▼──────────────┐
│  Framework     │ │    Modules      │
│  ├─ App        │ │  ├─ Todo        │
│  ├─ Domain     │ │  ├─ Ai         │
│  └─ Infra      │ │  └─ ...        │
└────────────────┘ └─────────────────┘
        │                 │
        └────────┬────────┘
                 │
         ┌───────▼────────┐
         │  PostgreSQL    │
         │  + Marten/EF   │
         └────────────────┘
```

### Project Structure

```
Framework/                          # Shared abstractions & patterns
├── Application/                     # CQRS, MediatR, validators
├── Domain/                          # Value objects, entities, events
└── Infrastructure/                  # Persistence options, base services

Modules/                            # Feature modules (vertical slices)
├── Todo/
│   ├── Domain/                     # Business entities & rules
│   ├── Infrastructure/             # Data access & external services
│   └── Slices/                     # REST, GraphQL, MCP endpoints
├── Ai/
│   ├── Infrastructure/             # OpenAI, MCP services
│   └── Slices/                     # QnA, chat features
└── [OtherModules]/

IAC/                                # Infrastructure as Code
├── Aspire/                         # Orchestration & service discovery
└── Docker/                         # Compose files, PostgreSQL init

Microscope.Boilerplate.API/         # Application host
├── Extensions/                     # MCP, GraphQL, REST setup
└── Program.cs                      # Dependency injection & startup
```

Each module implements three layers:
- **Domain**: Business entities, aggregates, rules
- **Infrastructure**: Persistence (Marten/EF), external services
- **Slices**: REST endpoints, GraphQL resolvers, MCP tools

## Available Skills for Coding Agents (Backend)

This project defines reusable skills that coding agents can execute. Skills are organized by development workflow in **[`./.agents/skills/`](./.agents/skills/)** directory.

## Adding New Features

When adding a new feature in a module:

### 1. Create Module Structure
```
Modules/MyModule/
├── Domain/                         # Business logic
│   ├── MyAggregate.cs
│   └── MyAggregateEvents.cs
├── Infrastructure/
│   ├── Extensions.cs              # DI setup
│   └── Services/
├── Slices/
│   ├── Features/MyFeature/
│   │   ├── CreateMyFeature/
│   │   │   ├── CreateMyFeatureCommand.cs
│   │   │   ├── CreateMyFeatureHandler.cs
│   │   │   ├── CreateMyFeatureEndpoint.cs (REST)
│   │   │   └── CreateMyFeatureResolver.cs (GraphQL)
│   │   │   └── CreateMyFeatureRpcService.cs (gRPC)
│   │   │   └── CreateMyFeatureMcpTool.cs (MCP)
│   │   └── GetMyFeatures/
│   ├── Extensions.cs              # Module registration
│   └── IMyFeatureModule.cs         # Module marker interface
```

### 2. Implement Vertical Slice
- **Command**: MediatR request
- **Handler**: Business logic with persistence
- **REST Endpoint**: Carter-based minimal API
- **GraphQL Resolver**: HotChocolate query/mutation
- **gRPC Service**: gRPC operation
- **MCP Tool**: Optional for AI agent exposure

### 3. Register in API Host
```csharp
// In Microscope.Boilerplate.API/Program.cs
services
    .AddModuleApplication()
    .AddModuleInfrastructure();
```

## Infrastructure

### Database
- **Primary**: PostgreSQL 15 with Marten for document storage
- **Alternative**: Entity Framework Core with SQL databases
- **Initialization**: `src/IAC/Docker/Postgres/init.sql`
- **Per-Module Persistence**: Each module can choose Marten or EF Core via `PersistenceOptions`

### Authentication & Security
- JWT Bearer tokens on API endpoints
- Authorization policies with role/claim checks
- FluentValidation for input validation

### AI & Agent Capabilities
- **OpenAI Integration**: GPT-4o model for chat & reasoning
- **MCP Server**: Exposes module tools as MCP resources
- **Remote MCP**: Connects to external MCP servers for extended capabilities
- **Tool Discovery**: Automatic registration from module attributes

### Orchestration
- **Aspire AppHost**: Service discovery, port management
- **Docker Compose**: Development environment setup
- **Service Defaults**: Shared OpenTelemetry & health checks configuration
