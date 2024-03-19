# Microscope Boilerplate

> An opiniated started kit for product engineering teams

## Requirements

* dotnet 8 SDK
* nodejs 16+
* docker engine

## Available templates

* mcsp_distributed
* mcsp_desktop
* mcsp_cli
* mcsp_doc

### mcsp_distributed
> Distributed architecture oriented 
* blazor wasm
* rest & graphql sdk
* bff & api gateway
* api "TodoApp" service
* IAM
* Storage 
* Postgres database
* OpenTelemetry
* Bus

### mcsp_doc
> Documentation as code web application
* web app
* vitepress
* markdown & mermaid 
* templates (#product, #ADR, #PRD, #guidelines, ...)
* opiniated guidelines

### mcsp_cli
> Console app with CLI & UI
* Cocona CLI
* Spectre.Console UI
* Commands folder & sample

### mcsp_desktop
> Cross platform desktop app
* Avalonia
* material ui & icons
* CommunityToolkit.MVVM

## Installation

### Get source code
```console
git clone https://github.com/bhtz/microscope-boilerplate.git
```

### Install microscope dotnet templates
```console
cd microscope-boilerplate/templates
dotnet pack
dotnet new install /bin/Release/Microscope.Boilerplate.1.0.0.nupkg
```

### Install microscope dotnet tool
```console
cd microscope-boilerplate/tool
dotnet pack
dotnet tool install --global --add-source ./nupkg Microscope.Boilerplate.Tool.CLI

```

## Uninstall tool
```console
dotnet tool uninstall Microscope.Boilerplate.Tool.CLI --global
```

---------------------------------------------

## Distributed  template

### Create new distributed solution
```console
dotnet new mcsp_distributed -n Acme.AwesomeProject
```

### Create new distributed solution with CLI
```console
dotnet new mcsp_distributed -n Acme.AwesomeProject -C
```

### Create new distributed solution with Terraform IAC setup
```console
dotnet new mcsp_distributed -n Acme.AwesomeProject -T
```

### Run solution (with docker compose)
```console
cd Acme.AwesomeProject/src/IAC/Docker
docker-compose up
```

### Run solution (with Aspire)
```console
cd Acme.AwesomeProject/src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
```

---------------------------------------------

## CLI template

### Create new CLI project
```console
dotnet new mcsp_cli -n Acme.CLI
```

---------------------------------------------

## Doc template

### Create new documentation as code project
```console
dotnet new mcsp_doc -n Acme.Doc
```

### Create new documentation as code project with default guidelines
```console
dotnet new mcsp_doc -n Acme.Doc -G
```

## Build
**Build solution**
```console
dotnet build
```

**Build solution containers**
```console
dotnet publish -p:PublishProfile=DefaultContainer
```

**Build solution containers for arm64**
```console
dotnet publish -r linux-arm64 -p:PublishProfile=DefaultContainer
```

**Build CLI containers for arm64**
```console
cd src/Clients/CLI/Microscope.Boilerplate.Clients.CLI
dotnet publish -r linux-arm64 /t:PublishContainer
```

## Run solution
```console
cd src/IAC/Docker
docker-compose up
```

## Go to app
* open [Boilerplate app](http://localhost:5215/)

## Documentation
**Build documentation**
```console
cd templates/docs/Microscope.Boilerplate.Doc
npm run docs:build
```

### Run solution documentation (vitepress)

**Install NPM packages**
```console
cd ./src/Docs/Microscope.Boilerplate.Docs
npm i
```

**Dev documentation**
```console
npm run docs:dev
```

### Dev slides (revealjs)
```console
npm run slides:dev
```

**Build docs & run**
```console
npm run docs:build
dotnet run
```

## Solution

### Environments
```console
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_ENVIRONMENT=Production
```
### SDK generation
you will need to run todoapp api first for this
```console
cd src/Clients/SDK/Microscope.Boilerplate.Clients.SDK.GraphQL
dotnet graphql update
dotnet build
```

### EF Core Tools

**Navigate to infrastructure project**
```console
cd src/Services/TodoApp/Infrastructure/Microscope.Boilerplate.Services.TodoApp.Infrastructure/
```

**Add migration**
```console
dotnet ef --startup-project ../../Interface/Microscope.Boilerplate.Services.TodoApp.Api/ migrations add InitialCreate -o ./Persistence/Migrations
```
**Update database**
```console
dotnet ef --startup-project ../../Interface/Microscope.Boilerplate.Services.TodoApp.Api/ database update
```
**Export SQL**
```console
dotnet ef --startup-project ../../Interface/Microscope.Boilerplate.Services.TodoApp.Api/ migrations script > ./Scripts/TodoApp.sql
```

### IAC (experimental)

#### ASPIRE
**Publish Aspire manifest**
```console
cd templates/src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
dotnet run --publisher manifest --output-path manifest.json
```

#### TERRAFORM
**Install Terraform & Azure CLI**
```console
brew update
brew tap hashicorp/tap
brew install azure-cli
brew install hashicorp/tap/terraform
```

**Init**
```console
terraform init
terraform plan
terraform apply
```

#### PULUMI (absolete)
**Install Pulumi & Azure CLI**
```console
brew update
brew install azure-cli
brew install pulumi/tap/pulumi
```

**Azure CLI login**
```console
az login
```

**Set azure location (optional)**
```console
cd ./src/IAC/Pulumi/Microscope.Boilerplate.IAC
pulumi config set azure-native:location westus2
```

**Deploy stack on Azure**
```console
pulumi up
```

## E2E Tests

**Setup environment**
- Register a new E2E test user on keycloak IAM
- username: admin@microscope.io
- password: microscope

**Run End to End tests**
```console
cd ./src/Clients/E2E/Microscope.Boilerplate.Clients.E2E
dotnet test
```

**Run End to End tests with GUI**
```console
cd ./src/Clients/E2E/Microscope.Boilerplate.Clients.E2E
HEADED=1 dotnet test
```
