# Microscope.Boilerplate

Microscope.Boilerplate BFF SSR BLAZOR

## Getting started 

**Docker compose up**
```console
cd ./IAC/Docker
docker-compose up
```

[http://localhost:8083/admin/master/console/#/microscope/clients](http://localhost:8083/admin/master/console/#/microscope/clients)

* username: admin
* password: microscope

> Clients > boilerplate > credentials > regenerate secret token
 
> Copy it to your appsettings.{env}.json

```json
"ClientSecret": "dMMj1Q9yiIzQeI8Td0pMnIx9zq95NREz",
```

```console
cd ./Microscope.Boilerplate.Clients.BFF
dotnet run
```

> 🎉 Enjoy !
