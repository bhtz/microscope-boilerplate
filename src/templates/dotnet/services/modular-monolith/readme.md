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
- [ ] Clean AuditableAggregate
- [ ] Implement persistence
  - [ ] EFcore
    - [ ] Repository
    - [ ] Configurations
    - [ ] Migrations
  - [x] Marten
    - [x] Repository
- [ ] Todo slices

## Helpers

Navigate to module infra layer :

Generate migrations :

    dotnet ef --startup-project ../../Microscope.Boilerplate.API/ migrations add InitialCreate -o Migrations

Update database :

    dotnet ef --startup-project ../../Microscope.Boilerplate.API/ database update

Generate SQL script :

    dotnet ef --startup-project ../../Interface/AuditExOp.Api/ migrations script > ./Scripts/AuditExOp.SQL