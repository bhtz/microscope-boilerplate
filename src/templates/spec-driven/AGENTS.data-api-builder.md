---
apply: always
---

# Code guidelines (Data API Builder template)

This file documents guidance for the Data API Builder template: lightweight data-backed REST/GraphQL APIs optimized for rapid prototyping and serverless hosting.

### Technology Stack

- **Backend**: Azure Data API builder - Backend As a Service
- **API Protocols**: REST, GraphQL
- **Database**: PostgreSQL

### Architecture

- Simple request → repository → store flow
- Favor small, single-purpose endpoints and idempotent operations
- Optional GraphQL facade for aggregate reads

### Project Structure

```
Project/                            
├── dab                         # dab configurations
├── postgres                    # postgres configuration & init files
```

## Available Skills for Coding Agents (Data API Builder)

- `dotnet-build`, `dotnet-run` for local runs
- `docker-compose-up` / `docker-compose-down` for dependencies
- migration and seed skills for DB setup

## Adding New Features

1. Add a vertical slice under `Features/MyFeature` with request/handler/endpoint and DTOs.
2. Add database migrations and seed data under `Infrastructure` when the schema changes.
3. Add integration tests that exercise the real DB provider via `docker-compose` in CI.

## Infrastructure

- Use `docker-compose` for local development databases and provide init scripts under `src/IAC/Docker`.
- Support multiple persistence targets (Postgres, Cosmos) via configuration and feature flags.

````
