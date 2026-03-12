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
* ✅ Mcp
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
* ✅ Aspire

**Data**
* ✅ Postgres
* ✅ Open telemetry


### Create new project
```console
dotnet new mcsp_service -n Acme
```

### Template options

* -A, --Aspire
    * Whether to include aspire or not.
    * Type : bool
    * Default : false

* -Gr, --Grpc
    * Whether to include Grpc API protocol proxy or not.
    * Type : bool
    * Default : false

* -G, --Graphql
    * Whether to include GraphQL API protocol or not.
    * Type : bool
    * Default : false

* -R, --Rest
    * Whether to include Rest API protocol or not.
    * Type : bool
    * Default : false

* -M, --Mcp
    * Whether to include MCP protocol or not.
    * Type : bool
    * Default : false

* -U, --UnitTest
    * Whether to include unit tests or not.
    * Type : bool
    * Default : false
