# Roadmap

Microscope boilerplate - Opiniated solution boilerplate & guidelines for product engineering teams

## V1

Initial setup of the boilerplate

## Template

- [ ] Setup dotnet template

## Clients
- [x] Setup mudblazor UI
- [x] Setup Preferences & Settings
    - [x] Dark / Light mode
    - [x] Drawer open
    - [x] Language support
- [x] Setup PWA
- [x] Setup Authentication
- [ ] Setup Globalization
- [ ] Setup Feature management

## Services
### TodoList 
- [ ] Core
    - [x] Domain 
      - [x] Setup aggregate root
      - [x] Setup entities
      - [x] Setup domain events
      - [x] Setup repository interface
      - [x] Setup exceptions
    - [ ] Application
        - [x] Common behaviours
        - [ ] Mappings
        - [ ] Feature
            - [ ] Todolist features
                - [ ] Commands
                - [ ] Queries
                - [ ] Policies
                - [ ] Events
  - [ ] Infrastructure
    - [ ] Persistence
         - [x] Entity framework
           - [x] EF Entities configuration
           - [x] EF Migration
         - [ ] MartenDB
    - [ ] External systems implementation
        - [ ] Storage
        - [ ] User
        - [ ] AI Prompting
        - [ ] Mail
        - [ ] PDF
    - [ ] Distributed tracing
        - [ ] OpenTelemetry
- [ ] Interface
    - [x] Setup GraphQL API
    - [x] Setup REST API
    - [x] Authentication
        - [x] OPENID JWT
        - [x] MASTER KEY
    - [x] Authorization
    - [x] HealthCheck
    - [x] Feature management
    - [ ] SignalR websocket
- [ ] Tests
    - [x] Unit tests
        - [x] Setup Unit tests
            - [x] Todolist tests
    - [ ] Integration tests
    - [x] Architecture tests

### Storage (optional) ?
- [ ] Azure blob storage
- [ ] Minio
- [ ] AWS S3
- [ ] File system

###  Workflow (optional)
- [ ] Elsa core

###  Scheduled Jobs (optional)
- [ ] Hangfire

## Cross cutting 
- [x] SharedKernel
    - [x] use mediatr contract only

## Building blocks
- [x] Reverse proxy
    - [x] Setup YARP reverse proxy
- [ ] IAC
    - [ ] docker-compose
        - [x] postgres
        - [x] keycloak
          - [ ] import realms configuration
        - [ ] Internal services
    - [ ] Azure biceps / ARM
    - [ ] K8S

## Docs
- [x] Setup vitepress
- [x] Enable task list markdown plugin
- [x] Home page
- [ ] Getting started
    - [x] Roadmap page
- [ ] Guidelines
    - [ ] Discovery & delivery dual track
    - [ ] Product & Tech Discovery
        - [ ] FOCUSED product discovery 
        - [ ] DDD Tech Discovery 
    - [ ] Agile Delivery
        - Agile guidelines
        - Application lifecycle management
- [ ] Boilerplate
    - [ ] Architecture (C4)
        - [ ] Clients
        - [ ] Services
        - [ ] Building blocks
        - [ ] Docs
        - [ ] Cross cutting
    - [ ] Testing
        - [ ] Unit tests
        - [ ] Architecture tests
        - [ ] Integration tests
        - [ ] E2E tests
    - [ ] Deploy
