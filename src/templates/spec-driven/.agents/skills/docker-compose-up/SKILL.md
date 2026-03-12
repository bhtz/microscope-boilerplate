---
name: docker-compose-up
description: Start development dependency services with `docker-compose` (PostgreSQL, Keycloak, DAB) for local testing.
---

# docker-compose-up

Start Docker containers for dependencies (when not using Aspire).

## Command
```bash
cd src/IAC/Docker
docker-compose up -d
```

## Purpose
Start PostgreSQL (5432), Keycloak (8083), and DAB (4700)
