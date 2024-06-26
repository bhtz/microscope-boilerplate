# V1

## Templating & packaging

- [x] dotnet templates
  - [x] Setup nuget package
  - [x] "mcsp_cli" template
  - [x] "mcsp_doc" template
    - [x] Guidelines option
    - [x] Tech doc option
    - [x] Product doc option
    - [x] Blog option
    - [x] Organization option
  - [x] "mcsp_distributed" template
    - [x] "Terraform" option
    - [x] "CLI“ option
    - [x] "E2E" tests option
    - [x] "BaaS" option
    - [ ] "Aspire" option
    - [ ] "Jaeger" option
    - [ ] "Minio" option
  - [x] "mcsp_desktop" (avaloniaui + material template + layout & sidemenu)
  - [x] "mcsp_bff_ssr_blazor" (blazor BFF SSR + auth template + hasura: aka #BLASURA)
    - [ ] serviceDefault option
    - [ ] aspire option
    - [ ] docker option
    - [ ] mudblazor
    - [ ] radzen
    - [ ] fluent
  - [ ] "mcsp_module" (standalone module template)
  - [ ] "mcsp_service" (standalone microservice template, module + api)
  - [ ] "mcsp_vertical_service" (standalone microservice template vertical slice style, module + api) template ?
  - [ ] "mcsp_bff_ssr_blazor_wasm" (blazor WASM + mudblazor + auth template) ?
  - [ ] "mcsp_mobile" (dotnet MAUI mobile / desktop app template) ?
  - [ ] "mcsp_mobile_avalonia" (dotnet avalonia mobile / desktop template) ?

- [x] dotnet tool
  - [x] Setup nuget package
  - [x] Clean label and options
  - [x] install as global tool "microscope"
----------------------------

## DISTRIBUTED TEMPLATE

### Solution

- [x] Hasura JWT 
- [x] Keycloak default role & role mapping
- [x] Dotnet 8 migration
- [ ] Nullables
- [ ] Clean warnings
- [ ] Clean todos

### Clients
- [ ] Move to blazor WEB & SSR instead of WASM only
  - [x] Go to BFF SSR Blazor template
- [x] Blazor PWA
  - [x] Setup mudblazor UI
  - [x] Setup Preferences & Settings
      - [x] Dark / Light mode
      - [x] Drawer open
      - [x] Language support
  - [x] Setup Authentication
  - [x] Setup Feature management
  - [x] Setup Globalization
  - [x] Setup PWA
  - [ ] Refactoring program.cs
- [x] SDKs
  - [x] GraphQL SDK
    - [ ] SDK generation with admin key dotnet graphql -x x-hasura-admin xxx
  - [ ] REST SDK
    - [x] Setup
    - [ ] Automate swagger.json output from api project
- [x] BFF
    - [x] blazor hosted
    - [x] reverse proxy APIs (YARP)
    - [x] GraphQL gateway & schema stitching
    - [ ] Forward all headers during schema stitching (related to sdk generation as admin)
    - [ ] File upload sample

### Services

#### TodoApp service
- [ ] Core
    - [x] Domain 
      - [x] Setup aggregate root
      - [x] Setup entities
      - [x] Setup domain events
      - [x] Setup repository interface
      - [x] Setup exceptions
    - [x] Application
        - [x] Common behaviours
        - [x] Mappings
        - [x] Features
            - [x] Todolist
                - [x] Commands
                  - [x] Create todo list
                  - [x] Delete todo list
                  - [x] Update todo list
                  - [x] Create todo item
                  - [x] Delete todo item
                  - [x] Toggle todo item
                - [x] Queries
                  - [x] Get all todo lists by user
                  - [x] Get todo list by id
                - [x] Policies
                  - [x] Todolist created by policy requirement
                - [x] Events
                  - [x] SendMailOnTodoListCompleted
                  - [x] OnTodoListCompletedIntegrationEvent
    - [ ] Infrastructure
      - [x] Infrastructure settings (IOption validation)
      - [ ] Persistence
           - [x] Entity framework
             - [x] EF Entities configuration
             - [x] EF Migration
           - [ ] MartenDB
      - [x] External systems implementation
          - [x] Storage
          - [x] User
          - [x] Mail
      - [ ] Bus
          - [x] MassTransit
            - [x] RabbitMQ
            - [ ] Azure Service Bus
            - [ ] Amazon SQS
            - [ ] OpenTelemetry
    - [ ] Interface
        - [x] GraphQL API
          - [x] GraphQL documentation
          - [ ] Refactoring with QueryType et MutationType attributes ?
        - [x] REST API
          - [x] OpenAPI documentation
          - [ ] Complete missing operations
          - [ ] API versioning
        - [ ] Async API / messenging documentation
        - [x] Authentication
            - [x] OPENID JWT
            - [x] MASTER KEY
        - [x] Authorization
        - [x] HealthCheck
        - [x] Feature management
        - [x] Auto migration option
        - [x] Interface settings (IOption validation)
        - [ ] Users endpoints
          - [ ] Keycloak service
          - [ ] AAD service
        - [ ] SignalR websocket use case
          - [ ] Listen service bus
        - [ ] OpenTelemetry
          - [x] REST api
          - [ ] GraphQL api
    
    - [ ] Tests
        - [x] Unit tests
        - [x] Architecture tests
        - [ ] Integration tests
        - [x] E2E tests
            - [x] Setup playwright NUnit project
            - [x] Home page test
            - [x] Login test
            - [x] Todolist test
              - [x] Create todo list test
              - [x] Update todo list test
              - [x] Create todo item test
              - [x] Toggle todo item test
              - [x] Delete todo item test
              - [x] Delete todo list test

#### POC service "Backend As A Service"
- [x] Hasura
  - [x] Docker
  - [x] Auth
- [ ] Azure Data API Builder
  - [ ] Docker
  - [ ] Auth

#### Storage (optional) ?
- [x] File system
- [x] Azure blob storage
- [ ] AWS S3
- [x] Minio
  - [ ] Upgrade minio sdk to 6.x

####  Workflow (optional)
- [ ] Elsa core

####  Scheduled Jobs (optional)
- [ ] Hangfire

### Cross cutting
- [x] SharedKernel
    - [x] use mediatr contract only
- [x] Refactoring to move Microscope.Storage crosscutting lib
- [x] ServiceDefaults
    - [x] OpenTelemetry
    - [x] Default Healthcheck

### IAC
- [x] Docker compose
  - [x] BFF
  - [x] TodoAPI
  - [x] IAM (keycloak)
  - [x] Storage (minio)
  - [x] Database (postgres)
  - [x] Bus (rabbitmq)
  - [x] BaaS (hasura)
  - [x] OTEL (Jaeger)

- [ ] Aspire
  - [x] BFF
  - [x] TodoAPI
  - [x] IAM (keycloak)
  - [ ] Storage (minio)
  - [x] Database (postgres)
  - [x] Bus (rabbitmq)
  - [x] BaaS (hasura)
  - [x] OTEL (aspire)

- [ ] Terraform
  - [x] Azure
  - [ ] AWS
  - [ ] GCP

- [ ] K8S

----------------------------

## DOCUMENTATION TEMPLATE
- [x] Setup vitepress
- [x] Setup revealjs slides
- [x] Setup docs web server as static files & container
- [x] Refactoring documentation in template
- [x] Templating options

----------------------------
