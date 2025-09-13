# Microscope.Boilerplate

> Microscope.Boilerplate BFF template.

## Getting started 

**Build solution containers**
```console
dotnet publish -p:PublishProfile=DefaultContainer
```

**Build solution containers for arm64**
```console
dotnet publish -r linux-arm64 -p:PublishProfile=DefaultContainer
```

**Docker compose up**
```console
cd ./IAC/Docker
docker-compose up
```

```console
cd ./Microscope.Boilerplate.Clients.BFF
dotnet run
```

> 🎉 Enjoy !

## Template options
- [x] Aspire
- [x] Docker
- [x] Yarp
- [x] BaaS
    - [x] Dab
- [x] GraphQL Gateway
    - [x] Fusion over trevorblade public api
- [x] GraphQL local
- [x] Common pages
  - [x] Not Authorized
  - [x] Not found
- [x] Configure cookie authentication AccessDeniedPath
- [ ] Save prefered theme in cookie for blazor server refresh 
- [ ] UI options
  - [x] MudBlazor
  - [ ] FluentUI
