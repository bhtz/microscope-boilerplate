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

## Build
**Build solution** 
```console
dotnet restore
dotnet build
dotnet publish
```

## Run solution
**Not implemented yet**
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

**Set environment**
```console
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_ENVIRONMENT=Production
```

### EF Core migration

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
