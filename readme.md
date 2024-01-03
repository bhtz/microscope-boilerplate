# Microscope boilerplate

An opiniated started kit for product engineering teams

## Available templates

* FULLSTACK ("mcsp_fullstack")
  * microservice (hexagonal arch) oriented fullstack, blazor, bff & api gateway, IAM, Storage, Postgres, Telemetry, Bus
* DOC ("mcsp_doc")
  *  Documentation as code Web app template (markdown & mermaid) using vitepress (#product, #ADR, #PRD, #guidelines, ...)
* CLI ("mcsp_cli")
  * Console app template using Cocona (CLI) & Spectre.Console (UI)

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

## Fullstack template

### Create new fullstack solution
```console
dotnet new mcsp_fullstack -n Acme.AwesomeProject
```

### Create new fullstack solution without documentation project
```console
dotnet new mcsp_fullstack -n Acme.AwesomeProject -D "false"
```

### Run solution
```console
cd Acme.AwesomeProject/src/IAC/Docker
docker-compose up
```

## CLI template

### Create new CLI project
```console
dotnet new mcsp_cli -n Acme.CLI
```

## Doc template

### Create new documentation as code project
```console
dotnet new mcsp_doc -n Acme.Doc
```

## Reading documentation :

[Getting started documentation](https://github.com/bhtz/microscope-boilerplate/blob/multi-template/templates/docs/Microscope.Boilerplate.Doc/resources/Architecture/getting-started.md)
