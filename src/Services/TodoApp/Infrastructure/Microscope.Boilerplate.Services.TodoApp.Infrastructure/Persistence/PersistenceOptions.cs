using System.ComponentModel.DataAnnotations;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;

public class PersistenceOptions
{
    public const string ConfigurationKey = "Persistence";
    
    [Required]
    public string Adapter { get; set; }

    [Required]
    public string ConnectionString { get; set; }

    public bool EnableMigration { get; set; } = false;
}