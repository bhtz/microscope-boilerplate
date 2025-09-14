# BFF/Frontend

> Blazor frontend + BFF pattern

**Material UI :**
![](/images/BFF.png)

**Fluent UI :**
![](/images/BFF-fluent.png)

**Aspire :**
![](/images/BFF-aspire.png)

### Features

**UI**
* ✅ Material UI
* ✅ Fluent UI
    
**BFF & Gateway**
* ✅ GraphQL Fusion gateway (HotChocolate)
* ✅ Proxying API (Yarp)
* ✅ Backend as a Service (Data api builder)
* ✅ Custom endpoint (REST / GraphQL)
* ✅ OIDC / Cookie authentication (Keycloak)
* ✅ Feature management
* ✅ Blazor SSR (Server side rendering)

**FRONTEND**
* ✅ Auto generated GraphQL SDK (StrawberryShake)
* ✅ Dark mode / Light mode
* ✅ Multi-theme
* ✅ I18N

**IAM**
* ✅ IAM (keycloak)

**IAC**
* ✅ Docker
* ✅ Aspire

**Data**
* ✅ Postgres
* ✅ Open telemetry


### Create new project
```console
dotnet new mcsp_bff -n Acme
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

* -B, --BaaS 
    * Whether to include Backend as a Service container (DAB) or not
    * Type : bool
    * Default : false

* -Y, --Yarp
    * Whether to include microscope Yarp reverse proxy or not.
    * Type : bool
    * Default : false

* -G, --Gateway
    * Whether to include GraphQL Fusion gateway or not.
    * Type : bool
    * Default : false

* -U, --UI
    * Select UI framework (Material | Fluent)
    * Type : string
    * Default : Material

