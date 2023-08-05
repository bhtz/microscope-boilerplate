using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Events;

public class SendMailOnTodoListCompletedEventHandler : INotificationHandler<OnTodoListCompletedEvent>
{
    private readonly IMailService _mailService;
    
    public SendMailOnTodoListCompletedEventHandler(IMailService mailService)
    {
        _mailService = mailService;
    }
    
    public async Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        await _mailService.SendTodoListCompletedMail(notification.TodoList.CreatorMail, notification.TodoList);
    }
}