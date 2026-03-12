---
apply: always
title: User Secrets & Configuration Management
category: Development Workflow
name: user-secrets-config
description: Manage `dotnet user-secrets` and environment variable overrides for local development and CI, including API keys and DB connection strings.
---

# User Secrets & Configuration Management

Gestion sécurisée des secrets et configuration sensible en développement local avec `dotnet user-secrets`.

## Overview

This skill helps developers securely manage sensitive configuration (API keys, connection strings, third‑party credentials) during local development using `dotnet user-secrets`, plus guidance for environment variable overrides and CI/CD integration.

## When to Use This Skill

- Use when storing secrets for local development
- Use to configure API keys (OpenAI, MCP), DB connection strings, and auth secrets
- Use when preparing CI/CD to inject secrets safely
- Use to validate configuration binding and startup validation

## How It Works

### Step 1: Initialize User Secrets for a Project

```bash
cd src/templates/modular-monolith
dotnet user-secrets init
```

### Step 2: Add Secrets

```bash
dotnet user-secrets set "AI:OpenAI:ApiKey" "sk-dev-xxxxx"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Port=5432;Database=mcsp_app;User Id=postgres;Password=devpassword"
```

### Step 3: Access in Code

```csharp
builder.Configuration.AddUserSecrets<Program>();
var key = builder.Configuration["AI:OpenAI:ApiKey"];
services.Configure<OpenAiOptions>(builder.Configuration.GetSection("AI:OpenAI"));
```

### Step 4: Override with Environment Variables

Environment variables take precedence and are used for CI and containerized runs. Use `AI__OpenAI__ApiKey` style for nested keys.

```bash
export AI__OpenAI__ApiKey="sk-ci-override"
```

## Examples

### Add OpenAI Key

```bash
dotnet user-secrets set "AI:OpenAI:ApiKey" "sk-xxxxxxxx"
dotnet user-secrets list
```

### Use in Code with Options Pattern

```csharp
public class OpenAiOptions { public string? ApiKey { get; set; } public string? Model { get; set; } }
services.Configure<OpenAiOptions>(configuration.GetSection("AI:OpenAI"));
```

## Best Practices

- Never commit secrets to source control.
- Prefer user-secrets for local development and environment variables for CI/containers.
- Validate options on startup (`ValidateOnStart`).
- Rotate keys regularly and automate rotation where possible.

## Common Pitfalls

- Forgetting to call `AddUserSecrets<T>()` in development.
- Assuming user-secrets are available in CI — they are local to developer machines.
- Hardcoding secrets in code or config files.

## Checklist

- [ ] `dotnet user-secrets init` executed for project
- [ ] All dev secrets set (`AI`, `ConnectionStrings`, `Auth`)
- [ ] Secrets not committed to git
- [ ] Startup validation for critical secrets
- [ ] CI/CD secrets configured in pipeline

## Tools

- `dotnet user-secrets` CLI
- `env` / shell for environment variables
- GitHub Actions / Azure Pipelines secrets store

## Related Skills

- `local-development` - Bootstrapping local env and services
- `ef-core-migrations` - Migrations often need DB connection strings
- `api-testing` - Uses secrets for authenticated requests

## Additional Resources

- https://learn.microsoft.com/aspnet/core/security/app-secrets
- https://learn.microsoft.com/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0

---

**Pro Tip:** Use the Options pattern with `ValidateOnStart()` to fail fast if a critical secret is missing.
