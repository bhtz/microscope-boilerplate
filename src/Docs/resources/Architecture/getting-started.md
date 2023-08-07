# Getting started

## Requirements

* dotnet 7 SDK
* nodejs 16+
* docker engine

## Get source code
```console
git clone https://github.com/bhtz/microscope-boilerplate.git
```

## Install microscope boilerplate template
```console
dotnet new install ./microscope-boilerplate
```

## Create new solution
```console
dotnet new microscope_boilerplate -n Acme.AwesomeProject
```

## Create new solution without documentation project
```console
dotnet new microscope_boilerplate -n Acme.AwesomeProject -D "false"
```

## Build
**Build solution**
```console
dotnet build
dotnet publish
```

## Run solution
```console
cd src/IAC
docker-compose up
```

## Documentation
**Build documentation**
```console
cd src/Docs
npm run docs:build
npm run slides:build
```

### Run solution documentation (vitepress)
```console
npm run docs:dev
```

### Run presentation slides (revealjs)
```console
cd src/Docs
npm run slides:dev
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

