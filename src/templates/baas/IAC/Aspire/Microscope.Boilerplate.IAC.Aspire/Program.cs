var builder = DistributedApplication.CreateBuilder(args);

#region Database

var postgres = builder.AddContainer("postgres", "postgres:18")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "root")
    .WithEnvironment("POSTGRES_DB", "postgres")
    .WithVolume("microscope_boilerplate_baas_data", "/var/lib/postgresql")
    .WithBindMount("./Postgres/init.sql", "/docker-entrypoint-initdb.d/init.sql")
    .WithEndpoint(5432, 5432);

#endregion

#region Identity & Access Management

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "26.5.4")
    .WithEnvironment(context =>
    {
        context.EnvironmentVariables.Add("KEYCLOAK_ADMIN", "admin");
        context.EnvironmentVariables.Add("KEYCLOAK_ADMIN_PASSWORD", "microscope");

        context.EnvironmentVariables.Add("KC_DB", "postgres");
        context.EnvironmentVariables.Add("KC_DB_URL", "jdbc:postgresql://postgres:5432/mcsp_identity");
        context.EnvironmentVariables.Add("KC_DB_USERNAME", "postgres");
        context.EnvironmentVariables.Add("KC_DB_PASSWORD", "root");
        context.EnvironmentVariables.Add("KC_HTTP_ENABLED", "true");
        context.EnvironmentVariables.Add("KC_HTTPS_CERTIFICATE_FILE", "/opt/keycloak/conf/keycloak.crt");
        context.EnvironmentVariables.Add("KC_HTTPS_CERTIFICATE_KEY_FILE", "/opt/keycloak/conf/keycloak.key");
        context.EnvironmentVariables.Add("KC_HOSTNAME_STRICT", "false");
        context.EnvironmentVariables.Add("KC_LOG_LEVEL", "INFO");
        context.EnvironmentVariables.Add("KC_HTTPS_PORT", "8443");
        context.EnvironmentVariables.Add("QUARKUS_HTTP_HTTP2", "false");
        context.EnvironmentVariables.Add("QUARKUS_HTTP_LIMITS_MAX_HEADER_SIZE", "200K");
    })
    .WithHttpEndpoint(8083, 8080)
    .WithHttpsEndpoint(8443, 8443)
    .WithExternalHttpEndpoints()
    .WithBindMount("./Keycloak/realm-export.json", "/opt/keycloak/data/import/realm-export.json")
    .WithBindMount("./Keycloak/Themes/microscope/","/opt/keycloak/themes/microscope")
    .WithBindMount("./Keycloak/Certs/keycloak.crt","/opt/keycloak/conf/keycloak.crt")
    .WithBindMount("./Keycloak/Certs/keycloak.key","/opt/keycloak/conf/keycloak.key")
    .WithArgs(new[]
    {
        "start-dev",
        "--import-realm"
    })
    .WaitFor(postgres);

#endregion

#region Backend As A Service

#if (Hasura)
// Hasura graphql engine configuration with JWT authentication and Postgres database connection
var hasura = builder.AddContainer("hasura", "hasura/graphql-engine", "v2.48.12.cli-migrations-v3")
    .WithEnvironment(context =>
    {
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_DATABASE_URL", "postgresql://postgres:root@host.docker.internal:5432/mcsp_app");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_ENABLE_CONSOLE", "true");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_ADMIN_SECRET", "microscope");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_SERVER_PORT", "8000");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_UNAUTHORIZED_ROLE", "anonymous");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_ENABLE_REMOTE_SCHEMA_PERMISSIONS", "true");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_NO_OF_RETRIES", "5");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_METADATA_DIR", "/hasura-metadata");
        context.EnvironmentVariables.Add("HASURA_GRAPHQL_JWT_SECRET",
            """
                {
                    "jwk_url": "http://host.docker.internal:8083/realms/microscope/protocol/openid-connect/certs",
                    "audience": ["boilerplate-hasura"],
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
    .WithBindMount("./Hasura/metadata", "/hasura-metadata")
    .WithEndpoint(4800, 8000, "http")
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(keycloak);
#endif

#if (Dab)
// Azure Data API Builder configuration with Postgres database connection and JWT authentication
var dab = builder
    .AddContainer("dab", "mcr.microsoft.com/azure-databases/data-api-builder:latest")
    .WithBindMount("./Dab/dab-config.json", "/App/dab-config.json")
    .WithEndpoint(4700, 5000, "http")
    .WithEnvironment("HTTPS_PORTS", "4700")
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(keycloak);
#endif

#endregion

#region Services

// add your own custom services here

#endregion

#region Clients

// add your own clients here

#endregion

builder.Build().Run();
