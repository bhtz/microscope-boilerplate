# Microscope.Boilerplate.Documentation

Documentation as code project.

## Getting started

### Install dotnet dependencies & build
```console
dotnet restore && dotnet build
```

### Install NPM dependencies & build
```console
npm i && npm run docs:build
```

### Run documentation as code web app
```console
dotnet run
```

## Integration

### Add to an existing docker compose :
```yaml
services:

  docs:
    image: Acme/docs:latest
    ports:
      - "9000:8080"
```

