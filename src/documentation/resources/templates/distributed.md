# Distributed

> Distributed architecture oriented template

* ✅ Blazor WASM
* ✅ Rest & graphql sdk
* ✅ BFF (GraphQL gateway + reverse proxy + blazor host)
* ✅ "TodoApp" API service
* ✅ IAM (keycloak)
* ✅ Storage
* ✅ Postgres database
* ✅ OpenTelemetry (aspire + jaeger)
* ✅ Bus (rabbitMQ)
* ✅ Auto generated SDK (GraphQL + REST)
* ✅ IAC (docker + aspire)
* ✅ Light / Dark theme
* ✅ I18N
* ✅ Feature management


### Create new distributed solution
```console
dotnet new mcsp_distributed -n Acme.AwesomeProject
```

### Create new distributed solution with CLI
```console
dotnet new mcsp_distributed -n Acme.AwesomeProject -C
```

### Create new distributed solution with Terraform IAC setup
```console
dotnet new mcsp_distributed -n Acme.AwesomeProject -T
```

### Run solution (with docker compose)
```console
cd Acme.AwesomeProject/src/IAC/Docker
docker-compose up
```

### Run solution (with Aspire)
```console
cd Acme.AwesomeProject/src/IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
```

### Template options

* -B, --BaaS 
    * Whether to include Backend as a Service container (Hasura) or not
    * Default : false

* -T, --Terraform  
    * Whether to include Terraform IAC or not.
    * Type : bool
    * Default : false

* -C, --CLI
    * Whether to include CLI or not.
    * Type : bool
    * Default : false

* -E, --E2E
    * Whether to include E2E tests client or not.
    * Type : bool
    * Default : false

* -B, --BaaS
    * Whether to include Backend as a Service container (Hasura) or not.
    * Type : bool
    * Default : false
