using Microscope.Boilerplate.Services.TodoApp.Application.Services;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;

public class SMTPMailService : IMailService
{
    public Task<bool> SendTodoListCompletedMail(string To, object data)
    {
        Console.WriteLine("SMTP mail adapter not implemented yet");
        return Task.FromResult(true);
    }
}
