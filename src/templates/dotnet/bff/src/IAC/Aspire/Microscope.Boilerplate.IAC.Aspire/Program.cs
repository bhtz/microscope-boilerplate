using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

#region Configuration

var isOllamaEnabled = builder.Configuration.GetValue<bool>("Ollama");
var isHasuraEnabled = builder.Configuration.GetValue<bool>("BaaS:Hasura");
var isDabEnabled = builder.Configuration.GetValue<bool>("BaaS:Dab");
var isDocEnabled = builder.Configuration.GetValue<bool>("Doc");

#endregion

#region Database

var postgres = builder.AddContainer("postgres", "postgres:15")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "root")
    .WithEnvironment("POSTGRES_DB", "postgres")
    .WithVolume("microscope_boilerplate_data", "/var/lib/postgresql/data")
    .WithBindMount("../../Docker/Postgres/init.sql", "/docker-entrypoint-initdb.d/init.sql")
    .WithEndpoint(5432, 5432);

#endregion

#region Identity & Access Management

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "22.0")
    .WithEnvironment(context =>
    {
        context.EnvironmentVariables.Add("KEYCLOAK_ADMIN", "admin");
        context.EnvironmentVariables.Add("KEYCLOAK_ADMIN_PASSWORD", "microscope");

        context.EnvironmentVariables.Add("KC_DB", "postgres");
        context.EnvironmentVariables.Add("KC_DB_URL", "jdbc:postgresql://postgres:5432/mcsp_identity");
        context.EnvironmentVariables.Add("KC_DB_USERNAME", "postgres");
        context.EnvironmentVariables.Add("KC_DB_PASSWORD", "root");
        context.EnvironmentVariables.Add("KC_HTTP_ENABLED", "true");
        context.EnvironmentVariables.Add("KC_HOSTNAME_STRICT", "false");
        context.EnvironmentVariables.Add("KC_LOG_LEVEL", "INFO");
    })
    .WithEndpoint(8083, 8080)
    .WithBindMount("../../Docker/Keycloak/realm-export.json", "/opt/keycloak/data/import/realm-export.json")
    .WithBindMount("../../Docker/Keycloak/Themes/microscope/","/opt/keycloak/themes/microscope")
    .WithArgs(new[]
    {
        "start-dev",
        "--import-realm"
    })
    .WaitFor(postgres);

#endregion

#region Backend As A Service

if (isHasuraEnabled)
{
    var hasura = builder.AddContainer("hasura", "hasura/graphql-engine", "v2.38.0")
        .WithEnvironment(context =>
        {
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_DATABASE_URL", "postgresql://postgres:root@host.docker.internal:5432/mcsp_app");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_ENABLE_CONSOLE", "true");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_ADMIN_SECRET", "microscope");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_SERVER_PORT", "8000");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_UNAUTHORIZED_ROLE", "anonymous");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_ENABLE_REMOTE_SCHEMA_PERMISSIONS", "true");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_NO_OF_RETRIES", "5");
            context.EnvironmentVariables.Add("HASURA_GRAPHQL_JWT_SECRET",
                """
                {
                    "jwk_url": "http://host.docker.internal:8083/realms/microscope/protocol/openid-connect/certs",
                    "audience": ["poc-service"],
                    "issuer": "http://localhost:8083/realms/microscope",
                    "claims_map": {
                        "x-hasura-allowed-roles": ["admin", "user"],
                        "x-hasura-default-role": { "path": "$.roles[0]" },
                        "x-hasura-user-id": { "path": "$.sub" }
                    }
                }
                """
            );
        })
        .WithEndpoint(8080, 8000, "http")
        .WithExternalHttpEndpoints()
        .WaitFor(postgres)
        .WaitFor(keycloak);
}

if (isDabEnabled)
{
    var dab = builder
        .AddContainer("dab", "mcr.microsoft.com/azure-databases/data-api-builder:latest")
        .WithBindMount("../../Docker/Dab/dab-config.json", "/App/dab-config.json")
        .WithEndpoint(4700, 5000, "http")
        .WithEnvironment("HTTPS_PORTS", "4700")
        .WithExternalHttpEndpoints()
        .WaitFor(postgres);
}

#endregion

#region Services

// var api = builder
//     .AddProject<Projects.Mylight_Enode_API>("api")
//     .WithExternalHttpEndpoints()
//     .WaitFor(postgres)
//     .WaitFor(keycloak);

#endregion

#region Clients

var bff = builder.AddProject<Projects.Microscope_Boilerplate_Clients_BFF>("bff")
    .WithExternalHttpEndpoints()
    // .WaitFor(api)
    .WaitFor(keycloak);

#endregion

#region Documentation

// if (isDocEnabled)
// {
//     builder.AddProject<Projects.Mylight_Enode_Doc>("doc")
//         .WithExternalHttpEndpoints();
// }

#endregion

#region AI

// if (isOllamaEnabled)
// {
//     var ollama = builder
//         .AddOllama("ollama")
//         .WithDataVolume()
//         .WithOpenWebUI(x =>
//         {
//             x.WithEnvironment("ENABLE_WEBSOCKET_SUPPORT", "false");
//         });
//
//     var phi35 = ollama.AddModel("phi3.5");
//     
//     bff.WithReference(phi35);
// }

#endregion

builder.Build().Run();
