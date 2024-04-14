using System.Net.Mail;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Services;

public interface IMailService
{
    Task<bool> SendTodoListCompletedMail(string To, object data);
}