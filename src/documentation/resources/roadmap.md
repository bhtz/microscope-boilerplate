# Roadmap & issues

## microscope boilerplate templates

### BFF/Frontend

- [x] Add Fluent UI option
- [x] Theme management over cookie
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [x] Clean keycloak export
- [x] Remove BaaS
- [x] Go to aspire & clean docker-compose
- [ ] Fluent theme selector
- [ ] I18N fluent UI
- [ ] E2E test (Playwright / bUnit)
- [ ] SignalR / Subscription sample

### Service
- [x] Add unit tests (xUnit)
- [x] Isolated marten IDocumentStore per module
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [x] Clean keycloak export
- [x] https on keycloak 
- [x] Go to aspire & clean docker-compose
- [x] AI remote MCP tools to cache instead of perform at each request
- [ ] Transaction behavior using martenDB unit of work
- [ ] Integration tests (xUnit)
- [ ] Multi tenancy
  - [ ] EF CORE global filter by tenant
  - [ ] marten session by tenant
- [ ] Bus & integration event ?

### Desktop
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [ ] Add Authentication option ?

### CLI
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [ ] Add Authentication option ?

### BaaS
- [x] CLI
  - [x] Rename it in CLI
- [x] Aspire
  - [x] Data API Builder
  - [x] Hasura graphQL engine
- [ ] Hasura
  - [x] Manage hasura console
  - [x] Authentication
- [ ] Dab
  - [ ] [Authentication - Github issue](https://github.com/Azure/data-api-builder/issues/2226)

### Documentation
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [x] Add Authentication / keycloak option
- [ ] Dissociate aspire & authentication option
- [ ] Add OTEL with serviceDefault

### Scheduler
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [x] Docker
- [x] Aspire
- [x] CLI
- [ ] Authentication
  - [x] No auth
  - [x] Basic auth
  - [x] Api key auth
  - [ ] Host auth
    - [ ] https://github.com/Arcenox-co/TickerQ/issues/575
- [ ] Dissociate aspire & authentication option
- [ ] Add OTEL with serviceDefault

### Spec Driven Development
- [ ] Custom agents
  - [ ] Product manager
  - [ ] Engineering manager
  - [ ] Software engineer
  - [ ] Product designer
  - [ ] QA engineer
- [ ] Rules
  - [ ] Blazor
  - [ ] gRPC
  - [ ] GraphQL
  - [ ] REST
  - [ ] Entity framework
- [ ] Skills
- [ ] Add to CLI
- [ ] Templatize

### Workflow
- [ ] Elsa core setup
    - [ ] Server
    - [ ] Studio
- [ ] Docker
- [ ] Aspire
- [ ] Add in CLI
- [ ] Add IAM authentication
- [ ] AI ?

-------------------------

## Templates sandbox
- [ ] Mobile (MAUI / Uno) ?
- [ ] Spreadsheet as a Service (NocoDB / Superset) ?

-------------------------

## Boilerplate CLI
- [x] .NET 10 (CentralPackageManagement, .slnx, upgrade nuget)
- [ ] Update 2026 templates & options
- [ ] Refactor template system
