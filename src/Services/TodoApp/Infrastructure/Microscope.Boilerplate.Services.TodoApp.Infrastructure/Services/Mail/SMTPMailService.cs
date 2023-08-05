using System.Net.Mail;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;

public class SMTPMailService : IMailService
{
    private Task<bool> SendMail(MailMessage mailMessage)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendTodoListCompletedMail(string To, object data)
    {
        Console.WriteLine("SMTP mail adapter not implemented yet");
        return true;
    }
}
