using System.ComponentModel.DataAnnotations;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;

public class MailOptions
{
    public static readonly string ConfigurationKey = "Mail";
    
    [Required]
    public string Adapter { get; set; }
}