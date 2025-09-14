# Microscope Boilerplate

> An opiniated started kit for product engineering teams

[Documentation](https://bhtz.github.io/microscope-boilerplate/)

![](http://localhost:5555/microscope-boilerplate/images/tool.png)

## Requirements

* dotnet 9 SDK
* nodejs 20+
* docker engine

## Templates

* mcsp_bff
* mcsp_service
* mcsp_desktop
* mcsp_cli
* mcsp_doc
* mcsp_dab

### mcsp_service
> Vertical slice architecture & modular monolith 
* ✅ REST
* ✅ GRPC
* ✅ GRAPHQL
* ✅ Vertical slices architecture
* ✅ IAM
* ✅ Postgres database
* ✅ OpenTelemetry
* ✅ Aspire
* ✅ Docker
* ✅ EFcore
* ✅ MartenDB
* ✅ Feature management

### mcsp_bff
> Blazor frontend + BFF pattern
* ✅ Material UI | Fluent UI
* ✅ Custom endpoint (Rest / GraphQL)
* ✅ Server side rendering (Blazor)
* ✅ OIDC / Cookie authentication (Keycloak)
* ✅ Proxying API (Yarp)
* ✅ GraphQL gateway (HotChocolate)
* ✅ Auto generated SDK (StrawberryShake)
* ✅ Light / Dark mode
* ✅ I18N
* ✅ Feature management
* ✅ Docker
* ✅ Aspire

### mcsp_doc
> Documentation as code web application
* ✅ web app
* ✅ vitepress
* ✅ markdown & mermaid 
* ✅ templates (#product, #ADR, #PRD, #guidelines, ...)
* ✅ opiniated guidelines

### mcsp_cli
> Console app with CLI & UI
* ✅ Cocona CLI
* ✅ Spectre.Console UI
* ✅ Commands folder & sample

### mcsp_desktop
> Cross platform desktop app
* ✅ Avalonia
* ✅ material ui & icons
* ✅ CommunityToolkit.MVVM

### Get source code
```console
git clone https://github.com/bhtz/microscope-boilerplate.git
```

---------------------------------------------

## Templates

### Install & uninstall microscope dotnet templates
```console
cd microscope-boilerplate/templates
dotnet pack
dotnet new install ./bin/Release/Microscope.Boilerplate.1.0.0.nupkg
dotnet new uninstall Microscope.Boilerplate
```

### Create new Service solution
```console
dotnet new mcsp_service -n Acme.AwesomeProject
```

### Create new BFF/Frontend solution
```console
dotnet new mcsp_bff -n Acme.AwesomeProject -C
```

### Create new CLI project
```console
dotnet new mcsp_cli -n Acme.CLI
```

### Create new Documentation as code project
```console
dotnet new mcsp_doc -n Acme.Doc
```

## CLI

### Install & uninstall microscope CLI (as dotnet tool)
```console
cd microscope-boilerplate/tool
dotnet pack
dotnet tool install --global --add-source ./nupkg Microscope.Boilerplate.Tool.CLI
dotnet tool uninstall --global  Microscope.Boilerplate.Tool.CLI
```

### Install templates
> Will install Aspire, Hotchocolate templates & Aspire CLI
```console
microscope install
```

### Use CLI
```console
microscope
```
