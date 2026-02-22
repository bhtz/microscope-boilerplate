# Coding Agent Skills

This document indexes all available skills for coding agents working with the Microscope.Boilerplate BFF template. Each skill is documented in a separate file for modularity and reusability.

## Build & Compilation Skills

- [dotnet-build](./dotnet-build.md) - Build the entire .NET solution
- [dotnet-build-bff](./dotnet-build-bff.md) - Build the BFF service project

## GraphQL Skills

- [dab-fusion-subgraph-pack](./dab-fusion-subgraph-pack.md) - Create GraphQL Fusion subgraph pack for DAB (Data Api builder - backend as a service)
- [dab-fusion-subgraph-pack-development](./dab-fusion-subgraph-pack-development.md) - Create development subgraph pack for DAB (Data Api builder - backend as a service)
- [fusion-compose](./fusion-compose.md) - Compose GraphQL Fusion gateway schema
- [fusion-compose-development](./fusion-compose-development.md) - Compose development gateway schema

## Running & Orchestration

- [dotnet-aspire-run](./dotnet-aspire-run.md) - Run entire application stack using .NET Aspire
- [dotnet-aspire-project](./dotnet-aspire-project.md) - Run .NET Aspire AppHost project
- [dotnet-run-bff](./dotnet-run-bff.md) - Run BFF service only

## Database & Infrastructure

- [docker-compose-up](./docker-compose-up.md) - Start Docker containers for dependencies
- [docker-compose-down](./docker-compose-down.md) - Stop Docker containers

## Validation & Deployment

- [dotnet-build-docker](./dotnet-build-docker.md) - Build Docker containers for deployment
- [dotnet-build-arm64](./dotnet-build-arm64.md) - Build ARM64 Docker containers

## Skill Categories

| Category           | Skills                                                                                             |
|--------------------|----------------------------------------------------------------------------------------------------|
| **Build**          | dotnet-build, dotnet-build-bff                                                                     |
| **GraphQL**        | fusion-subgraph-pack, fusion-subgraph-pack-development, fusion-compose, fusion-compose-development |
| **Running**        | dotnet-aspire-run, dotnet-aspire-project, dotnet-run-bff                                           |
| **Infrastructure** | docker-compose-up, docker-compose-down                                                             |
| **Deployment**     | dotnet-build-docker, dotnet-build-arm64                                                            |
