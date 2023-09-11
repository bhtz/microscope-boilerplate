using System.Net.Mail;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RazorLight;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Microscope.Boilerplate.Services.TodoList.Infrastructure.Services.Mail;

public class SendGridMailService : IMailService
{
    private readonly IConfiguration _configuration;
    private readonly IOptions<MailOptions> _options;

    public SendGridMailService(IConfiguration configuration, IOptions<MailOptions> options)
    {
        _configuration = configuration;
        _options = options;
    }

    private async Task<bool> SendMail(MailMessage mailMessage)
    {
        var apiKey = _options.Value.ApiKey;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("benjamin.heintz@live.com", "Boilerplate");
        var subject = mailMessage.Subject;
        var to = new EmailAddress(mailMessage.To.First().ToString(), null);

        string htmlContent = string.Empty;
        if (mailMessage.IsBodyHtml)
        {
            htmlContent = mailMessage.Body;
        }

        var plainTextContent = mailMessage.Body;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }

    private RazorLightEngine GetTemplateEngine()
    {
        return new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(SendGridMailService))
            .SetOperatingAssembly(typeof(SendGridMailService).Assembly)
            .UseMemoryCachingProvider()
            .Build();
    }

    public async Task<bool> SendUserInvitation(List<string> emails)
    {
        var engine = this.GetTemplateEngine();

        string html = await engine.CompileRenderAsync("Templates.UserInvitationMailTemplate", string.Empty);

        foreach (var target in emails)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = $"Boilerplate invitation",
                Body = html,
                IsBodyHtml = true
            };

            mail.To.Add(target);
            await this.SendMail(mail);
        }

        return true;
    }

    public async Task<bool> SendTodoListCompletedMail(string To, object data)
    {
        TodoApp.Domain.Aggregates.TodoListAggregate.TodoList todoList = (TodoApp.Domain.Aggregates.TodoListAggregate.TodoList)data;
        var engine = this.GetTemplateEngine();

        string html = await engine.CompileRenderAsync("Templates.SendTodoListCompletedMailTemplate", todoList);

        MailMessage mail = new MailMessage()
        {
            Subject = $"Boilerplate : A todolist {todoList.Name} have been completed",
            Body = html,
            IsBodyHtml = true
        };

        mail.To.Add(To);

        return await this.SendMail(mail);
    }
}
