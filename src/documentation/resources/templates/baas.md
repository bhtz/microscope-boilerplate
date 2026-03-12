# Backend as a Service

> Backend as a service tools

### Features

**BaaS**
* ✅ Hasura graphql engine (2.X)
* ✅ Data API builder

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
dotnet new mcsp_baas -n Acme
```

### Template options

* -H, --Hasura 
    * Whether to include aspire (include authentication using keycloak IAM) or not.
    * Type : bool
    * Default : false

* -D, --Dab 
    * Whether to include aspire (include authentication using keycloak IAM) or not.
    * Type : bool
    * Default : false

