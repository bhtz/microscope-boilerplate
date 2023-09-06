using System.ComponentModel.DataAnnotations;

namespace Microscope.Storage;

public class StorageOptions
{
    public const string ConfigurationKey = "Storage";
    
    [Required]
    public string Adapter { get; set; }
    [Required]
    public string Host { get; set; }
    public string Key { get; set; }
    public string Secret { get; set; }
}
