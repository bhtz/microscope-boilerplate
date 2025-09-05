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
- [ ] GraphQL gateway
    - [ ] Fusion over trevorblade public api
- [ ] GraphQL server
    - [ ] Continents graphQL query
