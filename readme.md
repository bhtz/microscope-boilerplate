# Microscope Boilerplate

## Requirements

* dotnet 7 SDK
* nodejs 16+

## Get source code
```console
git clone https://github.com/bhtz/microscope-boilerplate.git
```

## Install microscope boilerplate template
```console
dotnet new install ./microscope-boilerplate
```

## Create solution
```console
dotnet new microscope_boilerplate -n Acme.AwesomeProject
```

## Build
**Build solution & containers** 
```console
dotnet build
dotnet publish
```

**Build solution & containers for arm64 **
```console
dotnet build
dotnet publish -r linux-arm64
```

## Run solution
**Not implemented yet**
```console
cd src/IAC
docker-compose up
```

## Register IAM application client
* open [Keycloak console](http://localhost:8083/auth/)
* Login as admin 
  * user : admin
  * password : microscope
    * Don't forget to change this default setting
* Go to import > select file > ./src/IAC/realm-export.json 

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
