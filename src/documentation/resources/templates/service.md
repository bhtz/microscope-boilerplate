# Modular monolith service

> Modular monolith & vertical slices oriented asp .net service

**Aspire :**
![](/images/Service-aspire.png)

**Nitro :**
![](/images/Service-graphql.png)

**Scalar :**
![](/images/Service-scalar.png)

### Features

**Architecture**
* ✅ CQRS
* ✅ Vertical slices
* ✅ Modular monolith

**API**
* ✅ Grpc
* ✅ GraphQL
* ✅ Rest
* ✅ Feature management

**Persistence**
* ✅ MartenDB
* ✅ EFcore

**IAM**
* ✅ Keycloak (with customizable UI)

**Doc**
* ✅ scalar (Rest)
* ✅ nitro (GraphQL)

**IAC**
* ✅ Docker
* ✅ Aspire

**Data**
* ✅ Postgres
* ✅ Open telemetry


### Create new project
```console
dotnet new mcsp_service -n Acme
```

### Template options

* -D, --Docker
    * Whether to include docker compose or not.
    * Type : bool
    * Default : false

* -A, --Aspire
    * Whether to include aspire or not.
    * Type : bool
    * Default : false

* --Grpc
    * Whether to include Grpc API protocol proxy or not.
    * Type : bool
    * Default : false

* --Graphql
    * Whether to include GraphQL API protocol or not.
    * Type : bool
    * Default : false

* --Rest
    * Whether to include Rest API protocol or not.
    * Type : bool
    * Default : false


