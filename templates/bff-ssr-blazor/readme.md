# Microscope.Boilerplate

Microscope.Boilerplate BFF SSR BLAZOR

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
