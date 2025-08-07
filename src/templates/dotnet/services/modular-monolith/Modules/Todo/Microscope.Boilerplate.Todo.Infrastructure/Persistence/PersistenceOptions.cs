using FluentValidation;

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore;

public class PersistenceOptions
{
    public const string ConfigurationKey = "Persistence";
    
    public const string MARTEN_FRAMEWORK = "marten";
    public const string ENTITY_FRAMEWORK = "efcore";

    public string Framework { get; set; } = MARTEN_FRAMEWORK;
    
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
        // WHEN ENTITY FRAMEWORK
        When(x => x.Framework == PersistenceOptions.ENTITY_FRAMEWORK, () =>
        {
            RuleFor(x => x.Adapter)
                .NotNull()
                .NotEmpty()
                .Must(x => x
                    is PersistenceOptions.INMEMORY_ADAPTER
                    or PersistenceOptions.POSTGRES_ADAPTER
                    or PersistenceOptions.MSSQL_ADAPTER
                    or PersistenceOptions.SQLITE_ADAPTER)
                .WithMessage(
                    $"Adapter must be : {PersistenceOptions.INMEMORY_ADAPTER} | {PersistenceOptions.SQLITE_ADAPTER} | {PersistenceOptions.POSTGRES_ADAPTER} | {PersistenceOptions.MSSQL_ADAPTER}, not empty or null string");

            When(x => x.Adapter is not PersistenceOptions.INMEMORY_ADAPTER, () =>
            {
                RuleFor(x => x.ConnectionString)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("If persistence adapter is not inmemory, ConnectionString configuration is required");
            });
        });

        // WHEN MARTEN DB
        When(x => x.Framework == PersistenceOptions.MARTEN_FRAMEWORK, () =>
        {
            RuleFor(x => x.ConnectionString)
                .NotNull()
                .NotEmpty()
                .WithMessage("ConnectionString configuration is required");
        });
    }
}