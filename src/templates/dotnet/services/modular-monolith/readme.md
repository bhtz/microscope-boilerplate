# Microscope.Boilerplate Modular monolith service

> Microscope.Boilerplate Modular monolith service

Todo : 

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
- [ ] API KEY authentication
  - using AspNetCore.Authentication.ApiKey
- [ ] Multi tenancy
  - [ ] EF CORE global filter by tenant
  - [ ] marten session by tenant
- [ ] Refactoring IRepository & Aggregate framework
- [ ] Clean AuditableAggregate
- [ ] Clean up readme
- [ ] Bus & integration event

## Helpers EF CORE

Navigate to module infra layer :

Generate migrations :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ migrations add InitialCreate -o ./Persistence/EFcore/Migrations

Update database :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ database update

Generate SQL script :

    dotnet ef --startup-project ../../../Microscope.Boilerplate.API/ migrations script > ./Persistence/EFcore/Scripts/Todo.SQL