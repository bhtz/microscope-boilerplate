---
apply: always
---

# Code guidelines (Scheduler template)

Guidance for background job and scheduler templates that run periodic or long-running tasks.

### Technology Stack

- **Scheduling**: TickerQ
- **Persistence**: PostgreSQL

### Architecture

- Scheduler coordinates jobs which execute domain logic; prefer idempotent jobs
- Use retry/backoff and dead-lettering for transient failures

### Project Structure

```
Scheduler.Host/                      
├── Jobs/                            # Job handlers
├── Data/                            # Persistence data (EF core)
└── Program.cs
```

## Available Skills for Coding Agents (Scheduler)

- `dotnet-build`, `dotnet-run` for local runs
- `docker-compose` to start dependent services like Redis or PostgreSQL

## Adding New Features

1. Create a Job handler under `Jobs/` and back it with idempotent logic
2. Register the job in the host startup with schedule configuration
3. Add monitoring/metrics for job success/failure

## Infrastructure

- Use `docker-compose` or your orchestrator to run the scheduler and its backing services (Postgres, Redis).
- Expose observability via OpenTelemetry and provide health endpoints for monitoring and automated restarts.

````
