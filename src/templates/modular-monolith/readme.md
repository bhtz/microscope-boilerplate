# Microscope.Boilerplate Modular monolith service

> Microscope.Boilerplate Modular monolith service

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
  - [x] Tool CLI options
- [x] Feature management
- [x] Aspire docker composer publisher
- [x] Domain event handler
- [ ] Todo domain unit test
- [ ] Bus & integration event ?
- [ ] Multi tenancy
  - [ ] EF CORE global filter by tenant
  - [ ] marten session by tenant
- [ ] Only module symbol ?
- [x] Refactoring IRepository & Aggregate framework
- [x] Clean AuditableAggregate
- [ ] Clean up readme

## Helpers EF CORE

Navigate to module infra layer :

Generate migrations :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ migrations add InitialCreate -o ./Persistence/EFcore/Migrations

Update database :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ database update

Generate SQL script :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ migrations script > ./Persistence/EFcore/Scripts/Todo.SQL