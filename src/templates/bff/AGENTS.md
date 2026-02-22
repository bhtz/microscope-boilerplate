# Code guidelines

This file provides guidance when working with code in this repository.

## Available Skills for Coding Agents

This project defines reusable skills that coding agents can execute. Refer to **[`SKILLS.md`](./SKILLS.md)** for the complete list of available skills organized by category:

- **Build & Compilation**: dotnet-build, dotnet-build-service
- **GraphQL**: dotnet-fusion-pack, dotnet-fusion-compose, dotnet-export-graphql-schema
- **Running & Orchestration**: dotnet-aspire-run, dotnet-run-api, dotnet-run-bff
- **Project Management**: dotnet-create-feature, dotnet-create-module
- **Infrastructure**: docker-compose-up, docker-compose-down
- **Configuration**: dotnet-set-user-secret
- **Deployment**: dotnet-build-docker, dotnet-build-arm64

Each skill includes the exact command, purpose, parameters, and expected outputs. Use these skills when automating development tasks.

---

## Project Overview

Microscope boilerplates "BFF" template for BFF / Blazor frontend applications.

## Architecture

This project follows the **Backend for Frontend (BFF) pattern**, serving as a dedicated backend for the Blazor frontend applications. It acts as a security, orchestration, and translation layer between the client and various backend services.

The BFF project is responsible for:
- **Authentication & Security**: Managing OIDC flows (Keycloak, Entra ID) and maintaining secure, HTTP-only cookie-based sessions.
- **API Gateway & Federation**: Composing multiple GraphQL subgraphs into a single schema using **HotChocolate Fusion**.
- **Reverse Proxy**: Routing requests to downstream services (like Data API Builder) via **YARP**.
- **Server-Side Rendering (SSR)**: Supporting Blazor's Interactive Auto/Server modes.

**Key Principle**: **Security by Isolation**. The BFF is the only component that handles sensitive tokens (Access/Refresh tokens), keeping them securely in the backend and shielding the frontend from direct token exposure. It also promotes **Vertical Slices**, where features are implemented as self-contained modules across the BFF and UI.

### Project Structure

```
src/
├── Clients/             # Frontend applications
│   ├── BFF/             # Backend for Frontend (orchestration layer)
│   ├── Web.Blazor/      # Blazor WebAssembly UI
│   └── SDK/             # Generated GraphQL clients
├── CrossCutting/        # Shared concerns (ServiceDefaults)
├── IAC/                 # Infrastructure as Code
│   └── Aspire/          # .NET Aspire orchestration
```

### Technology Stack

- **Architecture Pattern**: BFF (Backend for Frontend)
- **Backend for Frontend**: .NET 9, ASP.NET Core
- **Frontend**: Blazor Server / Auto with MudBlazor (Material Design) or FluentUI
- **Data Access**: Marten (PostgreSQL document DB)
- **API Protocols**:
    - REST (Carter for minimal APIs)
    - GraphQL (HotChocolate with Fusion Gateway)
- **Authentication**: Keycloak or Entra ID (OpenID Connect/OAuth2)
- **Reverse Proxy**: YARP (Yet Another Reverse Proxy)
- **Orchestration**: .NET Aspire

### BFF Architecture

The project uses a BFF (Backend for Frontend) pattern:
- **BFF** handles authentication, SSR, GraphQL gateway federation, and frontend-specific concerns.
- **Security**: Implements the "BFF Pattern" for security, where tokens are stored in secure, encrypted cookies, never reaching the browser's local storage.
- **API Gateway**: Uses HotChocolate Fusion to merge multiple GraphQL subgraphs (e.g., Data API Builder, local resolvers).
- **Reverse Proxy**: Uses YARP to forward requests to backend services based on path-based routing.
- **Backend**: In this template, many backend capabilities are provided by Data API Builder (DAB) acting as a Data-as-a-Service layer.

## Development Commands

### Running the Application

**Using .NET Aspire (recommended for full stack development):**
```bash
cd src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
dotnet run
```

This starts all services with dependencies:
- PostgreSQL (port 5432)
- Keycloak (port 8083)
- Data API Builder (port 4700)
- BFF service
- Blazor Web application (Fluent or Material)

**Running individual services:**
```bash
# Run BFF only
cd src/Clients/Microscope.Boilerplate.Clients.BFF
dotnet run
```

### Building

**Build entire solution:**
```bash
dotnet build Microscope.Boilerplate.sln
```

**Build Docker containers:**
```bash
dotnet publish -p:PublishProfile=DefaultContainer
```

**Build for ARM64:**
```bash
dotnet publish -r linux-arm64 -p:PublishProfile=DefaultContainer
```

### GraphQL Schema Management

**Create BaaS (DAB) Fusion subgraph pack:**
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF/SubGraphs/Dab
fusion subgraph pack -c subgraph-config.json -p Dab.fsp
fusion subgraph pack -c subgraph-config.Development.json -p Dab.Development.fsp
```

**Compose Fusion gateway:**
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF
fusion compose -p gateway.fgp -s ./SubGraphs/Dab/Dab.fsp
fusion compose -p gateway.Development.fgp -s ./SubGraphs/Dab/Dab.Development.fsp
```

## Adding New Features

When adding a new feature:
- **BFF Endpoint**: Add a new static class in `src/Clients/Microscope.Boilerplate.Clients.BFF/Endpoints` and register it in `Program.cs`. Use Carter-style mapping.
- **GraphQL Resolver**: Add new resolvers in `src/Clients/Microscope.Boilerplate.Clients.BFF/GraphQL/` using HotChocolate's `[QueryType]`, `[MutationType]`, or `[SubscriptionType]` attributes. They are automatically discovered via `AddBffTypes()`.
- **Shared Service**: For features shared between BFF and Blazor, add interfaces and services in `src/Clients/Microscope.Boilerplate.Clients.Web.Shared/Services`.
- **UI Component**: Add generic new components in `src/Clients/Microscope.Boilerplate.Clients.Web.Shared/Components` or project-specific components in `Web.Blazor.Fluent` / `Web.Blazor.Material`.
- **GraphQL Subgraph**: Update subgraph configurations in `SubGraphs` and recompose the gateway using the `fusion` CLI when adding external subgraphs.

## Infrastructure

### Database
- PostgreSQL 15 with Marten for document storage
- Initialization script: `src/IAC/Docker/Postgres/init.sql`

### Authentication
- Keycloak with custom theme
- Realm configuration: `src/IAC/Docker/Keycloak/realm-export.json`
- JWT Bearer authentication on API
- Cookie-based OIDC on BFF

### Data API Builder (BaaS)
- Configuration: `src/IAC/Docker/Dab/dab-config.json`
- Provides direct database access via GraphQL/REST

## Important Notes

- **Authentication**: Keycloak is the identity provider. For local development, it is pre-configured via the realm export.
- **Port Conflicts**: Ensure ports 5432 (Postgres), 8083 (Keycloak), and 4700 (DAB) are available.
- **Docker**: If not using Aspire, use `docker-compose up` in `src/IAC/Docker`.