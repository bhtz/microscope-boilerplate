# BFF SSR BLAZOR

> Blazor frontend + BFF pattern

* ✅ Material UI (MudBlazor)
* ✅ Version endpoint
* ✅ Server side rendering (Blazor)
* ✅ OIDC / Cookie authentication (Keycloak)
* ✅ Proxying API (Yarp)
* ✅ GraphQL gateway (HotChocolate)
* ✅ Auto generated SDK (StrawberryShake)
* ✅ Light / Dark theme
* ✅ I18N
* ✅ Feature management
* ✅ IAC (docker)
* ✅ BaaS (optional with Hasura)

### Create new CLI project
```console
dotnet new mcsp_bff_ssr_blazor -n Acme
```

### Template options

* -B, --BaaS 
    * Whether to include Backend as a Service container (Hasura) or not
    * Default : false
