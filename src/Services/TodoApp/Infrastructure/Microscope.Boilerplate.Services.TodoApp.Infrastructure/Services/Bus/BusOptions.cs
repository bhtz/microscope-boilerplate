using System.ComponentModel.DataAnnotations;

namespace Microscope.Boilerplate.Services.TodoApp.Infrastructure.Services.Bus;

public class BusOptions
{
    public static readonly string ConfigurationKey = "Bus";
    
    [Required]
    public string Adapter { get; set; }
    [Required]
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}