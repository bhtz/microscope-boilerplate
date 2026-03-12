var builder = DistributedApplication.CreateBuilder(args);

#region Database

var postgres = builder.AddContainer("postgres", "postgres:18")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "root")
    .WithEnvironment("POSTGRES_DB", "postgres")
    .WithVolume("microscope_boilerplate_bff_data", "/var/lib/postgresql")
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

#region Services

// add your own services here

#endregion

#region Clients

var bff = builder.AddProject<Projects.Microscope_Boilerplate_Clients_BFF>("bff")
    .WithExternalHttpEndpoints()
    .WaitFor(keycloak);

#endregion

builder.Build().Run();
