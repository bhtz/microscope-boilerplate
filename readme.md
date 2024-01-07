# Microscope boilerplate

An opiniated started kit for product engineering teams

## Available templates

### mcsp_distributed
> Distributed architecture oriented 
* blazor wasm
* material ui
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

### Install microscope boilerplate templates
```console
cd microscope-boilerplate
dotnet pack
dotnet new install /bin/Release/Microscope.Boilerplate.1.0.0.nupkg
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

### Run solution
```console
cd Acme.AwesomeProject/src/IAC/Docker
docker-compose up
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

## Reading documentation :

[Getting started documentation](https://github.com/bhtz/microscope-boilerplate/blob/multi-template/templates/docs/Microscope.Boilerplate.Doc/resources/Architecture/getting-started.md)
