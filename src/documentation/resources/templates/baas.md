# Backend as a Service

> Data API Builder (DAB) as a BaaS

**Aspire :**
![]()

**Nitro :**
![]()

**Scalar :**
![]()

### Features

**BaaS**
* ✅ GraphQL
* ✅ Rest

**Persistence**
* ✅ Postgres

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


