# Microscope.Boilerplate Modular Monolith Service

> Microscope.Boilerplate Modular Monolith service template with CQRS, vertical slices, and multi-protocol support

## Getting Started

### Agent Rules & Skills

This template includes comprehensive agent rules and development skills:

- **📖 Rules**: See [AGENTS.md](./AGENTS.md) - Complete architecture and development rules
- **🛠️ Skills**: See [.agents/skills/SKILLS.md](./.agents/skills/SKILLS.md) - Practical development tasks
- **🔗 Symlinks**: See [SYMLINKS.md](./SYMLINKS.md) - Setup access from `.claude/` and `.github/`
- **🔄 Sync Scripts**: See [SPEC-DRIVEN-QUICKSTART.md](./SPEC-DRIVEN-QUICKSTART.md) - Synchronize rules across tools

### Sync Agent Rules Across Tools

Keep rules synchronized between `.agents/` and tool-specific directories (`.github/`, `.claude/`, `.codex/`):

**Windows (Batch):**
```cmd
spec-driven.bat
```

**macOS/Linux:**
```bash
./spec-driven.sh
```

This makes rules & skills accessible via:
- `.claude/` - For Claude AI assistant
- `.github/` - For GitHub Copilot
- `.codex/` - For Codex

---

## Roadmap : 

- [x] Docker setup
- [x] Refactoring of modularity
  - [x] Rest
  - [x] GraphQL
  - [x] CQRS
- [x] Refactoring of framework
  - [x] isolate domain framework
  - [x] isolate application framework
- [x] Implement basic persistence
  - [x] EFcore
    - [x] Repository
    - [x] Configurations
    - [x] Migrations
  - [x] Marten
    - [x] Repository
- [x] Marten & EfCore unit of work
- [x] Todo slices
  - [x] Command & Query & Handlers
  - [x] GraphQL operations
  - [x] REST operations
  - [x] cleanup AllowAnonymous
- [x] Grpc support
  - [x] Grpc slices rpcService
  - [x] Grpc server
- [x] Template protocols options
  - [x] GraphQL
  - [x] REST
  - [x] GRPC
  - [x] MCP
    - [x] Setup ChatClient & MCP
    - [x] Configure list of remote MCP
  - [x] Tool CLI options
- [x] Feature management
- [x] Aspire docker composer publisher
- [x] Domain event handler
- [x] Refactoring IRepository & Aggregate framework
- [x] Clean AuditableAggregate
- [x] Todo domain unit test
  - [x] Sample Unit test
  - [x] Add template option
- [x] .NET 10
- [x] .slnx
- [x] Directory props & build
- [x] Clean keycloak clients


## Helpers EF CORE

Navigate to module infra layer :

Generate migrations :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ migrations add InitialCreate -o ./Persistence/EFcore/Migrations

Update database :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ database update

Generate SQL script :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ migrations script > ./Persistence/EFcore/Scripts/Todo.SQL

Generate AI secret key

    cd src/Services/Microscope.Management.API
    dotnet user-secrets init
    dotnet user-secrets set "AI:Key" "{value}"

Rsync .agents/** to specific coding agents convention : 

    rsync -a --delete .agents/ .github/
    rsync -a --delete .agents/ .claude/