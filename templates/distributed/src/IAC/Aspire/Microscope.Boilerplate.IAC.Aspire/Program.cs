using System.Net.Sockets;

var builder = DistributedApplication.CreateBuilder(args);

var pg = builder.AddPostgresContainer("postgres", port: 5432, password: "root");

var bus = builder.AddRabbitMQContainer("bus", 5672, "guest");

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "22.0")
    .WithEnvironment(context =>
    {
        context.EnvironmentVariables.Add("KEYCLOAK_ADMIN", "admin");
        context.EnvironmentVariables.Add("KEYCLOAK_ADMIN_PASSWORD", "microscope");
        context.EnvironmentVariables.Add("KEYCLOAK_DB_VENDOR", "postgres");
        context.EnvironmentVariables.Add("DB_ADDR", "host.docker.internal");
        context.EnvironmentVariables.Add("DB_PORT", "5432");
        context.EnvironmentVariables.Add("DB_DATABASE", "postgres");
        context.EnvironmentVariables.Add("DB_USER", "postgres");
        context.EnvironmentVariables.Add("DB_PASSWORD", "root");
    })
    .WithServiceBinding(8080, 8083)
    .WithVolumeMount("../../Docker/realm-export.json", "/opt/keycloak/data/import/realm-export.json")
    .WithArgs(new []
    {
        "start-dev",
        "--import-realm"
    });

var hasura = builder.AddContainer("hasura", "hasura/graphql-engine", "v2.31.0-ce")
    .WithEnvironment(context =>
    {
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_DATABASE_URL", "postgresql://postgres:root@host.docker.internal:5432/postgres");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_ENABLE_CONSOLE", "true");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_ADMIN_SECRET", "microscope");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_SERVER_PORT", "8000");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_UNAUTHORIZED_ROLE", "anonymous");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_ENABLE_REMOTE_SCHEMA_PERMISSIONS", "true");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_NO_OF_RETRIES", "5");
        // context.EnvironmentVariables.Add("HASURA_GRAPHQL_JWT_SECRET", 
        //         """
        //         {
        //             "jwk_url": "http://host.docker.internal:8083/realms/microscope/protocol/openid-connect/certs",
        //             "audience": ["poc-service"],
        //             "issuer": "http://localhost:8083/realms/microscope",
        //             "claims_map": {
        //                 "x-hasura-allowed-roles": { "path": "$$.roles" },
        //                 "x-hasura-default-role": { "path": "$$.roles[0]" },
        //                 "x-hasura-user-id": { "path": "$$.sub" }
        //             }
        //         }
        //         """
        // );
    })
    .WithServiceBinding(8000, 8080);

var todoAppService = builder.AddProject<Projects.Microscope_Boilerplate_Services_TodoApp_Api>("todoapiservice")
        .WithEnvironment("Persistence__ConnectionString", () =>
        {
            return pg.Resource?.GetConnectionString();
        })
        .WithEnvironment("Bus__Host", () =>
        {
            return bus.Resource.GetConnectionString();
        });

var bffService = builder.AddProject<Projects.Microscope_Boilerplate_Clients_BFF>("bff")
    .WithReference(todoAppService);

builder.Build().Run();
