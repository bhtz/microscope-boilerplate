var builder = DistributedApplication.CreateBuilder(args);

#region Database

var postgres = builder.AddContainer("postgres", "postgres:15")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "root")
    .WithEnvironment("POSTGRES_DB", "postgres")
    .WithVolume("microscope_boilerplate_scheduler_data", "/var/lib/postgresql/data")
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

#region Services

var scheduler = builder
    .AddProject<Projects.Microscope_Boilerplate_Scheduler>("scheduler")
    .WithExternalHttpEndpoints()
    .WaitFor(postgres)
    .WaitFor(keycloak);

#endregion

builder.Build().Run();
