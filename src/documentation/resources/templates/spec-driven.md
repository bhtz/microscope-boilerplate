# Spec Driven Development 

> Agentic Spec Driven Development with agents, coding rules & skills

### Features

**Supported coding agents**
* ✅ Codex
* ✅ Claude code
* ✅ Github Copilot
* ⚠️ Cursor
* ⚠️ Windsurf
* ⚠️ Junie

**Custom agents**
* ⚠️ Product manager
* ⚠️ QA engineer
* ⚠️ Software engineer
* ⚠️ Scrum master
* ⚠️ Solution / Principal architect
* ⚠️ Engineering manager
* ⚠️ Head of

**Rules**
* ⚠️ Blazor
* ⚠️ BFF
* ⚠️ CQRS
* ⚠️ DAB
* ⚠️ Hasura
* ⚠️ GraphQL
* ⚠️ REST
* ⚠️ MCP
* ⚠️ Vertical slices
* ⚠️ GRPC

**Skills**
* ⚠️ Aspire
* ⚠️ Dotnet build
* ⚠️ User secrets
* ⚠️ Documentation

### Create new project
```console
dotnet new mcsp_spec-driven -n Acme
```

### Template options

* -A, --Aspire
    * Whether to include aspire or not.
    * Type : bool
    * Default : false

* -T, --Template <bff|dab|...> 
    * Select the template variant to generate coding skills and rules for.
    * Type : choice
        * bff - Backend-for-Frontend (BFF) / gateway + Blazor
        * dab - Data API Builder - Backend As a Service
        * docs - Documentation site
        * service - API Service / modular monolith oriented
        * scheduler - Scheduler / background jobs
        * cli - Command-line tool / CLI
        * desktop - Desktop application (Avalonia)
