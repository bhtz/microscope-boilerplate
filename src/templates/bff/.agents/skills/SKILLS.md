# Coding Agent Skills

This document defines common skills and tasks that coding agents can execute within the Microscope.Boilerplate BFF template.

## Build & Compilation Skills

### dotnet-build
Build the entire .NET solution.
```bash
dotnet build Microscope.Boilerplate.sln
```
**Purpose**: Compile all projects, verify no syntax errors
**Output**: Success/failure with error details

### dotnet-build-bff
Build the BFF service project.
```bash
dotnet build src/Clients/Microscope.Boilerplate.Clients.BFF/Microscope.Boilerplate.Clients.BFF.csproj
```
**Purpose**: Compile the BFF service only
**Output**: Success/failure with error details

## GraphQL Skills

### fusion-subgraph-pack
Create GraphQL Fusion subgraph pack for DAB (Data API Builder).
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF/SubGraphs/Dab
fusion subgraph pack -c subgraph-config.json -p Dab.fsp
```
**Purpose**: Generate `.fsp` file for GraphQL Fusion composition
**Output**: `Dab.fsp` file created

### fusion-subgraph-pack-development
Create GraphQL Fusion subgraph pack for development.
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF/SubGraphs/Dab
fusion subgraph pack -c subgraph-config.Development.json -p Dab.Development.fsp
```
**Purpose**: Generate development `.fsp` file for local testing
**Output**: `Dab.Development.fsp` file created

### fusion-compose
Compose GraphQL Fusion gateway schema from subgraphs.
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF
fusion compose -p gateway.fgp -s ./SubGraphs/Dab/Dab.fsp
```
**Purpose**: Create unified gateway schema from subgraph packs
**Output**: `gateway.fgp` file created

### fusion-compose-development
Compose GraphQL Fusion gateway schema for development.
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF
fusion compose -p gateway.Development.fgp -s ./SubGraphs/Dab/Dab.Development.fsp
```
**Purpose**: Create development gateway schema
**Output**: `gateway.Development.fgp` file created

## Running & Orchestration

### dotnet-aspire-run
Run entire application stack using .NET Aspire.
```bash
cd src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
dotnet run
```
**Purpose**: Start services with dependencies (PostgreSQL, Keycloak, DAB, BFF, Blazor app)
**Dashboard**: Printed in console (e.g., `http://localhost:15214`)

### dotnet-aspire-project
Run .NET Aspire AppHost project.
```bash
dotnet run --project src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire/Microscope.Boilerplate.IAC.Aspire.csproj
```
**Purpose**: Start Aspire orchestration with all configured services
**Output**: Dashboard URL and service endpoints in console

### dotnet-run-bff
Run BFF service only.
```bash
cd src/Clients/Microscope.Boilerplate.Clients.BFF
dotnet run
```
**Purpose**: Start Backend for Frontend service

## Database & Infrastructure

### docker-compose-up
Start Docker containers for dependencies (when not using Aspire).
```bash
cd src/IAC/Docker
docker-compose up -d
```
**Purpose**: Start PostgreSQL (5432), Keycloak (8083), and DAB (4700)

### docker-compose-down
Stop Docker containers.
```bash
cd src/IAC/Docker
docker-compose down
```
**Purpose**: Stop all running containers

## Validation & Deployment

### dotnet-build-docker
Build Docker containers for deployment.
```bash
dotnet publish -p:PublishProfile=DefaultContainer
```
**Purpose**: Create Docker images for services
**Output**: Docker images ready for deployment

### dotnet-build-arm64
Build for ARM64 architecture (e.g., Apple Silicon).
```bash
dotnet publish -r linux-arm64 -p:PublishProfile=DefaultContainer
```
**Purpose**: Create ARM64-compatible containers
**Output**: ARM64 Docker images

## Skill Categories

| Category           | Skills                                                                                             |
|--------------------|----------------------------------------------------------------------------------------------------|
| **Build**          | dotnet-build, dotnet-build-bff                                                                     |
| **GraphQL**        | fusion-subgraph-pack, fusion-subgraph-pack-development, fusion-compose, fusion-compose-development |
| **Running**        | dotnet-aspire-run, dotnet-aspire-project, dotnet-run-bff                                           |
| **Infrastructure** | docker-compose-up, docker-compose-down                                                             |
| **Deployment**     | dotnet-build-docker, dotnet-build-arm64                                                            |
