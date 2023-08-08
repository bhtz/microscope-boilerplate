using System.ComponentModel.DataAnnotations;

namespace Microscope.Storage;

public class StorageOptions
{
    public static readonly string Name = "Storage";
    
    [Required]
    public string Adapter { get; set; }
    [Required]
    public string Host { get; set; }
    public string Key { get; set; }
    public string Secret { get; set; }
}
