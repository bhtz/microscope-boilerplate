using FluentValidation;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;

public class PersistenceOptions
{
    public const string ConfigurationKey = "Persistence";
    public const string INMEMORY_ADAPTER = "inmemory";
    public const string SQLITE_ADAPTER = "sqlite";
    public const string POSTGRES_ADAPTER = "postgres";
    public const string MSSQL_ADAPTER = "mssql";
    public string Adapter { get; set; }
    public string ConnectionString { get; set; }
    public bool EnableMigration { get; set; } = false;
}

public class PersistenceOptionsValidator : AbstractValidator<PersistenceOptions>
{
    public PersistenceOptionsValidator()
    {
        RuleFor(x => x.Adapter)
            .NotNull()
            .NotEmpty()
            .Must(x => x 
                is PersistenceOptions.INMEMORY_ADAPTER 
                or PersistenceOptions.POSTGRES_ADAPTER
                or PersistenceOptions.MSSQL_ADAPTER
                or PersistenceOptions.SQLITE_ADAPTER)
            .WithMessage($"Adapter must be : {PersistenceOptions.INMEMORY_ADAPTER} | {PersistenceOptions.SQLITE_ADAPTER} | {PersistenceOptions.POSTGRES_ADAPTER} | {PersistenceOptions.MSSQL_ADAPTER}, not empty or null string");

        When(x => x.Adapter is not BusOptions.INMEMORY_ADAPTER, () =>
        {
            RuleFor(x => x.ConnectionString)
                .NotNull()
                .NotEmpty()
                .WithMessage("If persistence adapter is not inmemory, ConnectionString configuration is required");
        });
    }
}