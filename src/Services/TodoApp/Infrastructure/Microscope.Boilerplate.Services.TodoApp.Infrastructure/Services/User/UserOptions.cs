using System.ComponentModel.DataAnnotations;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.User;

public class UserOptions
{
    public static readonly string ConfigurationKey = "Users";
    
    [Required]
    public string Adapter { get; set; }
    
    [Required]
    public string UserServiceEndpoint { get; set; }
}