---
name: dotnet-aspire-run
description: Launch the .NET Aspire orchestrator to start the full local application stack and its dependencies for development.
---

# dotnet-aspire-run

Run entire application stack using .NET Aspire.

## Command
```bash
cd src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
dotnet run
```

## Purpose
Start services with dependencies (PostgreSQL, Keycloak, DAB, BFF, Blazor app)

## Output
Dashboard URL printed in console (e.g., `https://localhost:17068`)
