# Getting started

## Requirements

* dotnet 8 SDK
* nodejs 16+
* docker engine

## Get source code
```console
git clone https://github.com/bhtz/microscope-boilerplate.git
```

## Install microscope boilerplate templates
```console
cd microscope-boilerplate
dotnet pack
dotnet new install /bin/Release/Microscope.Boilerplate.1.0.0.nupkg
```

## Create new fullstack solution
```console
dotnet new mcsp_fullstack -n Acme.AwesomeProject
```

## Create new fullstack solution without documentation project
```console
dotnet new mcsp_fullstack -n Acme.AwesomeProject -D "false"
```

## Add new CLI project
```console
dotnet new mcsp_cli -n Acme.CLI
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
cd src/Docs
npm run docs:build
npm run slides:build
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

#### PULUMI
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
